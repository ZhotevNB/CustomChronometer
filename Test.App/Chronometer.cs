
using Chronometer.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Chronometer

{
    public class Chronometer : IChronometer
    {
        private List<string> LapsRecord = new List<string>();
        public Chronometer()
        {
            this.Laps = new List<string>();
            this.totalTime = new Stopwatch();
            this.lapTime = new Stopwatch();
        }

        Stopwatch totalTime { get; set; }

        Stopwatch lapTime { get; set; }

        public string GetTime =>totalTime.Elapsed.ToString(@"mm\:ss\.ffff");
        

        public List<string> Laps { get; set; }

        public string Lap()
        {
            var lap = lapTime.Elapsed;
            lapTime.Restart();
           
            var lapString = lap.ToString(@"mm\:ss\.ffff");
            Laps.Add(lapString);
            return lapString;
        }
        //lap.ToString("mm:ss.f");
        public void Reset()
        {
            totalTime.Restart();
            lapTime.Restart();
            Laps.Clear();
        }

        public void Start()
        {
            totalTime.Start();
            lapTime.Start();
        }

        public void Stop()
        {
            totalTime.Stop();
            lapTime.Stop();
        }
    }
}
