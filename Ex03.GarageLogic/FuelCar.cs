using System;


namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        public FuelCar(string i_LicenceNumber, string i_ModelName, Wheel[] i_Wheels, eCarColor i_CarColor, eNumberOfDoors i_NumOfDoors, float i_MaxFuelAmount, float i_CurrentFuelAmount, eFuelType i_FuelType)
            : base(i_LicenceNumber, i_ModelName, new FuelEngine(i_FuelType, i_CurrentFuelAmount, i_MaxFuelAmount), i_Wheels, i_CarColor, i_NumOfDoors)
        { }

    }
}
