using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Electronic : EnergySource
    {

        public Electronic(float i_MaxCapacityBattery, float i_CurrenCapacityBattery) :base(i_MaxCapacityBattery, i_CurrenCapacityBattery)
        {

        }

        // $G$ NTT-999 (-3) What is 0?
        public override eFuelKinds GetFuelKind()
        {
            return 0;
        }
    }
}
