using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1.GameObjects
{
    class Enemy : Object
    {
        public bool fly { get; set; }
        public float rangeLeft { get; set; }
        public float rangeRight { get; set; }
        public int life { get; set; }

        public Enemy()
        {
            this.modelWalk = new Model[2];
        }
    }
}
