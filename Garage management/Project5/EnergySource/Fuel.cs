using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Fuel : EnergySource
    {
        eFuelKinds m_KindOfFuel;

        public Fuel(eFuelKinds i_KindOfFuel, float i_MaxCapacityFuel, float i_CurrentCapacityFuel) : base(i_MaxCapacityFuel,i_CurrentCapacityFuel)
        {
            m_KindOfFuel = i_KindOfFuel;
        }

        public override eFuelKinds GetFuelKind()
        {
            return m_KindOfFuel;
        }
    }
}
