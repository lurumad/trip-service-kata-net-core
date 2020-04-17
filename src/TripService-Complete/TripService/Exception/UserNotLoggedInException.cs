using System;

namespace ContosoTrips.Exceptions
{
    [Serializable]
    public class UserNotLoggedInException : System.Exception
    {
        public UserNotLoggedInException(string message) : base(message)
        {

        }
    }
}
