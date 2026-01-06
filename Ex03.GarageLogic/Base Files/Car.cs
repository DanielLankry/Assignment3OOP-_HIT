using System;


namespace Ex03.GarageLogic
{
    
    public class Car : Vehicle
    {
       private eCarColor m_CarColor;
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

        public override string GetTypeSpecificDetails()
        {
            return string.Format("Color: {0}\nDoors: {1}", CarColor, NumberOfDoors);
        }

        public override void LoadTypeSpecificData(string[] i_Parts, int i_StartIndex)
        {
            if (i_Parts.Length < i_StartIndex + 2)
            {
                throw new FormatException("Car requires color and number of doors.");
            }

            CarColor = (eCarColor)Enum.Parse(typeof(eCarColor), i_Parts[i_StartIndex], true);
            NumberOfDoors = (eNumberOfDoors)int.Parse(i_Parts[i_StartIndex + 1]);
        }
    }
}
