﻿using ModularRestaurant.Shared.Domain.Common;

namespace ModularRestaurant.Ratings.Domain.Rules
{
    public class CanReplyToUserRatingOnceRule : IBusinessRule
    {
        public string Message => "Cannot reply to user rating more than once.";

        private readonly string _existingReply;

        public CanReplyToUserRatingOnceRule(string existingReply)
        {
            _existingReply = existingReply;
        }

        public bool IsBroken() => _existingReply is not null;
    }
}