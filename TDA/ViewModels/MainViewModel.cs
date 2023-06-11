using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;
using Plugin.Maui.Audio;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TDA.Data;

namespace TDA.ViewModels
{

    public partial class MainViewModel : INotifyPropertyChanged
    {
        private int _remainedTime;
        private TimeOnly _timer;
        private bool isRunning;
        private int storeCycles;
        private int storeBreath;
        private int storeExhale;
        private bool willBeSaved;

        private int cycles;
        private int minCycles;
        private int maxCycles;
        private int breathSec;
        private int minBreathSec;
        private int maxBreathSec;
        private int exhaleSec;
        private int minExhaleSec;
        private int maxExhaleSec;
        private readonly IAudioManager audioManager;
        private Ellipse myEllipse;

        private int secondsOfBreath;
        private int secondsOfExhale;
        private VolumeName volumeName;
        private Volumes volumes;
        private StoreData storeData;



        public MainViewModel(IAudioManager audioManager, Ellipse ellipse)
        {
            this.audioManager = audioManager;
            storeData = LoadUserData();
            MinCycles = 1;
            Cycles = storeData.Cycles;
            MaxCycles = 200;

            MinBreathSec = 1;
            BreathSec = storeData.BreathSec;
            MaxBreathSec = 6;
            
            MinExhaleSec = 5;
            MaxExhaleSec = 180;
            ExhaleSec = storeData.ExhaleSec;
            IsRunning = false;
            willBeSaved = false;
            _timer = new TimeOnly();
            volumes = new Volumes();
            volumeName = volumes.GetVolume(storeData.VolumeId);
            myEllipse = ellipse;
            DeviceDisplay.Current.KeepScreenOn = true;
        }


        

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
            set
            {
                cycles = value;
                SetRemainedTime();
                OnPropertyChanged(nameof(Cycles));
            }
        }

        public int BreathSec
        {
            get { return breathSec; }
            set
            {
                breathSec = value;
                SetRemainedTime();
                OnPropertyChanged(nameof(BreathSec));
            }
        }

        public int ExhaleSec
        {
            get { return exhaleSec; }
            set
            {
                exhaleSec = value;
                SetRemainedTime();
                OnPropertyChanged(nameof(ExhaleSec));
            }
        }

        public int RemainedTime
        {
            get { return _remainedTime; }
            set
            {
                _remainedTime = value;
                OnPropertyChanged(nameof(RemainedTime));
            }
        }

        public int SecondsOfBreath
        {
            get => secondsOfBreath;
        }
        public int SecondsOfExhale
        {
            get => secondsOfExhale;
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }

        }

        public VolumeName CurrentVolume
        {
            get { return volumeName; }
            set { volumeName = value; OnPropertyChanged(nameof(CurrentVolume)); }
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
        private void ResetTimer()
        {
            Cycles = storeCycles;
            BreathSec = storeBreath;
            ExhaleSec = storeExhale;
            willBeSaved = false;
        }

        private void StoreUserData(StoreData storeData)
        {
            if (storeData == null) return;
            string pathData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (pathData == null) return;
            string fileName = System.IO.Path.Combine(pathData, "tdf.data");
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(storeData.Cycles);
                    bw.Write(storeData.BreathSec);
                    bw.Write(storeData.ExhaleSec);
                    bw.Write(storeData.VolumeId);
                    bw.Flush();
                    bw.Close();
                    fs.Close();
                }
            }
            catch (Exception)
            {

            }
        }

        private StoreData LoadUserData()
        {
            StoreData storeData = new StoreData(20,3,10,3);
            string pathData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (pathData != null)
            {
                string fileName = System.IO.Path.Combine(pathData, "tdf.data");
                if(File.Exists(fileName))
                {
                    try
                    {
                        using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            fs.Position = 0;
                            storeData.Cycles = br.ReadInt32();
                            storeData.BreathSec = br.ReadInt32();
                            storeData.ExhaleSec = br.ReadInt32();
                            storeData.VolumeId = br.ReadInt32();
                            fs.Close();
                        }
                    }
                    catch (Exception)
                    {

                        
                    }
                }
            }
            return storeData;
        }

        // start breath
        [RelayCommand]
        public async void StartBreath()
        {
            if (!willBeSaved)
            {
                storeCycles = Cycles;
                storeBreath = BreathSec;
                storeExhale = ExhaleSec;
                
                willBeSaved=true;
            }
            IsRunning = !IsRunning;
            var startPlayAudio = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("tibip.wav"));
            var startBreathAudio = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("breath.wav"));
            var startExhaleAudio = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("exhale.wav"));
            startPlayAudio.Volume = CurrentVolume.Volume;
            startBreathAudio.Volume = CurrentVolume.Volume;
            startExhaleAudio.Volume = CurrentVolume.Volume;
            try
            {
               
                if (!IsRunning)
                {
                    // pause and round/truncate timer
                    int fullCycles = RemainedTime / (BreathSec + ExhaleSec);
                    int leftOver = RemainedTime - fullCycles;
                    if (leftOver > (BreathSec + ExhaleSec) / 2.0)
                    {
                        RemainedTime = (fullCycles + 1) * (BreathSec + ExhaleSec);
                        Cycles = fullCycles + 1;
                    }
                    else
                    {
                        RemainedTime = (fullCycles) * (BreathSec + ExhaleSec);
                        Cycles = fullCycles;
                    }
                    myEllipse.CancelAnimations();
                    myEllipse.Scale = 0.1;
                }
                else
                {
                    // start program
                    StoreUserData(new StoreData(Cycles, BreathSec, ExhaleSec, CurrentVolume.Id));
                    startPlayAudio.Play();
                    await Task.Delay(TimeSpan.FromMilliseconds(1500));
                    // нажал кабан на баклажан
                    // нажалкаб ан набак лажан
                    

                }
                while (IsRunning)
                {
                    _timer.Add(TimeSpan.FromSeconds(1));
                    startBreathAudio.Volume = CurrentVolume.Volume;
                    startExhaleAudio.Volume = CurrentVolume.Volume;
                    CountDown(startBreathAudio, startExhaleAudio);
                    await Task.Delay(TimeSpan.FromMilliseconds(1000));
                }
            }
            finally
            {
                startPlayAudio.Dispose();
                startBreathAudio.Dispose();
                startExhaleAudio.Dispose();
            }
        }

        [RelayCommand]
        public void ResetBreath()
        {
            _timer = new TimeOnly();
            ResetTimer();
        }

        [RelayCommand]
        public void SetVolume()
        {
           CurrentVolume =  volumes.NextValume(CurrentVolume.Id);
            
        }
        private void CountDown(IAudioPlayer startBreathAudio,IAudioPlayer startExhaleAudio)
        {
            if (_remainedTime > 0)
            {
                int leftOver = RemainedTime % (BreathSec + ExhaleSec);
                
                if(leftOver == 0)
                {
                    // play start begin breath
                    Cycles = (RemainedTime / (BreathSec + ExhaleSec));
                    myEllipse.ScaleTo(1d, (uint)BreathSec * 1000);
                    startBreathAudio?.Play();
                    
                }
                int secondsOfCycle = (BreathSec + ExhaleSec) - leftOver;
                if (secondsOfCycle > BreathSec)
                {
                    // exhale stadia
                    secondsOfBreath = BreathSec;
                    secondsOfExhale = secondsOfCycle - BreathSec;
                }
                else
                {
                    // breath statia
                    secondsOfBreath = secondsOfCycle;
                    secondsOfExhale = 0;
                }
                if(secondsOfBreath == BreathSec && secondsOfExhale == 0)
                {
                    //play start exhale
                    myEllipse.ScaleTo(0.1, (uint)ExhaleSec * 1000);
                    startExhaleAudio?.Play();
                    
                }
                OnPropertyChanged(nameof(SecondsOfBreath));
                OnPropertyChanged(nameof(SecondsOfExhale));
                //OnPropertyChanged(nameof(BreathParam));
                _remainedTime--;
                OnPropertyChanged(nameof(RemainedTime));
            }
            else
            {
                // упражнение завершено
                IsRunning = false;
                ResetTimer();

            }
        }
    }
}
