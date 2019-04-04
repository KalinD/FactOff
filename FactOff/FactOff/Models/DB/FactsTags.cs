﻿using System;

namespace FactOff.Models.DB
{
    public class FactsTags
    {
        public Guid FactId { get; set; }
        public Fact Fact { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }

        public FactsTags()
        {
        }
    }
}
