using System;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        public FuelMotorcycle(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            Engine = new FuelEngine(6.8f, eFuelType.Octan98);
            for (int i = 0; i < 2; i++)
            {
                Wheels.Add(new Wheel(29f));
            }
        }
    }
}
