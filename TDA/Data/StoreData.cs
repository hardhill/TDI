using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDA.Data
{
    internal class StoreData
    {
        private int _cycles;
        private int _breathSec;
        private int _exhaleSec;
        private int _volumeId;
        public int Cycles { get { return _cycles; } set { _cycles = value; } }
        public int BreathSec { get { return _breathSec; } set { _breathSec = value; } }
        public int ExhaleSec { get { return _exhaleSec; } set { _exhaleSec = value; } }
        public int VolumeId { get { return _volumeId; } set { _volumeId = value; } }
        public StoreData(int cycles,int breathSec, int exhaleSec, int volumeName) {
            _cycles = cycles;
            _breathSec = breathSec;
            _exhaleSec = exhaleSec;
            _volumeId = volumeName;
        }
    }
}
