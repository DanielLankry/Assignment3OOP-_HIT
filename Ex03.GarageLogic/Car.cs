using System;


namespace Ex03.GarageLogic
{
    public enum eCarColor
    {
        Blue,
        Green,
        White,
        Black
    }
    public enum eNumberOfDoors
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5

    }
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


    }
}
