from sqlalchemy import Column, Integer, String, Float, DateTime, ForeignKey
from sqlalchemy.sql import func
from database import Base

class Spill(Base):
    __tablename__ = "spills"

    id = Column(Integer, primary_key=True, index=True)
    chemical_name = Column(String(100))
    spill_location = Column(String(255))
    volume = Column(Float)
    timestamp = Column(DateTime, default=func.now())

class Simulation(Base):
    __tablename__ = "simulation"

    id = Column(Integer, primary_key=True, index=True)
    spill_id = Column(Integer, ForeignKey("spills.id"))
    dispersion_model = Column(String)
    weather_data = Column(String)
    tidal_data = Column(String)
    created_at = Column(DateTime, default=func.now())
    updated_at = Column(DateTime, default=func.now(), onupdate=func.now())
