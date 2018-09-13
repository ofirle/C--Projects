using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageManeger
    {
        public static class ConstantsEnergyMaxCapacity
        {
            public const float ElectronicCar = 3.2f;
            public const float ElectronicMotorCycle = 1.8f;
            public const float FuelCar = 45f;
            public const float FuelMotorCycle = 6f;
            public const float FuelTruck = 115f;
        }
        public static class ConstantsWeelsMaxPressures
        {
            public const float Car = 32f;
            public const float MotorCycle = 30f;
            public const float Truck = 28f;
        }
        public static class ConstantsNumberOfWeels
        {
            public const int Car = 4;
            public const int MotorCycle = 2;
            public const int Truck = 12;
        }

        Dictionary<string, VehicleDetails> m_DictionaryVehicles = new Dictionary<string, VehicleDetails>();

        public void CreateCar(object[] GeneralParamsOfVehicle, eCarColor i_CarColor, int i_CarDoors, ref List<Wheel> i_Wheels, bool i_IsElectronic)
        {
            Vehicle newVehicle;

            newVehicle = new Car((string)GeneralParamsOfVehicle[4], (string)GeneralParamsOfVehicle[0], i_CarColor, i_CarDoors, (float)GeneralParamsOfVehicle[5], ref i_Wheels, i_IsElectronic);
            m_DictionaryVehicles.Add((string)GeneralParamsOfVehicle[0], new VehicleDetails((string)GeneralParamsOfVehicle[1], (string)GeneralParamsOfVehicle[2], ref newVehicle));
        }

        public void CreateMotorCycle(object[] GeneralParamsOfVehicle, eMotorCycleLicences i_MotorCycleLicence, int i_EngineValume,ref  List<Wheel> i_Wheels ,bool i_IsElectronic)
        {
            Vehicle newVehicle;

            newVehicle = new MotorCycle((string)GeneralParamsOfVehicle[4], (string)GeneralParamsOfVehicle[0], i_EngineValume, i_MotorCycleLicence, (float)GeneralParamsOfVehicle[5], ref i_Wheels, i_IsElectronic);
            m_DictionaryVehicles.Add((string)GeneralParamsOfVehicle[0], new VehicleDetails((string)GeneralParamsOfVehicle[1], (string)GeneralParamsOfVehicle[2], ref newVehicle));
        }

        public void CreateTruck(object[] GeneralParamsOfVehicle, bool i_IsFrige, float i_StorageCapacity,ref List<Wheel> i_Wheels)
        {
            Vehicle newVehicle;

            newVehicle = new Truck((string)GeneralParamsOfVehicle[4], (string)GeneralParamsOfVehicle[0], i_IsFrige, i_StorageCapacity, (float)GeneralParamsOfVehicle[5], ref i_Wheels);
            m_DictionaryVehicles.Add((string)GeneralParamsOfVehicle[0], new VehicleDetails((string)GeneralParamsOfVehicle[1], (string)GeneralParamsOfVehicle[2], ref newVehicle));
        }

        public void CreatePlatesNumberByStatus(eCarState eVehicleStatus, ref List<string> io_ListOfPlateNumbers)//function 2. create List Of All the Relevant Plates Numbers by Status Of Vehicle
        {
            foreach (string plateNumber in m_DictionaryVehicles.Keys)
            {
                if (m_DictionaryVehicles[plateNumber].CarState == eVehicleStatus)
                {
                    io_ListOfPlateNumbers.Add(plateNumber);
                }
            }
        }

        public void CreateListOfPlatesNumbers(ref List<string> io_ListOfPlateNumbers) //function 2. create List Of All Plates Numbers
        { 
            foreach (string plateNumber in m_DictionaryVehicles.Keys)
            {
                io_ListOfPlateNumbers.Add(plateNumber);
            }
        }

        public bool checkIfPlateNumberExist(string i_PlateNumber) //return true if the plate is already in the system
        {
            bool plateIsExist = false;

            if(m_DictionaryVehicles.ContainsKey(i_PlateNumber))
            {
                VehicleDetails vehicleDetails = m_DictionaryVehicles[i_PlateNumber];
                plateIsExist = true;
            }

            return plateIsExist;
        }

        public void ChangeStatusOfVehicleByPlateNumber(string i_PlateNumber, eCarState i_Status) //change the status of the vehicle by the new status. function 3
        {
            m_DictionaryVehicles[i_PlateNumber].CarState = i_Status;
        }

        public void InflateWheelsToMax(string i_PlateNumber) //inflate Tires In the Vehicle. function 4
        {
            m_DictionaryVehicles[i_PlateNumber].InflateWheelsToMax();
        }

        public bool IsVehicleRunOnFuel(string i_PlateNumber) //bool function check if car have fuel engine
        {
            return m_DictionaryVehicles[i_PlateNumber].IsRunOnFuel();
        }

        public bool PossiableToFill(string i_PlateNumber ,float i_AmountToFuel)
        {
            return m_DictionaryVehicles[i_PlateNumber].PossiableToFill(i_AmountToFuel);
        }

        public bool RefualVehicle(string i_PlateNumber, eFuelKinds fuelKind, float i_AmountToFuel)
        {
            return m_DictionaryVehicles[i_PlateNumber].PossiableToFill(i_AmountToFuel);
        }

        public bool RechargeVehicle(string i_PlateNumber, float i_AmountToBattery)
        {
            return m_DictionaryVehicles[i_PlateNumber].PossiableToFill(i_AmountToBattery);
        }

        public eFuelKinds GetFuelKind(string i_PlateNumber)
        {
            return m_DictionaryVehicles[i_PlateNumber].GetFuelKind();
        }

        public string GetOwnerName(string i_PlateNumber)
        {
            return m_DictionaryVehicles[i_PlateNumber].OwnerName;
        }

        public string GetOwnerPhoneNumber(string i_PlateNumber)
        {
            return m_DictionaryVehicles[i_PlateNumber].OwnerPhoneNumber;
        }

        public eCarState GetCarState(string i_PlateNumber)
        {
            return m_DictionaryVehicles[i_PlateNumber].CarState;
        }

        public string GetVehicleModel(string i_PlateNumber)
        {
            return m_DictionaryVehicles[i_PlateNumber].GetVehicleModel();
        }

        public List<Wheel> GetListOfWheels(string i_PlateNumber)
        {
            return m_DictionaryVehicles[i_PlateNumber].GetListOfWheels();
        }

        public float GetEnergyPressent(string i_PlateNumber)
        {
            return m_DictionaryVehicles[i_PlateNumber].GetEnergyPressent();
        }

        public object[] GetVehicleFeatures(string i_PlateNumber)
        {
            return m_DictionaryVehicles[i_PlateNumber].GetVehicleFeatures();
        }

        public eKindsOfVehicles GetTypeOfVehicle(string i_PlateNumber)
        {
            return m_DictionaryVehicles[i_PlateNumber].GetTypeOfVehicle();
        }

        public float GetMaxCapacityForVehicle(eKindsOfVehicles i_eKindOfVehicle)
        {
            if(i_eKindOfVehicle == eKindsOfVehicles.Car)
            {
                return ConstantsEnergyMaxCapacity.FuelCar;
            }
            else if (i_eKindOfVehicle == eKindsOfVehicles.ElectronicCar)
            {
                return ConstantsEnergyMaxCapacity.ElectronicCar;
            }
            else if (i_eKindOfVehicle == eKindsOfVehicles.MotorCycle)
            {
                return ConstantsEnergyMaxCapacity.FuelMotorCycle;
            }
            else if (i_eKindOfVehicle == eKindsOfVehicles.ElectronicMotorCycle)
            {
                return ConstantsEnergyMaxCapacity.ElectronicMotorCycle;
            }
            else
            {
                return ConstantsEnergyMaxCapacity.FuelTruck;
            }
        }

        public eKindsOfVehicles GetKindOfVehicle(string i_PlateNumber)
        {
            return m_DictionaryVehicles[i_PlateNumber].GetKindOfVehicle();
        }
    }
}
