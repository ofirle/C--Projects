using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        string m_Model;
        string m_PlateNumber;
        float m_EnergyPressent;
        List<Wheel> m_Wheels;
        EnergySource m_EnergySource;

        public Vehicle(string i_model,string i_plateNumber,ref List<Wheel>io_Wheels)
        {
            m_Model = i_model;
            m_PlateNumber = i_plateNumber;
            m_Wheels = io_Wheels;
        }

        public void InflateWheelsToMax()//inflate each wheel to the max
        {
            for(int i=0; i<m_Wheels.Count;i++)
            {
                m_Wheels[i].InflateWheelToMax();
            }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
        }

        public string Model
        {
            get { return m_Model;}
        }

        abstract public bool IsElectronic();
        abstract public bool PossiableToFill(float i_AmountToFill);
        abstract public eFuelKinds GetFuelKind();
        abstract public float GetEnergyPressent();
        abstract public object[] GetVehicleFeature();
        abstract public eKindsOfVehicles GetKindOfVehicle();
    }
}
