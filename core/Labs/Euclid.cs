using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace core.Euclid
{
    
    public static class Euclid
    {
        public static int Extended(int _a, int _n)
        {
            int q = 0;

            if (NOD(_a, _n) == 1)
            {
                Vector3 u = new Vector3(0, 1, _n);
                Vector3 v = new Vector3(1, 0, _a);
                Vector3 t = new Vector3();

                while (u.z != 1)
                {
                    q = u.z / v.z;
                    t = new Vector3(Vector3.Sub(u, Vector3.Multiply(v, q)));
                    u = new Vector3(v);
                    v = new Vector3(t);
                }

                return u.x < 0 ?  u.x + _n : u.x;              
            }
            else
            {
                throw new Exception("Условие НОД не выполнено");
            }

        }

        public static int NOD(int _num, int _mod)
        {
            while (_num != 0 && _mod != 0)
            {
                if (_num > _mod)
                    _num %= _mod;
                else
                    _mod %= _num;
            }

            if (_num == 0)
                return _mod;
            else
                return _num;

        }

    }
}
