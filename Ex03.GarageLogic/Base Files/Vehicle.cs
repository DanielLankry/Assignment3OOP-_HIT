using System;
using System.Collections.Generic;


namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        readonly string r_LicenseNumber;
        readonly string m_ModelName;
       // public float EnergyPercentage;
        public List<Wheel> Wheels;
        public Engine Engine;

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
        //public float EnergyPercentage
        //{
        //    get
        //    {
        //        return (Engine.CurrentEnergyAmount / Engine.MaxEnergyAmount) * 100;
        //    }
        //}

        public Vehicle(string i_LicenceNumber, string i_ModelName)
        {
            r_LicenseNumber = i_LicenceNumber;
            m_ModelName = i_ModelName;
        }

        public virtual Dictionary<string, string> GetVehicleDetails()
        {
            Dictionary<string, string> info = new Dictionary<string, string>();
            info.Add("License Number", LicenseNumber);
            info.Add("Model", ModelName);
        //    info.Add("Energy %", CurentEnergyPercentage.ToString("0.00") + "%");
            return info;
        }
    }
}
