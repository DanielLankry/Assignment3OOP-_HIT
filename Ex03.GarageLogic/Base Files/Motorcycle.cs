using System;


namespace Ex03.GarageLogic
{
    public enum eLicenseType { A1, A2, AA, B }

    public abstract class Motorcycle : Vehicle
    {
        public eLicenseType LicenseType { get; set; }
        public int EngineVolume { get; set; }

        protected Motorcycle(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
        }
    }
}