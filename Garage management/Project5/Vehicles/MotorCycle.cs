using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class MotorCycle : Vehicle
    {
        int m_EngineValume;
        eMotorCycleLicences m_KingOfLicence;
        EnergySource m_EnergySource;

        public MotorCycle(string i_Model, string i_PlateNumber,int i_EngineValume,eMotorCycleLicences i_MotorCycleLicences, float i_CurrentCapacityEnergy,ref  List<Wheel> io_Wheels, bool i_IsElectronic) : base(i_Model, i_PlateNumber, ref io_Wheels)
        {
            m_EngineValume = i_EngineValume;
            m_KingOfLicence = i_MotorCycleLicences;

            if (i_IsElectronic)
            {
                m_EnergySource = new Electronic(1.8f, i_CurrentCapacityEnergy);
            }

            else
            {
                m_EnergySource = new Fuel(eFuelKinds.Octan96, 6f, i_CurrentCapacityEnergy);
            }
        }

        public override object[] GetVehicleFeature()
        {
            object[] MotorCycleFeatures = new object[2];

            MotorCycleFeatures[0] = m_EngineValume;
            MotorCycleFeatures[1] = m_KingOfLicence;

            return MotorCycleFeatures;
        }


        public override eKindsOfVehicles GetKindOfVehicle()
        {
            eKindsOfVehicles KindsOfVehicles = eKindsOfVehicles.ElectronicMotorCycle;

            if (m_EnergySource is Fuel)
            {
                KindsOfVehicles = eKindsOfVehicles.MotorCycle;
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

            if (m_EnergySource is Electronic)
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
