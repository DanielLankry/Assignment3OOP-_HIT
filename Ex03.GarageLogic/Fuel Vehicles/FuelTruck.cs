using System;

namespace Ex03.GarageLogic
{
    public class FuelTruck : Truck
    {
        public FuelTruck(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            Engine = new FuelEngine(140f, eFuelType.Soler);
            for (int i = 0; i < 12; i++)
            {
                Wheels.Add(new Wheel(26f));
            }
        }
    }
}