using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        bool m_IsFregide;
        float m_CapacityOfStorage;
        EnergySource m_EnergySource;

        public Truck(string i_model, string i_plateNumber, bool i_IsFregide, float i_CapacityOfStorage, float i_CurrentCapacityEnergy, ref List<Wheel> io_Wheels) : base(i_model, i_plateNumber, ref io_Wheels)
        {
            m_IsFregide = i_IsFregide;
            m_CapacityOfStorage = i_CapacityOfStorage;
            m_EnergySource = new Fuel(eFuelKinds.Soler, 115f, i_CurrentCapacityEnergy);
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

        public override eKindsOfVehicles GetKindOfVehicle()
        {
            return eKindsOfVehicles.Truck;
        }

        public override object[] GetVehicleFeature()
        {
            object[] TruckFeatures = new object[2];

            TruckFeatures[0] = m_IsFregide;
            TruckFeatures[1] = m_CapacityOfStorage;

            return TruckFeatures;
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
