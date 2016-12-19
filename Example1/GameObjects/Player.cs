using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1.GameObjects
{
    class Player : Object
    {
        

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
