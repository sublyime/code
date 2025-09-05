using System;

namespace ChemicalDispersionWater.SharedModels
{
    public class TideInfo
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double TideHeight { get; set; }
        public string? TideType { get; set; } // e.g., "High", "Low"
        // Add other properties as needed (e.g., CurrentSpeed, Direction)
    }
}
