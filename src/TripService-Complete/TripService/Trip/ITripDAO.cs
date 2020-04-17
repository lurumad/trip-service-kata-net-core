using ContosoTrips.Users;
using System.Collections.Generic;

namespace ContosoTrips.Trips
{
    public interface ITripDAO
    {
        List<Trip> GetTripsBy(User user);
    }
}