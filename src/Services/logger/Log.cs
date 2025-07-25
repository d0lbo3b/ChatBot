﻿namespace ChatBot.Services.logger;

public enum LogLevel {
    Info,
    Error,
    Warning
}

public class Log {
    public LogLevel Level { get; }
    public DateTime Time { get; }
    public string Message { get; }
    

    public Log(LogLevel level, DateTime time, string message) {
        Level = level;
        Time = time;
        Message = message;
    }
}