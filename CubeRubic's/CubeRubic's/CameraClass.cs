using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeRubic_s
{
    public class CameraClass
    {
        public float x = 6;
        public float y = 6;
        public float z = -6;

        public float c_x = 0;
        public float c_y = 0;
        public float c_z = 0;

        public float[] coords
        {
            get
            {
                return new float[] { x, y, z };
            }
            set
            {
                x = value[0];
                y = value[1];
                z = value[2];
            }
        }
    }
}
