using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class InputValidation
    {
        internal bool MenuValidInput(string i_Input, ref int io_InputMenuNumber)
        {
            bool isValid = false;

            try
            {
                io_InputMenuNumber = Int32.Parse(i_Input);
                if (io_InputMenuNumber >= 1 && io_InputMenuNumber <= 8)
                {
                    isValid = true;
                }
                else
                {
                    throw new ValueOutOfRangeException(8, 1, i_Input);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("{0} is Not an Integer. Please Try Again: ", i_Input);
            }

            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }

            return isValid;
        }

        internal bool ValidInputStatusVehicle(string i_StatusVehicle, ref GarageLogic.eCarState io_VehicleStatus)
        {
            bool isValid = false;

            try
            {
                if (i_StatusVehicle == "InProcess" || i_StatusVehicle == "1")
                {
                    io_VehicleStatus = GarageLogic.eCarState.InProcess;
                    isValid = true;
                }
                else if (i_StatusVehicle == "Repaired" || i_StatusVehicle == "2")
                {
                    io_VehicleStatus = GarageLogic.eCarState.Repaired;
                    isValid = true;
                }
                else if (i_StatusVehicle == "Paid" || i_StatusVehicle == "3")
                {
                    io_VehicleStatus = GarageLogic.eCarState.Paid;
                    isValid = true;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("{0} is Not an Options. Please Try Again: [InProcess(1) /Repaired(2) /Paid(3)]", i_StatusVehicle);
            }

            return isValid;
        }

        internal bool ValidInputKindOfVehicle(string i_InputKindOfVehicle, ref eKindsOfVehicles io_KindOfVehicle)
        {
            bool isValid = false;

            try
            {
                if (i_InputKindOfVehicle == "Car" || i_InputKindOfVehicle == "1")
                {
                    io_KindOfVehicle = GarageLogic.eKindsOfVehicles.Car;
                    isValid = true;
                }
                else if (i_InputKindOfVehicle == "Electronic Car" || i_InputKindOfVehicle == "2")
                {
                    io_KindOfVehicle = GarageLogic.eKindsOfVehicles.ElectronicCar;
                    isValid = true;
                }
                else if (i_InputKindOfVehicle == "MotorCycle" || i_InputKindOfVehicle == "3")
                {
                    io_KindOfVehicle = GarageLogic.eKindsOfVehicles.MotorCycle;
                    isValid = true;
                }
                else if (i_InputKindOfVehicle == "Electronic MotorCycle" || i_InputKindOfVehicle == "4")
                {
                    io_KindOfVehicle = GarageLogic.eKindsOfVehicles.ElectronicMotorCycle;
                    isValid = true;
                }
                else if (i_InputKindOfVehicle == "Truck" || i_InputKindOfVehicle == "5")
                {
                    io_KindOfVehicle = GarageLogic.eKindsOfVehicles.Truck;
                    isValid = true;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("{0} is Not an Options. Please Try Again: [Car(1) /Electronic Car(2) /MotorCycle(3) /Electronic MotorCycle(4) /Truck(5)]", i_InputKindOfVehicle);
            }

            return isValid;
        }

        internal bool ValidInputCurrentEnergyCapacity(string i_InputString, eKindsOfVehicles i_eKindOfVehicle, ref float io_CurrentEnergyCapacity, float i_MaxEnergyCapacity)
        {
            bool isValid = false;

            try
            {
                io_CurrentEnergyCapacity = float.Parse(i_InputString);
                if (io_CurrentEnergyCapacity >= 0 && io_CurrentEnergyCapacity <= i_MaxEnergyCapacity)
                {
                    isValid = true;
                }
                else
                {
                    throw new ValueOutOfRangeException(i_MaxEnergyCapacity, 0, i_InputString);
                }
            }

            catch (FormatException)
            {
                Console.WriteLine("{0} is Not an Float. Please Try Again: ", i_InputString);
            }

            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }

            return isValid;
        }

        internal bool ValidInputIsNotEmpty(string i_InputString)
        {
            bool isValid = true;

            try
            {
                if (i_InputString == string.Empty)
                {
                    isValid = false;
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Input Can't Be Empty. Please Try Again: ", i_InputString);
            }

            return isValid;
        }
    
        internal bool ValidInputCarColor(string i_InputString, ref eCarColor eCarColor)
        {
            bool isValid = false;

            try
            {
                if (i_InputString == "Grey" || i_InputString == "1")
                {
                    eCarColor = GarageLogic.eCarColor.Grey;
                    isValid = true;
                }
                else if (i_InputString == "Blue" || i_InputString == "2")
                {
                    eCarColor = GarageLogic.eCarColor.Blue;
                    isValid = true;
                }
                else if (i_InputString == "White" || i_InputString == "3")
                {
                    eCarColor = GarageLogic.eCarColor.White;
                    isValid = true;
                }
                else if (i_InputString == "Black" || i_InputString == "4")
                {
                    eCarColor = GarageLogic.eCarColor.Black;
                    isValid = true;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("{0} is Not an Options. Please Try Again: [Grey(1) /Blue(2) /White(3) /Black(4)]", i_InputString);
            }

            return isValid;
        }

        internal bool ValidInputCarDoors(string i_InputString, ref int io_CarDoors)
        {
            bool isValid = false;

            try
            {
                io_CarDoors = Int32.Parse(i_InputString);
                if(io_CarDoors >= 2 && io_CarDoors <= 5)
                {
                    isValid = true;
                }
                else
                {
                    throw new ValueOutOfRangeException(5, 2, i_InputString);
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("{0} is Not an Integer. Please Try Again: ", i_InputString);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }

            return isValid;
        }

        internal bool ValidInputLicenceMotorCycle(string i_InputString, ref eMotorCycleLicences motorCycleLicences)
        {
            bool isValid = false;

            try
            {
                if (i_InputString == "A" || i_InputString == "1")
                {
                    motorCycleLicences = GarageLogic.eMotorCycleLicences.A;
                    isValid = true;
                }
                else if (i_InputString == "A1" || i_InputString == "2")
                {
                    motorCycleLicences = GarageLogic.eMotorCycleLicences.A1;
                    isValid = true;
                }
                else if (i_InputString == "B1" || i_InputString == "3")
                {
                    motorCycleLicences = GarageLogic.eMotorCycleLicences.B1;
                    isValid = true;
                }
                else if (i_InputString == "B2" || i_InputString == "4")
                {
                    motorCycleLicences = GarageLogic.eMotorCycleLicences.B2;
                    isValid = true;
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            catch (ArgumentException)
            {
                Console.WriteLine("{0} is Not an Options. Please Try Again: [A(1) /A1(2) /B1(3) /B2(4)] ", i_InputString);
            }

            return isValid;
        }

        internal bool ValidInputYesOrNo(string i_InputString, ref bool io_YOrN)
        {
            bool isValid = false;

            try
            {
                if (i_InputString == "Y" || i_InputString == "y")
                {
                    io_YOrN = true;
                    isValid = true;
                }
                else if (i_InputString == "N" || i_InputString == "n")
                {
                    io_YOrN = false;
                    isValid = true;
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            catch (ArgumentException)
            {
                Console.WriteLine("Please Try Again: [Y/N] ", i_InputString);
            }

            return isValid;
        }

        internal bool ValidInputFloatNonNegative(string i_InputString, ref float io_FloatValue)
        {
            bool isValid = false;

            try
            {

                io_FloatValue = float.Parse(i_InputString);
                if (io_FloatValue >= 0)
                {
                    isValid = true;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch(FormatException)
            {
                Console.WriteLine("{0} is Not an Float. Please Try Again: ", i_InputString);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("You Can't Enter a Negative Input. Please Try Again: ", i_InputString);
            }

            return isValid;
        }

        internal bool ValidInputIntegerNonNegative(string i_InputString, ref int io_IntValue)
        {
            bool isValid = false;

            try
            {

                io_IntValue = Int32.Parse(i_InputString);
                if (io_IntValue >= 0)
                {
                    isValid = true;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("{0} is Not an Integer. Please Try Again: ", i_InputString);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("You Can't Enter a Negative Input. Please Try Again:  ", i_InputString);
            }

            return isValid;
        }


        internal bool ValidInputWheelPressure(string i_InputString, ref float wheelPressure, float i_MaxWheelPressure)
        {
            bool isValid = false;

            try
            {
                wheelPressure = float.Parse(i_InputString);
                if (wheelPressure >= 0 && wheelPressure <= i_MaxWheelPressure)
                {
                    isValid = true;
                }
                else
                {
                    throw new ValueOutOfRangeException(i_MaxWheelPressure, 0, i_InputString);
                }
            }

            catch (FormatException)
            {
                Console.WriteLine("{0} is Not an Integer. Please Try Again: ", i_InputString);
            }

            catch(ValueOutOfRangeException ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }

            return isValid;
        }

        internal bool ValidInputFuelKind(string i_InputString, ref eFuelKinds io_FuelKind)
        {
            bool isValid = false;

            try
            {
                if (i_InputString == "Octan95" || i_InputString == "1")
                {
                    io_FuelKind = eFuelKinds.Octan95;
                    isValid = true;
                }
                else if (i_InputString == "Octan96" || i_InputString == "2")
                {
                    io_FuelKind = eFuelKinds.Octan96;
                    isValid = true;
                }
                else if (i_InputString == "Octan98" || i_InputString == "3")
                {
                    io_FuelKind = eFuelKinds.Octan98;
                    isValid = true;
                }
                else if (i_InputString == "Soler" || i_InputString == "4")
                {
                    io_FuelKind = eFuelKinds.Soler;
                    isValid = true;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("{0} is Not an Option. Please Try Again: [Octan95(1) /Octan96(2) /Octan 98(3) /Solar(4)]", i_InputString);
            }

            return isValid;
        }
    }
}

