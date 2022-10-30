using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TDA.ViewModels
{
    
    public partial class MainViewModel: INotifyPropertyChanged
    {
        private int _remainedTime;
        private TimeOnly _timer;
        private bool isRunning ;



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
            _timer = new TimeOnly();    
        }

        
        private int cycles;
        private int minCycles;
        private int maxCycles;
        private int breathSec;
        private int minBreathSec;
        private int maxBreathSec;
        private int exhaleSec;
        private int minExhaleSec;
        private int maxExhaleSec;

        public int MinCycles
        {
            get { return minCycles; }
            set { minCycles = value; }
        }
        public int MaxCycles
        {
            get { return maxCycles; }
            set { maxCycles = value; OnPropertyChanged(nameof(MaxCycles)); }
        }
        public int MinBreathSec
        {
            get { return minBreathSec; }
            set { minBreathSec = value; OnPropertyChanged(nameof(MinBreathSec)); }
        }
        public int MaxBreathSec
        {
            get { return maxBreathSec; }
            set { maxBreathSec = value; OnPropertyChanged(nameof(MaxBreathSec)); }
        }
        public int MinExhaleSec
        {
            get { return minExhaleSec; }
            set { minExhaleSec = value; OnPropertyChanged(nameof(MinExhaleSec)); }
        }
        public int MaxExhaleSec
        {
            get { return maxExhaleSec; }
            set { maxExhaleSec = value; OnPropertyChanged(nameof(MaxExhaleSec)); }
        }
        public int Cycles
        {
            get { return cycles; }
            set { cycles = value;
                SetRemainedTime();
                OnPropertyChanged(nameof(Cycles)); }
        }

        public int BreathSec
        {
            get { return breathSec; }
            set { breathSec = value;
                SetRemainedTime();
                OnPropertyChanged(nameof(BreathSec)); }
        }

        public int ExhaleSec
        {
            get { return exhaleSec; }
            set { exhaleSec = value;
                SetRemainedTime();
                OnPropertyChanged(nameof(ExhaleSec)); }
        }

        public int RemainedTime
        {
            get { return _remainedTime; }
            set { _remainedTime = value;
                OnPropertyChanged(nameof(RemainedTime)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void SetRemainedTime()
        {
            RemainedTime = cycles * (breathSec + exhaleSec);
        }
        

        [RelayCommand]
        public async void StartBreath()
        {
            isRunning = !isRunning;
            while (isRunning)
            {
                _timer.Add(TimeSpan.FromSeconds(1));
                CountDown();
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        [RelayCommand]
        public void ResetBreath()
        {
            _timer = new TimeOnly();
            
        }

        private void CountDown()
        {
            if (_remainedTime > 0)
            {
                _remainedTime--;
                OnPropertyChanged(nameof(RemainedTime));
            }
            else
            {

            }
        }
    }
}
