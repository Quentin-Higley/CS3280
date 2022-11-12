using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class Flight
    {
        /// <summary>
        /// flight id
        /// </summary>
        private string flightId;
        /// <summary>
        /// flight number
        /// </summary>
        private string flightNumber;
        /// <summary>
        /// type of aircraft
        /// </summary>
        private string aircraftType;
        /// <summary>
        /// passengers on the flight
        /// </summary>
        private List<Passenger> passengerList;

        /// <summary>
        /// flight constructor
        /// </summary>
        /// <param name="flightId">flight id</param>
        /// <param name="flightNumber">flight number</param>
        /// <param name="aircraftType">type of aircraft</param>
        public Flight(string flightId, string flightNumber, string aircraftType)
        {
            this.flightId = flightId;
            this.flightNumber = flightNumber;
            this.aircraftType = aircraftType;
            passengerList = new List<Passenger>();
        }

        /// <summary>
        /// adds a single passenger to the flight
        /// </summary>
        public void addPassenger(Passenger p)
        {
            try
            {
                passengerList.Add(p);
                return;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// refreshes the passenger list
        /// </summary>
        /// <param name="passengers">list of passenger</param>
        /// <exception cref="Exception">exception</exception>
        public void addPassengers(List<Passenger> passengers)
        {
            try
            {
                passengerList.Clear();
                foreach (Passenger p in passengers)
                {
                    passengerList.Add(p);
                }
                return;
            }
            catch(Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// overrides the to string method
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            try
            {
                return $"{flightNumber} - {AircraftType}";
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// flight id getter
        /// </summary>
        public string FlightId { get { return flightId;  } }
        /// <summary>
        /// fligh number getter
        /// </summary>
        public string FlightNumber { get { return flightNumber; } }
        /// <summary>
        /// aircraft getter
        /// </summary>
        public string AircraftType { get { return aircraftType; } }
        /// <summary>
        /// passenger list getter
        /// </summary>
        public List<Passenger> PassengerList { get { return passengerList; } }
        
    }
}
