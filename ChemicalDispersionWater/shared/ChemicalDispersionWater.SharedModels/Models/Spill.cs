using NetTopologySuite.Geometries;
using System;

namespace ChemicalDispersionWater.SharedModels
{
    public class Spill
    {
        public int Id { get; set; }
        public int ChemicalId { get; set; }
        public double Volume { get; set; }
        public Point? Location { get; set; }
        public DateTime Timestamp { get; set; }

        public Chemical Chemical { get; set; } = null!;
    }
}
