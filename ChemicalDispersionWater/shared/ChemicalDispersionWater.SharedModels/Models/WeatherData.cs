using System;

namespace ChemicalDispersionWater.SharedModels
{
    public class WeatherData
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double TemperatureC { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double WindDirection { get; set; }
        // Add other properties as needed (e.g., Precipitation, AtmosphericPressure)
    }
}
