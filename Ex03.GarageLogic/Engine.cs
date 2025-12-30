using System;


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
                if(value <= m_MaxEnergyAmount && value >= 0)
                {
                    m_CurrentEnergyAmount = value;
                }
                // Add exception after Video
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
    }
}
