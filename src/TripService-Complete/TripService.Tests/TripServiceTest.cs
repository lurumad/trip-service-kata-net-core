using ContosoTrips.Exceptions;
using ContosoTrips.Trips;
using ContosoTrips.Users;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;
using static FluentAssertions.FluentActions;

namespace TripServiceTests
{
    public class trip_service_should
    {
        private int ZERO_TRIPS = 0;
        private static User loggedInUser;
        private static User GUEST = null;
        private static User REGISTER_USER = new User();
        private static User UNUSED_USER = new User();
        private static User ANOTHER_USER = new User();
        private static Trip TO_ZARAGOZA = new Trip();
        private static Trip TO_BILBAO = new Trip();
        private TripService productionTripService;
        private IUserSession userSession;
        private ITripDAO tripDAO;
        public trip_service_should()
        {
            userSession = Substitute.For<IUserSession>();
            tripDAO = Substitute.For<ITripDAO>();
            productionTripService = new TripService(userSession, tripDAO);
        }

        [Fact]
        public void not_allow_to_get_trips_when_user_is_not_logged_in()
        {
            loggedInUser = GUEST;
            userSession.GetLoggedUser().Returns(loggedInUser);
            Invoking(() => productionTripService.GetTripsByUser(UNUSED_USER)).Should().Throw<UserNotLoggedInException>();
        }

        [Fact]
        public void not_allow_to_get_back_trips_when_users_are_not_friends()
        {
            loggedInUser = REGISTER_USER;
            var notFriend = Builder.User
                .WithFriends(ANOTHER_USER)
                .WithTrips(TO_ZARAGOZA, TO_BILBAO)
                .Build();
            userSession.GetLoggedUser().Returns(loggedInUser);

            var trips = productionTripService.GetTripsByUser(notFriend);

            trips.Should().HaveCount(ZERO_TRIPS);
        }

        [Fact]
        private void allow_to_get_back_trips_when_users_are_friends()
        {
            loggedInUser = REGISTER_USER;
            var friend = Builder.User
                .WithFriends(ANOTHER_USER, loggedInUser)
                .WithTrips(TO_ZARAGOZA, TO_BILBAO)
                .Build();
            userSession.GetLoggedUser().Returns(loggedInUser);
            tripDAO.GetTripsBy(friend).Returns(friend.Trips());

            var trips = productionTripService.GetTripsByUser(friend);

            trips.Should().HaveCount(friend.Trips().Count);
        }
    }
}
