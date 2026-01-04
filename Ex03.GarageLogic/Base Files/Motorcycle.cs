using System;


namespace Ex03.GarageLogic
{
    public enum eLicenseType { A1, A2, AA, B }

    public abstract class Motorcycle : Vehicle
    {
        public readonly eLicenseType r_LicenseType;
        public readonly int r_EngineVolume;

        public eLicenseType LicenseType
        {
            get { return r_LicenseType; }
        }
        public int EngineVolume
        {
            get { return r_EngineVolume; }
        }

        protected Motorcycle(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
        }
    }
}