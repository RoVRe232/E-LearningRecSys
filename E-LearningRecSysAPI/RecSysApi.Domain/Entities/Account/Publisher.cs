﻿using System;

namespace RecSysApi.Domain.Entities.Account
{
    public class Publisher
    {
        public Guid PublisherID { get; set; }
        public Guid UserID { get; set; }

        public virtual User User { get; set; }
    }
}
