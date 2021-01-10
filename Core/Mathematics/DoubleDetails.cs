﻿namespace Core.Mathematics
{
    public struct DoubleDetails
    {
        public double Value { get; }
        public int IntegralPart { get; }
        public double FractionPart { get; }

        public DoubleDetails(double value)
        {
            Value = value;
            IntegralPart = (int) value;
            FractionPart = value % 1;
        }
    }
}
