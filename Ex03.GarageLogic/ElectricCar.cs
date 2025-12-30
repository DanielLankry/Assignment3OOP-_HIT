using System;


namespace Ex03.GarageLogic
{
    public class ElectricCar : Car  
    {
        public ElectricCar(string i_LicenceNumber, string i_ModelName, Wheel[] i_Wheels, eCarColor i_CarColor, eNumberOfDoors i_NumOfDoors, float i_MaxHours, float i_CurrentEnergyAmount)
           : base(i_LicenceNumber, i_ModelName, new ElectricEngine(i_CurrentEnergyAmount, i_MaxHours), i_Wheels, i_CarColor, i_NumOfDoors)
        { }
    }
}
