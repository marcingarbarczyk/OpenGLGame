using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1.GameObjects
{
    class Level
    {
        public Part[] parts { get; set; }

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
