﻿using ModularRestaurant.Ratings.Domain.Events;
using ModularRestaurant.Ratings.Domain.Rules;
using ModularRestaurant.Shared.Domain.Common;
using ModularRestaurant.Shared.Domain.Types;
using System.Collections.Generic;
using System.Linq;

namespace ModularRestaurant.Ratings.Domain.Entities
{
    public class Restaurant : AggregateRoot<RestaurantId>
    {
        public IReadOnlyList<UserRating> UserRatings => _userRatings;
        private List<UserRating> _userRatings = new();
        private MenuId _menuId;


        private Restaurant()
        {
        }

        public Restaurant(RestaurantId id)
        {
            Id = id;
            Events.Add(new RestaurantCreatedEvent(id));
        }

        //TODO: Removed after add integration with restaurantModule
        public static Restaurant Create(RestaurantId id)
        {
            return new Restaurant(id);
        }

        public void AddUserRating(UserId userId, int ratingValue, string text)
        {
            CheckRule(new UserCanOnlyRateRestaurantOnceRule(userId, _userRatings));

            _userRatings.Add(UserRating.Create(userId, ratingValue, text));
        }

        public void AddReplyToUserRating(UserId userId, string text)
        {
            var userRating = UserRatings.SingleOrDefault(x => x.UserId == userId);
            
            //TODO: Consider use FindOrThrow, because this is not business rule at all
            CheckRule(new CanAddReplyOnlyToExistingUserRatingRule(userRating));
            
            userRating!.AddRestaurantReply(text);
        }

        public void AssignMenu(MenuId id)
        {
            if (_menuId == id)
                return;
            _menuId = id;
            Events.Add(new MenuChangedEvent(id));
        }
    }
}