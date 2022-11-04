using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDA.ViewModels
{
    public class Volumes
    {
        VolumeName[] volumeName = { 
            new VolumeName(0,"volume_x_icon",0d), 
            new VolumeName(1,"volume_icon",0.33d), 
            new VolumeName(2,"volume_1_icon",0.66d), 
            new VolumeName(3,"volume_2_icon",1d) 
        };
        public VolumeName GetVolume(int id)
        {
            if (id >= volumeName.Length)
            {
                return volumeName[0];
            }
            return volumeName[id];
        }
        public VolumeName NextValume(int current)
        {
            if(current >= volumeName.Length-1)
            {
                return GetVolume(0);
            }
            else
            {
                return GetVolume(current+1);
            }
        }
    }
    public class VolumeName
    {
        public VolumeName(int id, string name, double vol)
        {
            Id = id;
            Name = name;
            Volume = vol;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Volume { get; set; }
    }
}
