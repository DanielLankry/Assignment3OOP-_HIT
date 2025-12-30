using System;


namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {

        public ElectricEngine(float i_CurrentHoursRemaining, float i_MaxHours) : base(i_CurrentHoursRemaining, i_MaxHours)
        { }

        public void ChargeBattery(float i_HoursToAdd)
        {
            if (i_HoursToAdd < 0)
            {
                throw new Exception("Hours to add should be greater then zero");
            }
            if (CurrentEnergyAmount + i_HoursToAdd > MaxEnergyAmount)
            {
                throw new Exception("Too many hours to add");
            }
            CurrentEnergyAmount += i_HoursToAdd;
        }
    }
}
