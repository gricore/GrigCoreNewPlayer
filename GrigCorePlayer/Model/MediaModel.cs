using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrigCorePlayer.Model
{
    public enum PlayerCommand
    {
        Play,
        Stop,
        Pause,
        Forward, 
        VolumeChange, 
        PositionChange
    }

    public class MediaModel
    {
        public PlayerCommand PlayerCommand { get; set; }
        public double Volume { get; set; }
        public double Position { get; set; }
    }
}
