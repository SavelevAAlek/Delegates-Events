﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates_Events.Infrastructure
{
    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException(string message) : base(message) { }
    }
}
