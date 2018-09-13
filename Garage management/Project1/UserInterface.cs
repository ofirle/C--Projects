using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        readonly InputValidation IV = new InputValidation();
        readonly GarageManeger GM = new GarageLogic.GarageManeger();

        public void PrintMenu()
        {
            string menuString = string.Format(
    @"Please Choose One Option From The Menu [1-8]:

1. Insert New Vehicle To The Garage
2. Show All The Number Licens Of THe Vehicle In The Garage (Can Be Filter By Status Of Vehicale)
3. Change Status Of Vehicle In The Garage
4. Inflate Tires To Max For A Vehicle
5. ReFuel Vehicle
6. ReCharge Vehicle
7. Show All Detials Of A Vehicle
8. Exit
");
            System.Console.WriteLine(menuString);
        }

        public void InputAfterMenu()
        {
            bool YesOrNo = false;
            int inputMenu = -1;
            string input = System.Console.ReadLine();

            while (!IV.MenuValidInput(input, ref inputMenu))
            {
                input = System.Console.ReadLine();
            }
            SwitchCaseGarage(inputMenu);

            System.Console.WriteLine("Do You Want To Do Another Action? [Y/N]: ");
            input = System.Console.ReadLine();
            while (!IV.ValidInputYesOrNo(input, ref YesOrNo))
            {
                input = System.Console.ReadLine();
            }
            if (YesOrNo == true)
            {
                PrintMenu();
                InputAfterMenu();
            }
            else
            {
                System.Console.WriteLine("GoodBye");
            }
        }

        // $G$ CSS-013 (-5) Bad variable name (should be in the form of: i_CamelCase).
        private void SwitchCaseGarage(int inputMenu)
        {
            switch (inputMenu)
            {
                case 1:
                    InsertNewVehicle();
                    break;

                case 2:
                    ShowAllPlateNumberByState();
                    break;

                case 3:
                    ChangeVehicleStatus();
                    break;

                case 4:
                    InflateWheels();
                    break;

                case 5:
                    Refuel();
                    break;
                case 6:
                    Recharge();
                    break;
                case 7:
                    PrintAllVihecleDetailsByPlateNumber();
                    break;
                case 8:
                    Environment.Exit(0);
                    break;
            }
        }

        private void ShowAllPlateNumberByState()//function 2
        {
            string CarStatus;
            eCarState eVehicleStatus = 0;
            List<string> PlateNumbersToPrint = new List<string>();
            System.Console.WriteLine("Whitch Car Status You Want To Search By? [For All(0) /InProcess(1) /Repaired(2) /Paid(3)]");
            CarStatus = System.Console.ReadLine();

            while (!IV.ValidInputStatusVehicle(CarStatus, ref eVehicleStatus))
            {
                if (CarStatus == "0")
                {
                    break;
                }
                CarStatus = System.Console.ReadLine();
            }

            if (CarStatus == "0")
            {
                GM.CreateListOfPlatesNumbers(ref PlateNumbersToPrint);
            }
            else
            {
                GM.CreatePlatesNumberByStatus(eVehicleStatus, ref PlateNumbersToPrint);
            }
            PrintListOfPlatesNumber(ref PlateNumbersToPrint);
        }

        // $G$ CSS-010 (-5) Private methods should start with an lowercase letter.
        private void PrintListOfPlatesNumber(ref List<string> plateNumbersToPrint)//continue function 2
        {
            System.Console.WriteLine(@"
Found {0} Results
", plateNumbersToPrint.Count);

            for (int i = 0; i < plateNumbersToPrint.Count; i++)
            {
                System.Console.WriteLine(@"{0}. {1}", i + 1, plateNumbersToPrint[i]);
            }
        }

        private string GetExistPlateNumber()
        {
            string plateNumber;

            System.Console.WriteLine("Please Insert Plate Number: ");
            plateNumber = System.Console.ReadLine();
            while (!GM.checkIfPlateNumberExist(plateNumber))
            {
                System.Console.WriteLine("You Inseted An Plate Number Of Vehicle That Isn't Exist In The System. Please Try Again: ");
                plateNumber = System.Console.ReadLine();
            }

            return plateNumber;
        }

        private void InsertNewVehicle()//function 1
        {
            string plateNumber;
            bool isPlateExist = false;

            System.Console.WriteLine("Please Insert Plate Number: ");
            plateNumber = System.Console.ReadLine();
            while (!IV.ValidInputIsNotEmpty(plateNumber))
            {
                plateNumber = System.Console.ReadLine();
            }
            isPlateExist = GM.checkIfPlateNumberExist(plateNumber);
            if (isPlateExist)//the vehicle is in the system
            {
                GM.ChangeStatusOfVehicleByPlateNumber(plateNumber, GarageLogic.eCarState.InProcess);
                System.Console.WriteLine("The Vehicle is Already in The System, Status has been Changed to 'In Process'");
            }
            else//the vehicle isn't register in the system
            {
                CreateNewVehicle(plateNumber);
            }
        }

        private void CreateNewVehicle(string i_PlateNumber)//continue of function 1
        {
            eKindsOfVehicles eKindOfVehicle = 0;
            string ownerName;
            string ownerPhoneNumber;
            string modelName;
            string inputString;
            float currentEnergyCapacity = 0;
            float MaxEnergyCapacityForVehicle;
            object[] GeneralParamsOfVehicle = new object[6];

            GeneralParamsOfVehicle[0] = i_PlateNumber;
            System.Console.WriteLine("Please Insert the Vehicle's Owner Name: ");
            ownerName = System.Console.ReadLine();
            while (!IV.ValidInputIsNotEmpty(ownerName))
            {
                ownerName = System.Console.ReadLine();
            }

            GeneralParamsOfVehicle[1] = ownerName;
            System.Console.WriteLine("Please Insert the Vehicle's Owner Phone Number: ");
            ownerPhoneNumber = System.Console.ReadLine();
            while (!IV.ValidInputIsNotEmpty(ownerPhoneNumber))
            {
                ownerPhoneNumber = System.Console.ReadLine();
            }
            GeneralParamsOfVehicle[2] = ownerPhoneNumber;

            System.Console.WriteLine(@"Please Choose Witch kind Of Vehicle Do You Want: 
[Car(1) /Electronic Car(2) /MotorCycle(3) /Electronic MotorCycle(4) /Truck(5)]");
            inputString = System.Console.ReadLine();
            while (!IV.ValidInputKindOfVehicle(inputString, ref eKindOfVehicle))
            {
                inputString = System.Console.ReadLine();
            }

            GeneralParamsOfVehicle[3] = eKindOfVehicle;

            System.Console.WriteLine("Please Insert The Model Name Of The Vehicle: ");
            modelName = System.Console.ReadLine();
            while (!IV.ValidInputIsNotEmpty(modelName))
            {
                modelName = System.Console.ReadLine();
            }

            GeneralParamsOfVehicle[4] = modelName;
            MaxEnergyCapacityForVehicle = GM.GetMaxCapacityForVehicle(eKindOfVehicle);
            System.Console.WriteLine("Please Insert the Vehicle's current Energy Capacity [Max {0}]: ", MaxEnergyCapacityForVehicle);
            inputString = System.Console.ReadLine();
            while (!IV.ValidInputCurrentEnergyCapacity(inputString, eKindOfVehicle, ref currentEnergyCapacity, MaxEnergyCapacityForVehicle))
            {
                inputString = System.Console.ReadLine();
            }

            GeneralParamsOfVehicle[5] = currentEnergyCapacity;

            if(eKindOfVehicle==eKindsOfVehicles.Car|| eKindOfVehicle == eKindsOfVehicles.ElectronicCar)
            {
                CreatCar(GeneralParamsOfVehicle, i_PlateNumber);
            }
            else if (eKindOfVehicle == eKindsOfVehicles.MotorCycle || eKindOfVehicle == eKindsOfVehicles.ElectronicMotorCycle)
            {
                CreatMotorCycle(GeneralParamsOfVehicle, i_PlateNumber);
            }
            else if (eKindOfVehicle == eKindsOfVehicles.Truck)
            {
                CreatTruck(GeneralParamsOfVehicle, i_PlateNumber);
            }
        }

        public void CreatCar(object[] GeneralParamsOfVehicle, string i_PlateNumber)
        {
            List<Wheel> wheels;
            eCarColor eCarColor = 0;

            string inputString;
            int carDoors = 0;
            bool isElectronic = false;

            wheels = CreateWheels(GarageManeger.ConstantsNumberOfWeels.Car, GarageManeger.ConstantsWeelsMaxPressures.Car);

            System.Console.WriteLine("Please Insert the Vehicle Color: [Grey(1) /Blue(2) /White(3) /Black(4)]");
            inputString = Console.ReadLine();

            while (!IV.ValidInputCarColor(inputString, ref eCarColor))
            {
                inputString = System.Console.ReadLine();
            }
            System.Console.WriteLine("Please Insert the Number Of Doors: [2-5]");
            inputString = System.Console.ReadLine();

            while (!IV.ValidInputCarDoors(inputString, ref carDoors))
            {
                inputString = System.Console.ReadLine();
            }

            if ((eKindsOfVehicles)GeneralParamsOfVehicle[3] == eKindsOfVehicles.ElectronicCar)
            {
                isElectronic = true;
            }
            GM.CreateCar(GeneralParamsOfVehicle, eCarColor, carDoors, ref wheels, isElectronic);
            System.Console.WriteLine("Car Entered Successfully!");

        }

        public void CreatMotorCycle(object[] GeneralParamsOfVehicle, string i_PlateNumber)
        {
            List<Wheel> wheels;
            eMotorCycleLicences motorCycleLicences = 0;

            int engineVolume = 0;
            string inputString;
            bool isElectronic = false;

            wheels = CreateWheels(GarageManeger.ConstantsNumberOfWeels.MotorCycle, GarageManeger.ConstantsWeelsMaxPressures.MotorCycle);

            System.Console.WriteLine("Please Insert the Licence Of The MotorCycle: [A(1) /A1(2) /B1(3) /B2(4)]");
            inputString = System.Console.ReadLine();

            while (!IV.ValidInputLicenceMotorCycle(inputString, ref motorCycleLicences))
            {
                inputString = System.Console.ReadLine();
            }

            System.Console.WriteLine("Please Insert Engine Volume Of The MotorCycle: ");
            inputString = System.Console.ReadLine();

            while (!IV.ValidInputIntegerNonNegative(inputString, ref engineVolume))
            {
                inputString = System.Console.ReadLine();
            }

            if ((eKindsOfVehicles)GeneralParamsOfVehicle[3] == eKindsOfVehicles.ElectronicMotorCycle)
            {
                isElectronic = true;
            }
            GM.CreateMotorCycle(GeneralParamsOfVehicle, motorCycleLicences, engineVolume, ref wheels, isElectronic);
            System.Console.WriteLine("Motor Cycle Entered Successfully!");

        }

        public void CreatTruck(object[] GeneralParamsOfVehicle, string i_PlateNumber)
        {
            List<Wheel> wheels;
            string inputString;
            float storageCapacity = 0;
            bool isFrige = false;
            wheels = CreateWheels(GarageManeger.ConstantsNumberOfWeels.Truck, GarageManeger.ConstantsWeelsMaxPressures.Truck);

            System.Console.WriteLine("Please Insert if The Truck Have Frige: [Y/N] ");
            inputString = System.Console.ReadLine();

            while (!IV.ValidInputYesOrNo(inputString, ref isFrige))
            {
                inputString = System.Console.ReadLine();
            }

            System.Console.WriteLine("Please Insert The Storage Capacity Of The Truck: ");
            inputString = System.Console.ReadLine();

            while (!IV.ValidInputFloatNonNegative(inputString, ref storageCapacity))
            {
                inputString = System.Console.ReadLine();
            }
            GM.CreateTruck(GeneralParamsOfVehicle, isFrige, storageCapacity, ref wheels);
            System.Console.WriteLine("Truck Entered Successfully!");
        }

        private List<Wheel> CreateWheels(int i_NumOfWeels, float i_MaxWheelPressure)
        {
            string WheelManufacturerName;
            string inputString;
            float wheelPressure = 0;
            List<Wheel> wheels = new List<Wheel>();

            for (int i = 0; i < i_NumOfWeels; i++)
            {
                System.Console.WriteLine("Please Insert The Wheel Manufacturer's Name: ");
                WheelManufacturerName = System.Console.ReadLine();

                System.Console.WriteLine("Please Insert Pressure Of The Current Wheel [Max {0}]: ", i_MaxWheelPressure);
                inputString = System.Console.ReadLine();

                while (!IV.ValidInputWheelPressure(inputString, ref wheelPressure, i_MaxWheelPressure))
                {
                    inputString = System.Console.ReadLine();
                }
                wheels.Add(new Wheel(WheelManufacturerName, wheelPressure, i_MaxWheelPressure));
            }

            return wheels;
        }

        private void ChangeVehicleStatus()//function 3
        {
            string stringStatusVehicle;
            string plateNumber;
            GarageLogic.eCarState eVehicleStatus = GarageLogic.eCarState.InProcess;

            plateNumber = GetExistPlateNumber();
            System.Console.WriteLine("To Which Status do You Want To Change? [InProcess(1) /Repaired(2) /Paid(3)]");
            stringStatusVehicle = System.Console.ReadLine();

            while (!IV.ValidInputStatusVehicle(stringStatusVehicle, ref eVehicleStatus))
            {
                stringStatusVehicle = System.Console.ReadLine();
            }
            GM.ChangeStatusOfVehicleByPlateNumber(plateNumber, eVehicleStatus);
            System.Console.WriteLine("Status Has Been Changed Successfully!");

        }

        private void InflateWheels()//function 4
        {
            string plateNumber;
            plateNumber = GetExistPlateNumber();
            GM.InflateWheelsToMax(plateNumber);
            System.Console.WriteLine("Inflate Tires Succsesfully!");
        }

        private void Refuel()//function 5
        {
            string plateNumber;
            string inputString;
            eFuelKinds fuelKind = 0;
            float amountToFuel = 0;
            bool isDetailMatch = false;

            try
            {
                while (!isDetailMatch)
                {
                    plateNumber = GetExistPlateNumber();

                    if (GM.IsVehicleRunOnFuel(plateNumber))
                    {
                        System.Console.WriteLine("Please Choose Fuel Type [Octan95(1) /Octan96(2) /Octan 98(3) /Solar(4)]");
                        inputString = System.Console.ReadLine();

                        while (!IV.ValidInputFuelKind(inputString, ref fuelKind))
                        {
                            inputString = System.Console.ReadLine();
                        }

                        if (GM.GetFuelKind(plateNumber) == fuelKind)
                        {
                            System.Console.WriteLine("Please Choose Amount To Fuel: ");
                            inputString = System.Console.ReadLine();

                            while (!IV.ValidInputFloatNonNegative(inputString, ref amountToFuel))
                            {
                                inputString = System.Console.ReadLine();
                            }

                            if (!GM.RefualVehicle(plateNumber, fuelKind, amountToFuel))
                            {
                                throw new ValueOutOfRangeException("Refuel");
                            }

                            isDetailMatch = true;
                            System.Console.WriteLine("Fuel Succsesfully!");
                        }
                        else
                        {
                            throw new ArgumentException("Invalid Operation! You Try To Refuel With The Wrong Fuel Type.");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Invalid Operation! You Try To Refuel an Electronic Vehicle.");
                    }
                }
            }
            catch (ValueOutOfRangeException voore)
            {
                System.Console.WriteLine("{0}", voore.Message);
            }
            catch (ArgumentException ae)
            {
                System.Console.WriteLine("{0}", ae.Message);
            }
        }

        private void Recharge()//function 6
        {
            string plateNumber;
            string inputString;
            float amountToBattery = 0;
            bool isDetailMatch = false;

            try
            {
                while (!isDetailMatch)
                {
                    plateNumber = GetExistPlateNumber();

                    if (!GM.IsVehicleRunOnFuel(plateNumber))
                    {

                        System.Console.WriteLine("Please Choose Amount Of Battery Time To Charge: ");
                        inputString = System.Console.ReadLine();

                        while (!IV.ValidInputFloatNonNegative(inputString, ref amountToBattery))
                        {
                            inputString = System.Console.ReadLine();
                        }

                        if (!GM.RechargeVehicle(plateNumber, amountToBattery))
                        {
                            throw new ValueOutOfRangeException("Recharge");
                        }
                        isDetailMatch = true;
                        System.Console.WriteLine("Recharge Succsesfully!");
                    }
                    else
                    {
                        throw new ArgumentException("Invalid Operation! You Try To Recharge a Fuel Vehicle.");
                    }
                }
            }
            catch (ValueOutOfRangeException voore)
            {
                System.Console.WriteLine("{0}", voore.Message);
            }
            catch (ArgumentException ae)
            {
                System.Console.WriteLine("{0}", ae.Message);
            }
        }

        private void PrintAllVihecleDetailsByPlateNumber()//function 7
        {
            string plateNumber;
            List<Wheel> VehicleWheels;
            plateNumber = GetExistPlateNumber();

            System.Console.WriteLine(@"
Details For The Vehicle With the Plate Number {0}

Owner's Name:.................{1}
Owner's Phone Number:.........{2}
Vehicle Status In Garage:.....{3}
Vehicle Model:................{4}", plateNumber, GM.GetOwnerName(plateNumber), GM.GetOwnerPhoneNumber(plateNumber), GM.GetCarState(plateNumber), GM.GetVehicleModel(plateNumber));

            VehicleWheels = GM.GetListOfWheels(plateNumber);

            for (int i = 0; i < VehicleWheels.Count; i++)
            {
                System.Console.WriteLine("Wheel {0}:......................Manufacturer: {1}, Pressure: {2}", i, VehicleWheels[i].NameOfManufacturer, VehicleWheels[i].CurrentAirPressure);

            }

            if (GM.IsVehicleRunOnFuel(plateNumber))
            {
                System.Console.WriteLine(@"Fuel Pressent:................{0}%
Fuel Kind:....................{1}", GM.GetEnergyPressent(plateNumber), GM.GetFuelKind(plateNumber));
            }

            else
            {
                System.Console.WriteLine("Battery Pressent:.............{0}%", GM.GetEnergyPressent(plateNumber));
            }

            object[] VehicleFeatures = GM.GetVehicleFeatures(plateNumber);
            eKindsOfVehicles typeOfVehicle = GM.GetTypeOfVehicle(plateNumber);

            if (typeOfVehicle == eKindsOfVehicles.Car)
            {
                System.Console.WriteLine(@"Car Color:....................{0} 
Car Number Of Doors:..........{1}", VehicleFeatures[0], VehicleFeatures[1]);
            }

            else if (typeOfVehicle == eKindsOfVehicles.MotorCycle)
            {
                System.Console.WriteLine(@"MotorCycle Engine Valume:.....{0}
MotorCycle Licenes:...........{1}", VehicleFeatures[0], VehicleFeatures[1]);
            }

            else
            {
                if ((bool)VehicleFeatures[0] == false)
                {
                    VehicleFeatures[0] = "No";
                }

                else
                {
                    VehicleFeatures[0] = "Yes";
                }

                System.Console.WriteLine(@"Truck Have A Freige:..........{0}, 
Capacity Of Storage Licenes:..{1}", VehicleFeatures[0], VehicleFeatures[1]);
            }
        }
    }
}


