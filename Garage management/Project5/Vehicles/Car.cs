using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        eCarColor m_Color;
        int m_DoorsNumber;
        EnergySource m_EnergySource;

        public Car(string i_model, string i_plateNumber, eCarColor i_carColor, int i_carDoors, float i_CurrentCapacityEnergy, ref List<Wheel> io_Wheels, bool i_IsElectronic) : base(i_model, i_plateNumber, ref io_Wheels)
        {
            m_Color = i_carColor;
            m_DoorsNumber = i_carDoors;
            if (i_IsElectronic)
            {
                m_EnergySource = new Electronic(3.2f, i_CurrentCapacityEnergy);
            }
            else
            {
                m_EnergySource = new Fuel(eFuelKinds.Octan98, 45f, i_CurrentCapacityEnergy);
            }
            
        }

        public override object[] GetVehicleFeature()
        {
            object[] CarFeatures= new object[2];

            CarFeatures[0] = m_Color;
            CarFeatures[1] = m_DoorsNumber;

            return CarFeatures;
        }

        public override eKindsOfVehicles GetKindOfVehicle()
        {
            eKindsOfVehicles KindsOfVehicles = eKindsOfVehicles.ElectronicCar;

            if (m_EnergySource is Fuel)
            {
                KindsOfVehicles = eKindsOfVehicles.Car;
            }
                return KindsOfVehicles;            
        }

        public override float GetEnergyPressent()
        {
           return m_EnergySource.GetEnergyPressent();
        }

        public override bool IsElectronic()
        {
            bool isElectronic = false;

            if(m_EnergySource is Electronic)
            {
                isElectronic = true;
            }

            return isElectronic;
        }

        public override bool PossiableToFill(float i_AmountToFill)
        {
            return m_EnergySource.PossiableToFill(i_AmountToFill);
        }

        public override eFuelKinds GetFuelKind()
        {
                return m_EnergySource.GetFuelKind();
        }
    }
}
