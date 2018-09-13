using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        // $G$ DSN-999 (-3) Max fields should be readonly.
        // $G$ NTT-999 (-3) Missing access modifier
        string m_NameOfManufacturer;
        float m_CurrentAirPressure;
        float m_MaxAirPressure;

        public Wheel(string i_NameOfManufacturer,float i_CurrentAirPressure,float i_MaxAirPressure)
        {
            m_NameOfManufacturer = i_NameOfManufacturer;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public string NameOfManufacturer
        {
            get { return m_NameOfManufacturer; }
        }

        // $G$ DSN-999 (-5) You should have use "ValueOutOfRangeException" here.
        // as instructed in the exercise , the end of page 1 
        public void InflateWheelToMax() //inflate wheel to max air pressure- part of function 4
        {
            m_CurrentAirPressure = m_MaxAirPressure;
        }

        public void ToInflate(float i_Amount) //to inflate a tire by how much is needed. the func should inflate the tire and check that the airpressure isnt over the max.
        {
            if (m_CurrentAirPressure + i_Amount <= m_MaxAirPressure)
            {
                m_CurrentAirPressure += i_Amount;
            }
        }
    }
}
