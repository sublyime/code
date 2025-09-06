from fastapi import FastAPI, Depends, HTTPException
from sqlalchemy.orm import Session
from database import get_db, engine
from models import Base, Spill, Simulation
from schemas import SpillCreate, SimulationResponse
import requests
from math import sqrt
import json

app = FastAPI()

# Create tables if not exist (run once or on startup)
Base.metadata.create_all(bind=engine)

@app.post("/api/simulate", response_model=SimulationResponse)
def simulate_spill(spill_data: SpillCreate, db: Session = Depends(get_db)):
    # Save spill
    spill = Spill(
        chemical_name=spill_data.chemical_name,
        spill_location=spill_data.spill_location,
        volume=spill_data.volume
    )
    db.add(spill)
    db.commit()
    db.refresh(spill)

    # Parse lat/lon
    try:
        coords = spill_data.spill_location.split(",")
        lat = float(coords[0].strip())
        lon = float(coords[1].strip())
    except Exception as e:
        raise HTTPException(status_code=400, detail=f"Invalid spillLocation format: {str(e)}")

    # Hardcoded station ID (extend to find nearest)
    station_id = "9414290"  # San Francisco example

    noaa_api_url = "https://api.tidesandcurrents.noaa.gov/api/prod/"

    # Fetch weather (met data)
    weather_url = f"{noaa_api_url}?station={station_id}&product=met&datum=MLLW&time_zone=lst_ldt&units=metric&format=json&date=today"
    try:
        weather_response = requests.get(weather_url)
        weather_data = weather_response.json() if weather_response.ok else {"error": "Weather fetch failed"}
    except Exception as e:
        weather_data = {"error": str(e)}

    # Fetch tides (water level)
    tides_url = f"{noaa_api_url}?station={station_id}&product=water_level&datum=MLLW&time_zone=lst_ldt&units=metric&format=json&date=today"
    try:
        tides_response = requests.get(tides_url)
        tides_data = tides_response.json() if tides_response.ok else {"error": "Tides fetch failed"}
    except Exception as e:
        tides_data = {"error": str(e)}

    # Basic dispersion model
    diffusion = sqrt(2 * spill_data.volume)
    model_output = json.dumps({"diffusion": diffusion, "lat": lat, "lon": lon})

    # Save simulation
    simulation = Simulation(
        spill_id=spill.id,
        dispersion_model=model_output,
        weather_data=json.dumps(weather_data),
        tidal_data=json.dumps(tides_data)
    )
    db.add(simulation)
    db.commit()
    db.refresh(simulation)

    return SimulationResponse(
        id=simulation.id,
        dispersion_model=simulation.dispersion_model,
        weather_data=simulation.weather_data,
        tidal_data=simulation.tidal_data,
        created_at=simulation.created_at
    )
