using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1.GameObjects
{
    class Camera
    {
        public double eyeX { get;set; }
        public double eyeY { get; set; }
        public double eyeZ { get; set; }

        public double centerX { get; set; }
        public double centerY { get; set; }
        public double centerZ { get; set; }

        public double upX { get; set; }
        public double upY { get; set; }
        public double upZ { get; set; }
    }
}
