using System;


namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        public ElectricMotorcycle(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            Engine = new ElectricEngine(2.6f);
            for (int i = 0; i < 2; i++)
            {
                Wheels.Add(new Wheel(29f));
            }
        }
    }
}