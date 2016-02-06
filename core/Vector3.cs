using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class Vector3
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }

        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;

        }

        public Vector3(int _x, int _y, int _z)
        {
            x = _x;
            y = _y;
            z = _z;

        }

        public Vector3(Vector3 _vector)
        {
            this.x = _vector.x;
            this.y = _vector.y;
            this.z = _vector.z;

        }

        public static Vector3 Sub(Vector3 _vector1, Vector3 _vector2)
        {
            Vector3 resVector = new Vector3();

            resVector.x = _vector1.x - _vector2.x;
            resVector.y = _vector1.y - _vector2.y;
            resVector.z = _vector1.z - _vector2.z;

            return resVector;
        }

        public static Vector3 Multiply(Vector3 _vector, int _num)
        {
            Vector3 resVector = new Vector3();

            resVector.x = _vector.x * _num;
            resVector.y = _vector.y * _num;
            resVector.z = _vector.z * _num;

            return resVector;
        }

    }
}
