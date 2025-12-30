using System;


namespace Ex03.GarageLogic
{
    public class Wheel
    {
        readonly string r_ManufacturerName;
        readonly float r_MaxAirPressure;
        float m_CurrentAirPressure;

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            r_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = i_CurrentAirPressure;
        }

        public string ManafacturerName
        {
            get { return r_ManufacturerName; }
        }
        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }
        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }


    }
}
