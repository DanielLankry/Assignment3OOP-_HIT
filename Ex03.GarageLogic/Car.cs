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

        public Car(string i_LicenceNumber, string i_ModelName, Engine i_Engine, Wheel[] i_Wheels, eCarColor i_CarColor, eNumberOfDoors i_NumberOfDoors)
            : base(i_LicenceNumber, i_ModelName, i_Engine, i_Wheels)
        { }

    }
}
