using System;


namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        readonly string r_LicenseNumber;
        readonly string m_ModelName;
        readonly Engine r_Engine;
        readonly Wheel[] r_Wheels; 

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }
        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
        }
        public Engine Engine
        {
            get
            {
                return r_Engine;
            }
        }

        public Vehicle(string i_LicenceNumber, string i_ModelName, Engine i_Engine, Wheel[] i_Wheels)
        {
            r_LicenseNumber = i_LicenceNumber;
            m_ModelName = i_ModelName;
            r_Engine = i_Engine;
            r_Wheels = i_Wheels;
        }
    }
}
