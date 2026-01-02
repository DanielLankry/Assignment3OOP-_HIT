using System;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxBatteryTime) : base(i_MaxBatteryTime)
        {
        }

        public override void FillEnergy(float i_Amount, string i_EnergyType = null)
        {
            if (CurrentEnergyAmount + i_Amount > MaxEnergyAmount)
            {
                throw new ValueRangeException(0, MaxEnergyAmount - CurrentEnergyAmount, "Battery overcharge");
            }
            CurrentEnergyAmount += i_Amount;
        }
    }
}