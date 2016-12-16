using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1.GameObjects
{
    class Player
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public float sizeX { get; set; }
        public float sizeY { get; set; }
        public float sizeZ { get; set; }

        public bool colliderXleft { get; set; }
        public bool colliderXright { get; set; }
        public bool colliderYbottom { get; set; }
        public bool colliderYtop { get; set; }
        public bool colliderZ { get; set; }

        public float speed { get; set; }
        public float weight { get; set; }

        public bool isJumping { get; set; }
        public float jumpingLimit { get; set; }
        public float jumpMax { get; set; }




        // not used still
        public bool directionLeft { get; set; }
        public bool directionRight { get; set; }

        
    }
}
