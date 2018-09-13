using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class VehicleDetails
    {
        string m_OwnerName;
        string m_OwnerPhoneNumber;
        eCarState m_CarState;
        Vehicle m_Vehicle;

        public VehicleDetails(string i_OwnerName, string i_OwnerPhoneNumber, ref Vehicle io_Vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_Vehicle = io_Vehicle;
            m_CarState = eCarState.InProcess;
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
        }

        public string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
        }

        public eCarState CarState
        {
            get { return m_CarState; }
            set { m_CarState = value; }
        }

        public string GetVehicleModel()
        {
            return m_Vehicle.Model;
        }

        public List<Wheel> GetListOfWheels()
        {
            return m_Vehicle.Wheels;
        }

        public void InflateWheelsToMax()
        {
            m_Vehicle.InflateWheelsToMax();
        }

        public bool IsRunOnFuel()//bool function check if car have fuel engine
        {
            return !m_Vehicle.IsElectronic();
        }

        internal bool PossiableToFill(float i_AmountToFill)
        {
            return m_Vehicle.PossiableToFill(i_AmountToFill);
        }

        internal eFuelKinds GetFuelKind()
        {
            return m_Vehicle.GetFuelKind();
        }
        public float GetEnergyPressent()
        {
            return m_Vehicle.GetEnergyPressent();
        }
        public object[] GetVehicleFeatures()
        {
            return m_Vehicle.GetVehicleFeature();
        }

        internal eKindsOfVehicles GetTypeOfVehicle()
        {
            if (m_Vehicle is Car)
            {
                return eKindsOfVehicles.Car;
            }
            else if (m_Vehicle is MotorCycle)
            {
                return eKindsOfVehicles.MotorCycle;
            }
            else
            {
                return eKindsOfVehicles.Truck;
            }

        }

        internal eKindsOfVehicles GetKindOfVehicle()
        {
            return m_Vehicle.GetKindOfVehicle();
        }
    }

    public enum eKindsOfVehicles
    {
        Car = 1,
        ElectronicCar,
        MotorCycle,
        ElectronicMotorCycle,
        Truck
    }
    public enum eCarState
    {
        InProcess = 1,
        Repaired,
        Paid
    }
    public enum eCarColor
    {
        Grey = 1,
        Blue,
        White,
        Black
    }
    public enum eMotorCycleLicences
    {
        A = 1,
        A1,
        B1,
        B2
    }
    public enum eFuelKinds
    {
        Octan95 = 1,
        Octan96,
        Octan98,
        Soler
    }
}
