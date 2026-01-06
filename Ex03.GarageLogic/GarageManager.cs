using System;
using System.Collections.Generic;
using System.IO;
using Ex03.GarageLogic.enums;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        // המילון שמחזיק את הרכבים לפי מספר רישוי
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

            // המרה מדקות לשעות
            float hoursToAdd = i_Minutes / 60f;
            vehicle.Engine.FillEnergy(hoursToAdd, null);
        }

        public string GetVehicleDetails(string i_LicensePlate)
        {
            if (!IsVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            VehicleInGarage entry = m_Vehicles[i_LicensePlate];
            Vehicle v = entry.Vehicle;

            // שימוש ב-StringBuilder או שרשור מחרוזות פשוט לבניית התצוגה
            string details = string.Format(
@"License Number: {0}
Model Name: {1}
Owner Name: {2}
Status: {3}
Energy Percentage: {4:0.00}%
Current Energy Amount: {5} / {6}
Wheels: {7} (Pressure: {8})",
                v.LicenseNumber,
                v.ModelName,
                entry.OwnerName,
                entry.Status,
                v.EnergyPercentage,
                v.Engine.CurrentEnergyAmount,
                v.Engine.MaxEnergyAmount,
                v.Wheels[0].ManufacturerName, // בהנחה שכל הגלגלים זהים
                v.Wheels[0].CurrentAirPressure);

            // הוספת פרטים ספציפיים לפי סוג הרכב
            if (v is Car)
            {
                Car car = v as Car;
                details += string.Format("\nColor: {0}\nDoors: {1}", car.CarColor, car.NumberOfDoors);
            }
            else if (v is Motorcycle)
            {
                Motorcycle moto = v as Motorcycle;
                details += string.Format("\nLicense Type: {0}\nEngine Volume: {1}cc", moto.LicenseType, moto.EngineVolume);
            }
            else if (v is Truck)
            {
                Truck truck = v as Truck;
                details += string.Format("\nHazardous Cargo: {0}\nCargo Volume: {1}", truck.IsHazardousCargo, truck.CargoVolume);
            }

            return details;
        }

        public void LoadVehiclesFromFile(string i_FileName)
        {
            if (!File.Exists(i_FileName))
            {
                return;
            }

            string[] allLines = File.ReadAllLines(i_FileName);

            foreach (string line in allLines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("*") || line.StartsWith("THE FORMAT"))
                {
                    continue;
                }

                try
                {
                    string[] parts = line.Split(',');
                    // הפורמט: Type, License, Model, Energy%, WheelManu, Pressure, Owner, Phone, [Extra...]

                    string type = parts[0];
                    string license = parts[1];
                    string model = parts[2];
                    float energyPercent = float.Parse(parts[3]);
                    string wheelManu = parts[4];
                    float currentPressure = float.Parse(parts[5]);
                    string ownerName = parts[6];
                    string ownerPhone = parts[7];

                    // 1. יצירת הרכב
                    Vehicle v = VehicleCreator.CreateVehicle(type, license, model);

                    // 2. עדכון גלגלים
                    foreach (Wheel w in v.Wheels)
                    {
                        // הערה: יש לוודא שקיימת מתודה לעדכון שם יצרן ב-Wheel או שהשדה public
                        w.ManufacturerName = wheelManu; // דורש set public ב-Wheel
                        w.Inflate(currentPressure);
                    }

                    // 3. עדכון אנרגיה
                    float currentEnergy = (energyPercent / 100f) * v.Engine.MaxEnergyAmount;
                    v.Engine.CurrentEnergyAmount = currentEnergy;

                    // 4. עדכון מאפיינים ייחודיים
                    if (v is Car)
                    {
                        Car car = v as Car;
                        // המרה מהטקסט ל-Enum
                        car.CarColor = (eCarColor)Enum.Parse(typeof(eCarColor), parts[8], true);
                        car.NumberOfDoors = (eNumberOfDoors)int.Parse(parts[9]);
                    }
                    else if (v is Motorcycle)
                    {
                        Motorcycle moto = v as Motorcycle;
                        moto.LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), parts[8], true);
                        moto.EngineVolume = int.Parse(parts[9]);
                    }
                    else if (v is Truck)
                    {
                        Truck truck = v as Truck;
                        truck.IsHazardousCargo = bool.Parse(parts[8]);
                        truck.CargoVolume = float.Parse(parts[9]);
                    }

                    AddNewVehicle(v, ownerName, ownerPhone);
                }
                catch
                {
                    // דילוג על שורה תקולה והמשך לשורה הבאה
                    continue;
                }
            }
        }
    }
}