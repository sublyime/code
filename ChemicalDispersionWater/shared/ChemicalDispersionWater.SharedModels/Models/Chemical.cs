namespace ChemicalDispersionWater.SharedModels;

public class Chemical
{
    public int Id { get; set; }

    // Common name or UN/NA identifier
    public string Name { get; set; } = string.Empty;

    // Mass-per-volume at 20 °C; units: g/cm³  (adjust units as you prefer)
    public double Density { get; set; }

    // Optional extras that often matter in dispersion modelling
    public double MolecularWeight { get; set; }        // g/mol
    public double BoilingPointC { get; set; }          // °C
    public double SolubilityMgL { get; set; }          // mg/L
}
