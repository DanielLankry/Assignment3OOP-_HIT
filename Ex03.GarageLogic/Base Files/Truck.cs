using System;

namespace Ex03.GarageLogic
{
    public abstract class Truck : Vehicle
    {
        public bool IsHazardousCargo { get; set; }
        public float CargoVolume { get; set; }

        protected Truck(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
        }
    }
}
