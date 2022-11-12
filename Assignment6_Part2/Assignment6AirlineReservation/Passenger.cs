using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class Passenger
    {
        /// <summary>
        /// passenger Id
        /// </summary>
        private string passengerId;
        /// <summary>
        /// passenger first name
        /// </summary>
        private string firstName;
        /// <summary>
        /// passenger lastname
        /// </summary>
        private string lastName;
        /// <summary>
        /// passenger seat number
        /// </summary>
        private string seatNumber;

        /// <summary>
        /// passenger constructor
        /// </summary>
        /// <param name="passengerId">passenger Id</param>
        /// <param name="firstName">passenger first name</param>
        /// <param name="lastName">passenger lastname</param>
        /// <param name="seatNumber">passenger seat number</param>
        public Passenger(string passengerId, string firstName, string lastName, string seatNumber)
        {
            try
            {
                this.passengerId = passengerId;
                this.firstName = firstName;
                this.lastName = lastName;
                this.seatNumber = seatNumber;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        /// <summary>
        /// overrides the ToString method
        /// </summary>
        /// <returns>string</returns>
        /// <exception cref="Exception">exception</exception>
        public override string ToString()
        {
            try
            {
                return $"{firstName} {LastName}";
            }
            catch(Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// passengerId getter
        /// </summary>
        public string PassengerId { get { return passengerId; } }

        /// <summary>
        /// first name getter
        /// </summary>
        public string FirstName { get { return firstName; } }

        /// <summary>
        /// last name getter
        /// </summary>
        public string LastName { get { return lastName; } }
        /// <summary>
        /// seat number getter
        /// </summary>
        public string SeatNumber { get { return seatNumber; } }
    }
}
