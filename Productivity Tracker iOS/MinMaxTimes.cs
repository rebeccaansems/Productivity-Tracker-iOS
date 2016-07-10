using System;
using SQLite;

public class MinMaxTimes
{
    [PrimaryKey, AutoIncrement]
    public int DataNum { get; set; }

    public int MinHour { get; set; }
    public int MaxHour { get; set; }
    public int MinMinute { get; set; }
    public int MaxMinute { get; set; }
}