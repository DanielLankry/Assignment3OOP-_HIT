using System;

namespace Ex03.GarageLogic
{
    public abstract class Truck : Vehicle
    {
        public bool m_IsHazardousCargo;
        public float m_CargoVolume;
        private eCarColor m_CarColor;
        eNumberOfDoors m_NumberOfDoors;
        public eCarColor CarColor
        {
            get
            {
                return m_CarColor;
            }
            private set
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
            private set
            {
                m_NumberOfDoors = value;
            }
        }
        public bool IsHazardousCargo
        {
            get { return m_IsHazardousCargo; }
            set { m_IsHazardousCargo = value; }
        }
        public float CargoVolume
        {
            get { return m_CargoVolume; }
            set { m_CargoVolume = value; }
        }
        protected Truck(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
        }
        public override string GetTypeSpecificDetails()
        {
            return string.Format("Hazardous Cargo: {0}\nCargo Volume: {1}", IsHazardousCargo, CargoVolume);
        }
        public override void LoadTypeSpecificData(string[] i_Parts, int i_StartIndex)
        {
            if (i_Parts.Length < i_StartIndex + 2)
            {
                throw new FormatException("Truck requires hazardous cargo flag and cargo volume.");
            }

            IsHazardousCargo = bool.Parse(i_Parts[i_StartIndex]);
            CargoVolume = float.Parse(i_Parts[i_StartIndex + 1]);
        }
    }
}
