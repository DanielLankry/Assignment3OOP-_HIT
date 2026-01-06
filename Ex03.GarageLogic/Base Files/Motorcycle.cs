using System;


namespace Ex03.GarageLogic
{

    public abstract class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }
        public int EngineVolume
        {
            get { return m_EngineVolume; }
            set { m_EngineVolume = value; }
        }

        protected Motorcycle(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
        }

        public override string GetTypeSpecificDetails()
        {
            return string.Format("License Type: {0}\nEngine Volume: {1}cc", LicenseType, EngineVolume);
        }

        public override void LoadTypeSpecificData(string[] i_Parts, int i_StartIndex)
        {
            if (i_Parts.Length < i_StartIndex + 2)
            {
                throw new FormatException("Motorcycle requires license type and engine volume.");
            }

            LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_Parts[i_StartIndex], true);
            EngineVolume = int.Parse(i_Parts[i_StartIndex + 1]);
        }
    }
}
