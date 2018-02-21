﻿namespace MyLogAn.UnitTests
{
    public class FakeLogger : ILogger
    {
        public string LastError;

        public void LogError(string message)
        {
            LastError = message;
        }
    }
}