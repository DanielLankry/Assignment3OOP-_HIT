using System;


namespace Ex03.GarageLogic
{
    
    public class Car : Vehicle
    {
        eCarColor m_CarColor;
        eNumberOfDoors m_NumberOfDoors;

        public eCarColor CarColor
        {
            get
            {
                return m_CarColor;
            }
            set
            {
                m_CarColor = value;
            }
        }
        public eNumberOfDoors NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }
            set
            {
                m_NumberOfDoors = value;
            }
        }

        protected Car(string i_LicenseNumber, string i_ModelName) : base(i_LicenseNumber, i_ModelName)
        {
        }

    }
}
