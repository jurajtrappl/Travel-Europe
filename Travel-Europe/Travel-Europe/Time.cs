using System;

namespace TravelEurope
{
    class Time
    {
        public int Hours { get; }
        public int Minutes { get; }

        public Time(double totalTime)
        {
            Hours = (int)Math.Floor(totalTime);
            Minutes = ComputeMinutes(totalTime - Hours);
        }

        int ComputeMinutes(double minutesPart)
        {
            return (int)Math.Round(minutesPart * 60);
        }
    }
}
