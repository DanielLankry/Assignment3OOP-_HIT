using System;
using System.Collections.Generic;


namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        float m_CurrentEnergyAmount;
        float m_MaxEnergyAmount;

        public float CurrentEnergyAmount
        {
            get
            {
                return m_CurrentEnergyAmount;
            }
            set
            {
                if(value <= m_MaxEnergyAmount || value >= 0)
                {
                   throw new ValueRangeException(0, m_MaxEnergyAmount, "Input Error");
                }
            }
        }

        public float MaxEnergyAmount
        {
            get
            {
                return m_MaxEnergyAmount;
            }
        }

        public Engine(float i_CurrentEnergyAmount, float i_MaxEnergyAmount)
        {
            m_MaxEnergyAmount = i_MaxEnergyAmount;
            CurrentEnergyAmount = i_CurrentEnergyAmount;
        }

        public Engine(float i_MaxEnergyAmount)
        {
            m_MaxEnergyAmount = i_MaxEnergyAmount;
        }

        public abstract void FillEnergy(float i_Amount, string i_EnergyType = null);
    }
}
