using System;
using System.Collections.Generic;
using System.Text;

namespace ShootingMobile.Model
{
    class Coardinate
    {
        public Single x { get; set; }

        //[ColumnName("Label")]
        public Single y { get; set; }

        public Coardinate(int x, float y)
        {
            this.x = x;
            this.y = (Single)y;
        }

        public override string ToString()
        {
            return x + " " + y;
        }
    }
}
