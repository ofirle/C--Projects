using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    abstract public class EnergySource
    {
        float m_CurrentCapacityOfEnergy;
        float m_MaxCapacityOfEnergy;

        public EnergySource(float i_MaxCapacityOfEnergy,float i_CurrentCapacityEnergy)
        {
            m_MaxCapacityOfEnergy = i_MaxCapacityOfEnergy;
            m_CurrentCapacityOfEnergy = i_CurrentCapacityEnergy;
        }

        // $G$ DSN-999 (-5) The "tank refill" method should have used the "ValueOutOfRangeException".
        internal bool PossiableToFill(float i_AmountToFill)
        {
            bool isPossiableToFill = false;

            if (m_CurrentCapacityOfEnergy + i_AmountToFill <= m_MaxCapacityOfEnergy)
            {
                m_CurrentCapacityOfEnergy += i_AmountToFill;
                isPossiableToFill = true;
            }
        
            return isPossiableToFill;
        }

        public float GetEnergyPressent()
        {
            return (m_CurrentCapacityOfEnergy / m_MaxCapacityOfEnergy) * 100;
        }

        abstract public eFuelKinds GetFuelKind();
    }
}