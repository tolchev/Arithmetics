using System;

[Flags]
public enum ArithmeticTypes
{
    Unknown = 0,
    Addition = 1,
    Subtraction = 2,
    Multiplication = 4,
    Division = 8
}