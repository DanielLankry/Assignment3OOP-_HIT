using System;


namespace Ex03.GarageLogic
{
    public class Wheel
    {
        readonly string r_ManufacturerName;
        float r_MaxAirPressure;
        float m_CurrentAirPressure;

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            r_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = i_CurrentAirPressure;
        }

        public Wheel(float i_MaxAirPressure)
        {
            MaxAirPressure = i_MaxAirPressure;
        }
        public string ManafacturerName
        {
            get { return r_ManufacturerName; }
        }
        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
            private set { r_MaxAirPressure = value; }
        }
        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            private set { m_CurrentAirPressure = value; }
        }

        public void Inflate(float i_AirToAdd)
        {
            if (m_CurrentAirPressure + i_AirToAdd > r_MaxAirPressure)
            {
                throw new ValueRangeException(0, r_MaxAirPressure - m_CurrentAirPressure, "Air Pressure is to high");
            }
            m_CurrentAirPressure += i_AirToAdd;
        }

    }
}
