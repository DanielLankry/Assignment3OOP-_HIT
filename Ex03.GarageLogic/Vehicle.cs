using System;


namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        readonly string r_LicenseNumber;
        readonly string m_ModelName;
        readonly Engine r_Engine; //  Maybe not readonly
        // Add Wheels class later 

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
    }
}
