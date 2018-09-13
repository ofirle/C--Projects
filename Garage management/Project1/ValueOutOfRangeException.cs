using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public float MaxValue
        {
            get { return m_MaxValue; }
        }

        public float MinValue
        {
            get { return m_MinValue; }
        }

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue, string i_input) :
        base(string.Format("{0} Is Out of Range, Please Try Again: [{1}-{2}]", i_input, i_MinValue, i_MaxValue))
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }

        public ValueOutOfRangeException(string i_Input) :
        base(string.Format("Invalid Operation! You're Try to {0} Over The Maximum Capacity.", i_Input))
        {
        }
    }
}

