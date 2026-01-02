using System;

namespace Ex03.GarageLogic
{

    public class FuelEngine : Engine
    {
        eFuelType r_FuelType;
        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
            set
            {
                r_FuelType = value;
            }
        }

        public FuelEngine(float i_MaxFuelAmount, eFuelType i_FuelType) : base(i_MaxFuelAmount)
        {
            r_FuelType = i_FuelType;
        }

        public override void FillEnergy(float i_Amount, string i_EnergyType)
        {
            try
            {
                eFuelType inputType;
                if (!System.Enum.TryParse(i_EnergyType, out inputType) || inputType != FuelType)
                {
                    throw new System.ArgumentException("Invalid fuel type");
                }

                if (CurrentEnergyAmount + i_Amount > MaxEnergyAmount)
                {
                    throw new ValueRangeException(0, MaxEnergyAmount - CurrentEnergyAmount, "Fuel tank overflow");
                }
            }
            catch (ValueRangeException)
            {
                CurrentEnergyAmount += i_Amount;
            }
        }
    }
}
