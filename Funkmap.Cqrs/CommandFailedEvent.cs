﻿
namespace Funkmap.Cqrs
{
    public class CommandFailedEvent
    {
        public string Error { get; set; }

        public string ExceptionMessage { get; set; }
    }
}
