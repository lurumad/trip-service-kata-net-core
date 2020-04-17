using ContosoTrips.Trips;
using ContosoTrips.Users;

namespace TripServiceTests
{
    public class UserBuilder
    {
        private User[] friends = new User[] { };
        private Trip[] trips = new Trip[] { };

        public UserBuilder WithFriends(params User [] friends)
        {
            this.friends = friends;
            return this;
        }

        public UserBuilder WithTrips(params Trip [] trips)
        {
            this.trips = trips;
            return this;
        }

        public User Build()
        {
            var user = new User();
            AddFriendsTo(user);
            AddTripsTo(user);
            return user;
        }

        private void AddTripsTo(User user)
        {
            foreach (var trip in trips)
            {
                user.AddTrip(trip);
            }
        }

        private void AddFriendsTo(User user)
        {
            foreach (var friend in friends)
            {
                user.AddFriend(friend);
            }
        }
    }
}
