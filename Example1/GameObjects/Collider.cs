using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1.GameObjects
{
    class Collider
    {
        public float[,] frontViewCoords { get; set; }
        public float[,] backViewCoords { get; set; }
        public float[,] leftViewCoords { get; set; }
        public float[,] rightViewCoords { get; set; }
        public float[,] topViewCoords { get; set; }
        public float[,] bottomViewCoords { get; set; }
        public string type { get; set; }
    }
}
