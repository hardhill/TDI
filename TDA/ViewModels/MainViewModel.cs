using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDA.ViewModels
{
    public partial class MainViewModel:ObservableObject
    {
        public MainViewModel()
        {
            MinCycles = 10;
            Cycles = 20;
            MaxCycles = 200;
            MinBreathSec = 1;
            BreathSec = 3;
            MaxBreathSec = 6;
            MinExhaleSec = 10;
            MaxExhaleSec = 180;

        }
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(RemainedTime))]
        private int cycles;

        [ObservableProperty]
        private int minCycles;

        [ObservableProperty]
        private int maxCycles;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(RemainedTime))]
        private int breathSec;

        [ObservableProperty]
        private int minBreathSec;

        [ObservableProperty]
        private int maxBreathSec;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(RemainedTime))]
        private int exhaleSec;

        [ObservableProperty]
        private int minExhaleSec;

        [ObservableProperty]
        private int maxExhaleSec;

        
        public int RemainedTime { 
            get { 

                return Cycles * (BreathSec+ExhaleSec); 
            } 
        }
    }
}
