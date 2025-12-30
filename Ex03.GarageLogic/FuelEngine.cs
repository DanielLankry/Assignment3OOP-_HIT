using System;

namespace Ex03.GarageLogic
{
   
    public class FuelEngine : Engine
    {
        readonly eFuelType r_FuelType;
        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        // public FuelEngine(eFuelType i_FuelType) // Complete after video watching
    }
}
