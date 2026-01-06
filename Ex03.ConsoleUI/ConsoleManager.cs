using System;
using System.Collections.Generic;
using Ex03.GarageLogic;
using Ex03.GarageLogic.enums;
namespace Ex03.ConsoleUI
{
    public class ConsoleManager
    {
        private readonly GarageManager m_Garage = new GarageManager();
        private bool m_Running = true;
        public void Start()
        {
            while (m_Running)
            {
                Console.Clear();
                Console.WriteLine("=== Garage Management System ===");
                Console.WriteLine("1. Load Vehicles from File (VehiclesDB.txt)");
                Console.WriteLine("2. Add New Vehicle");
                Console.WriteLine("3. Show License Plates");
                Console.WriteLine("4. Change Vehicle Status");
                Console.WriteLine("5. Inflate Wheels to Max");
                Console.WriteLine("6. Refuel Vehicle");
                Console.WriteLine("7. Charge Electric Vehicle");
                Console.WriteLine("8. Show Vehicle Details");
                Console.WriteLine("9. Exit");
                Console.WriteLine("================================");
                Console.Write("Enter your choice: ");
                string userInput = Console.ReadLine();
                try
                {
                    switch (userInput)
                    {
                        case "1":
                            m_Garage.LoadVehiclesFromFile("VehiclesDB.txt");
                            Console.WriteLine("File loaded successfully (if existed).");
                            break;
                        case "2":
                            addNewVehicleUI();
                            break;
                        case "3":
                            showLicensePlatesUI();
                            break;
                        case "4":
                            changeStatusUI();
                            break;
                        case "5":
                            inflateWheelsUI();
                            break;
                        case "6":
                            refuelUI();
                            break;
                        case "7":
                            chargeUI();
                            break;
                        case "8":
                            showDetailsUI();
                            break;
                        case "9":
                            m_Running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Invalid input format.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (ValueRangeException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                }
                if (m_Running)
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                }
            }
        }
        private void addNewVehicleUI()
        {
            Console.Write("Enter License Plate: ");
            string license = Console.ReadLine();
            if (m_Garage.IsVehicleInGarage(license))
            {
                Console.WriteLine("Vehicle already exists. Changing status to InRepair.");
                m_Garage.ChangeVehicleStatus(license, eVehicleStatus.InRepair);
                return;
            }
            Console.WriteLine("Choose vehicle type:");
            List<string> types = VehicleCreator.SupportedTypes;
            for (int i = 0; i < types.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + types[i]);
            }
            Console.Write("Enter choice number: ");
            int choiceIndex;
            if (!int.TryParse(Console.ReadLine(), out choiceIndex) || choiceIndex < 1 || choiceIndex > types.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }
            string selectedType = types[choiceIndex - 1];
            Console.Write("Enter Model Name: ");
            string modelName = Console.ReadLine();
            Vehicle newVehicle = VehicleCreator.CreateVehicle(selectedType, license, modelName);
            if (newVehicle is Car)
            {
                Car car = newVehicle as Car;
                Console.WriteLine("Enter Color (Blue, Green, White, Black):");
                string colorStr = Console.ReadLine();
                car.CarColor = (eCarColor)Enum.Parse(typeof(eCarColor), colorStr, true);
                Console.WriteLine("Enter Number of Doors (Two, Three, Four, Five):");
                string doorsStr = Console.ReadLine();
                car.NumberOfDoors = (eNumberOfDoors)Enum.Parse(typeof(eNumberOfDoors), doorsStr, true);
            }
            else if (newVehicle is Motorcycle)
            {
                Motorcycle moto = newVehicle as Motorcycle;
                Console.WriteLine("Enter License Type (A1, A2, AA, B):");
                string licStr = Console.ReadLine();
                moto.LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), licStr, true);
                Console.WriteLine("Enter Engine Volume (cc):");
                moto.EngineVolume = int.Parse(Console.ReadLine());
            }
            else if (newVehicle is Truck)
            {
                Truck truck = newVehicle as Truck;
                Console.WriteLine("Is Hazardous Cargo? (true/false):");
                truck.IsHazardousCargo = bool.Parse(Console.ReadLine());
                Console.WriteLine("Enter Cargo Volume:");
                truck.CargoVolume = float.Parse(Console.ReadLine());
            }
            Console.Write("Enter Wheel Manufacturer Name: ");
            string wheelManu = Console.ReadLine();
            Console.Write("Enter Current Air Pressure: ");
            float currentAir = float.Parse(Console.ReadLine());
            foreach (Wheel w in newVehicle.Wheels)
            {
                w.ManufacturerName = wheelManu;
                w.Inflate(currentAir);
            }
            Console.Write("Enter Current Energy Amount: ");
            float currentEnergy = float.Parse(Console.ReadLine());
            newVehicle.Engine.CurrentEnergyAmount = currentEnergy;
            Console.Write("Enter Owner Name: ");
            string owner = Console.ReadLine();
            Console.Write("Enter Owner Phone: ");
            string phone = Console.ReadLine();
            m_Garage.AddNewVehicle(newVehicle, owner, phone);
            Console.WriteLine("Vehicle added successfully!");
        }
        private void showLicensePlatesUI()
        {
            Console.WriteLine("Filter by status? (1-InRepair, 2-Repaired, 3-Paid, 0-All):");
            int filter = int.Parse(Console.ReadLine());
            List<string> plates;
            if (filter >= 1 && filter <= 3)
            {
                eVehicleStatus status = (eVehicleStatus)(filter - 1);
                plates = m_Garage.GetAllLicensePlates(status);
            }
            else
            {
                plates = m_Garage.GetAllLicensePlates(null);
            }
            Console.WriteLine("License Plates:");
            foreach (string plate in plates)
            {
                Console.WriteLine(plate);
            }
        }
        private void changeStatusUI()
        {
            Console.Write("Enter License Plate: ");
            string license = Console.ReadLine();
            Console.WriteLine("Enter New Status (1-InRepair, 2-Repaired, 3-Paid):");
            int statusInt = int.Parse(Console.ReadLine());
            eVehicleStatus status = (eVehicleStatus)(statusInt - 1);
            m_Garage.ChangeVehicleStatus(license, status);
            Console.WriteLine("Status updated.");
        }
        private void inflateWheelsUI()
        {
            Console.Write("Enter License Plate: ");
            string license = Console.ReadLine();
            m_Garage.InflateWheelsToMax(license);
            Console.WriteLine("Wheels inflated to max.");
        }
        private void refuelUI()
        {
            Console.Write("Enter License Plate: ");
            string license = Console.ReadLine();
            Console.Write("Enter Fuel Type (Soler, Octan95, Octan98): ");
            string type = Console.ReadLine();
            Console.Write("Enter Amount: ");
            float amount = float.Parse(Console.ReadLine());
            m_Garage.RefuelVehicle(license, type, amount);
            Console.WriteLine("Refueled successfully.");
        }
        private void chargeUI()
        {
            Console.Write("Enter License Plate: ");
            string license = Console.ReadLine();
            Console.Write("Enter Minutes to charge: ");
            float minutes = float.Parse(Console.ReadLine());
            m_Garage.ChargeVehicle(license, minutes);
            Console.WriteLine("Charged successfully.");
        }
        private void showDetailsUI()
        {
            Console.Write("Enter License Plate: ");
            string license = Console.ReadLine();
            Console.WriteLine(m_Garage.GetVehicleDetails(license));
        }
    }
}
