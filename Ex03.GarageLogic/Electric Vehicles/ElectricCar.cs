using System;


namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        public ElectricCar(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            Engine = new ElectricEngine(4.2f); 
            for (int i = 0; i < 5; i++)
            {
                Wheels.Add(new Wheel(33f));
            }
        }
    }
}