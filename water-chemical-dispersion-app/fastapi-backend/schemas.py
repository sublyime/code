from pydantic import BaseModel
from datetime import datetime

class SpillCreate(BaseModel):
    chemical_name: str
    spill_location: str
    volume: float

class SimulationResponse(BaseModel):
    id: int
    dispersion_model: str
    weather_data: str
    tidal_data: str
    created_at: datetime

    class Config:
        from_attributes = True  # For ORM compatibility
