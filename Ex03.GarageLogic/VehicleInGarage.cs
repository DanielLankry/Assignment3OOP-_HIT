using Ex03.GarageLogic.enums;
using System;


namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        public readonly Vehicle r_Vehicle;
        public string  m_OwnerName;
        public string m_OwnerPhone;
        public eVehicleStatus m_Status;

        public string OwnerName
        {
            get { return OwnerName; }
            set { OwnerName = value; }
        }

        public string OwnerPhone
        {
            get { return OwnerPhone; }
            set { OwnerPhone = value; }
        }

        public eVehicleStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public VehicleInGarage(Vehicle i_Vehicle, string i_OwnerName, string i_Phone)
        {
            r_Vehicle = i_Vehicle;
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_Phone;
            m_Status = eVehicleStatus.InRepair;
        }
    }
}
