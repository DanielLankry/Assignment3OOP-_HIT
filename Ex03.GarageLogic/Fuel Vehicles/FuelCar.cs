using System;


namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        public FuelCar(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            Engine = new FuelEngine(47f, eFuelType.Octan95); 
            for (int i = 0; i < 5; i++)
            {
                Wheels.Add(new Wheel(33f)); 
            }
        }
    }
}
