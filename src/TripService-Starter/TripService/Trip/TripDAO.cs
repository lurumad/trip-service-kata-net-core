using System.Collections.Generic;
using ContosoTrips.Exceptions;
using ContosoTrips.Users;

namespace ContosoTrips.Trips
{
    public class TripDAO : ITripDAO
    {
        public static List<Trip> FindTripsByUser(User user)
        {
            throw new DependendClassCallDuringUnitTestException(
                        "TripDAO should not be invoked on an unit test.");
        }

        public List<Trip> GetTripsBy(User user)
        {
            return FindTripsByUser(user);
        }
    }
}
