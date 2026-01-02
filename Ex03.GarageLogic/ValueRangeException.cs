using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueRangeException : Exception
    {
        public float m_MinValue;
        public float m_MaxValue;

        public float MinValue
        {
            get
            {
                return m_MinValue;
            }
        }

        public float MaxValue
        {
            get
            {
                return m_MaxValue;
            }
        }

        public ValueRangeException(float i_MinValue, float i_MaxValue,string i_Msg) : base(string.Format("{0} (Range: {1} - {2})", i_Msg, i_MinValue, i_MaxValue))
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }

        }
    }

