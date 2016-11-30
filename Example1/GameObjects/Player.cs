using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1.GameObjects
{
    class Player
    {
        public double zX { get; set; }
        public double zY { get; set; }
        public double zZ { get; set; }

        public double moveX { get; set; }
        public double moveY { get; set; }
        public double moveZ { get; set; }

        public double pozX { get; set; }
        public double pozY { get; set; }
        public double pozZ { get; set; }

        public double sizeX { get; set; }
        public double sizeY { get; set; }
        public double sizeZ { get; set; }

        public bool colliderX { get; set; }
        public bool colliderY { get; set; }
        public bool colliderZ { get; set; }

        public double gravity { get; set; }
        public double jumpHeight { get; set; }
        public double jumpPoint { get; set; }
        public bool isJumping { get; set; }

        public bool directionLeft { get; set; }
        public bool directionRight { get; set; }

        public double speedX { get; set; }
    }
}
