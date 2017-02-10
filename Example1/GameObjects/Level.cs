using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1.GameObjects
{
    class Level
    {
        public Part[] parts { get; set; }
        public float backgroundX { get; set; }
        public float backgroundY { get; set; }
        public float backgroundZ { get; set; }
        public float backgroundSizeX { get; set; }
        public float backgroundSizeY { get; set; }
        public float backgroundSizeZ { get; set; }
        public float end { get; set; }
        public uint textureID { get; set; }
        public string music { get; set; }
        public List<float[]> checkpoints = new List<float[]>();

        public Level()
        {
            parts = new Part[100];
            for(int i=0; i<parts.Length; i++)
            {
                parts[i] = new Part();
            }
        }

        
    }
}
