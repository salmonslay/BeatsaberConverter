﻿using System.Globalization;

namespace BeatsaberConverter.Osu
{
    internal class TimingPoint
    {
        public int Time { get; set; }

        /// <summary>
        /// For uninherited timing points, the duration of a beat, in milliseconds.
        /// For inherited timing points, a negative inverse slider velocity multiplier, as a percentage.For example, -50 would make all sliders in this timing section twice as fast as SliderMultiplier.
        /// src: https://osu.ppy.sh/wiki/en/osu%21_File_Formats/Osu_%28file_format%29#timing-points
        /// </summary>
        public double BeatLength { get; set; }

        /// <summary>
        /// The duration of a beat, in milliseconds.
        /// </summary>
        public double ActualBeatLength { get; set; }

        /// <summary>
        /// The inherited beath length this timing point is based on.
        /// </summary>
        public double InheritedBeatLength { get; set; }

        public int Meter { get; set; }

        public SampleSet SampleSet { get; set; }

        public int SampleIndex { get; set; }

        public int Volume { get; set; }

        public bool Uninherited { get; set; }

        public bool Kiai { get; set; }

        public TimingPoint(string line, TimingPoint? previousTimingPoint)
        {
            string[] data = line.Split(",");

            Time = Convert.ToInt32(data[0]);
            BeatLength = Convert.ToDouble(data[1], CultureInfo.InvariantCulture);
            Meter = Convert.ToInt32(data[2]);
            SampleSet = (SampleSet)Convert.ToInt32(data[3]);
            SampleIndex = Convert.ToInt32(data[4]);
            Volume = Convert.ToInt32(data[5]);
            Uninherited = data[6] == "1";
            Kiai = data[7] == "1";

            if (Uninherited)
            {
                ActualBeatLength = BeatLength;
                InheritedBeatLength = BeatLength;
            }
            else if (previousTimingPoint != null)
            {
                InheritedBeatLength = previousTimingPoint.InheritedBeatLength;
                ActualBeatLength = InheritedBeatLength / (-100 / BeatLength);
            }
        }
    }
}