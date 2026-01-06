using System;
using System.Collections.Generic;
using System.IO;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private readonly Dictionary<string, VehicleInGarage> m_Vehicles;

        public GarageManager()
        {
            m_Vehicles = new Dictionary<string, VehicleInGarage>();
        }

        public bool IsVehicleInGarage(string i_LicensePlate)
        {
            return m_Vehicles.ContainsKey(i_LicensePlate);
        }

        public void AddNewVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone)
        {
            VehicleInGarage newVehicleInGarage = new VehicleInGarage(i_Vehicle, i_OwnerName, i_OwnerPhone);
            m_Vehicles.Add(i_Vehicle.LicenseNumber, newVehicleInGarage);
        }

        public List<string> GetAllLicensePlates(eVehicleStatus? i_FilterStatus = null)
        {
            List<string> licenses = new List<string>();

            foreach (KeyValuePair<string, VehicleInGarage> entry in m_Vehicles)
            {
                if (i_FilterStatus == null || entry.Value.Status == i_FilterStatus)
                {
                    licenses.Add(entry.Key);
                }
            }

            return licenses;
        }

        public void ChangeVehicleStatus(string i_LicensePlate, eVehicleStatus i_NewStatus)
        {
            if (!IsVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            m_Vehicles[i_LicensePlate].Status = i_NewStatus;
        }

        public void InflateWheelsToMax(string i_LicensePlate)
        {
            if (!IsVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            Vehicle vehicle = m_Vehicles[i_LicensePlate].Vehicle;
            foreach (Wheel wheel in vehicle.Wheels)
            {
                float missingAir = wheel.MaxAirPressure - wheel.CurrentAirPressure;
                if (missingAir > 0)
                {
                    wheel.Inflate(missingAir);
                }
            }
        }

        public void RefuelVehicle(string i_LicensePlate, string i_FuelType, float i_Amount)
        {
            if (!IsVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            Vehicle vehicle = m_Vehicles[i_LicensePlate].Vehicle;
            if (!(vehicle.Engine is FuelEngine))
            {
                throw new ArgumentException("This vehicle is not fuel-based.");
            }

            vehicle.Engine.FillEnergy(i_Amount, i_FuelType);
        }

        public void ChargeVehicle(string i_LicensePlate, float i_Minutes)
        {
            if (!IsVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            Vehicle vehicle = m_Vehicles[i_LicensePlate].Vehicle;
            if (!(vehicle.Engine is ElectricEngine))
            {
                throw new ArgumentException("This vehicle is not electric.");
            }

            float hoursToAdd = i_Minutes / 60f;
            vehicle.Engine.FillEnergy(hoursToAdd, null);
        }

        public string GetVehicleDetails(string i_LicensePlate)
        {
            if (!IsVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            VehicleInGarage newVehicleInGarage = m_Vehicles[i_LicensePlate];
            Vehicle newVehicle = newVehicleInGarage.Vehicle;

            string details = string.Format(
@"License Number: {0}
Model Name: {1}
Owner Name: {2}
Status: {3}
Energy Percentage: {4:0.00}%
Current Energy Amount: {5} / {6}
Wheels: {7} (Pressure: {8})",
                newVehicle.LicenseNumber,
                newVehicle.ModelName,
                newVehicleInGarage.OwnerName,
                newVehicleInGarage.Status,
                newVehicle.EnergyPercentage,
                newVehicle.Engine.CurrentEnergyAmount,
                newVehicle.Engine.MaxEnergyAmount,
                newVehicle.Wheels[0].ManufacturerName,
                newVehicle.Wheels[0].CurrentAirPressure);

            string typeSpecificDetails = newVehicle.GetTypeSpecificDetails();
            if (!string.IsNullOrEmpty(typeSpecificDetails))
            {
                details += "\n" + typeSpecificDetails;
            }

            return details;
        }

        public List<string> LoadVehiclesFromFile(string i_FileName)
        {
            List<string> errors = new List<string>();

            if (!File.Exists(i_FileName))
            {
                errors.Add(string.Format("File not found: {0}", i_FileName));
                return errors;
            }

            string[] allLines = File.ReadAllLines(i_FileName);

            for (int i = 0; i < allLines.Length; i++)
            {
                string line = allLines[i];
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("*") || line.StartsWith("THE FORMAT") || line.StartsWith("VehicleType"))
                {
                    continue;
                }

                try
                {
                    string[] parts = line.Split(',');
                    if (parts.Length < 8)
                    {
                        throw new FormatException("Missing required fields.");
                    }

                    string type = parts[0];
                    string license = parts[1];
                    string model = parts[2];
                    float energyPercent = float.Parse(parts[3]);
                    string wheelManu = parts[4];
                    float currentPressure = float.Parse(parts[5]);
                    string ownerName = parts[6];
                    string ownerPhone = parts[7];

                    Vehicle newVehicle = VehicleCreator.CreateVehicle(type, license, model);
                    if (newVehicle == null)
                    {
                        throw new ArgumentException(string.Format("Unsupported vehicle type: {0}", type));
                    }

                    foreach (Wheel w in newVehicle.Wheels)
                    {
                        w.ManufacturerName = wheelManu;
                        w.Inflate(currentPressure);
                    }

                    float currentEnergy = (energyPercent / 100f) * newVehicle.Engine.MaxEnergyAmount;
                    newVehicle.Engine.CurrentEnergyAmount = currentEnergy;

                    newVehicle.LoadTypeSpecificData(parts, 8);

                    AddNewVehicle(newVehicle, ownerName, ownerPhone);
                }
                catch (FormatException ex)
                {
                    errors.Add(string.Format("Line {0}: {1}", i + 1, ex.Message));
                }
                catch (ArgumentException ex)
                {
                    errors.Add(string.Format("Line {0}: {1}", i + 1, ex.Message));
                }
                catch (IndexOutOfRangeException ex)
                {
                    errors.Add(string.Format("Line {0}: {1}", i + 1, ex.Message));
                }
                catch (ValueRangeException ex)
                {
                    errors.Add(string.Format("Line {0}: {1}", i + 1, ex.Message));
                }
            }

            return errors;
        }
    }
}


