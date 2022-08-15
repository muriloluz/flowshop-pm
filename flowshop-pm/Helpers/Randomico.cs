using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace flowshop_pm.Helpers
{
    public class Randomico
    {
        public int ProximoInt(int limiteNaoInclusivo)
        {
            return RandomNumberGenerator.GetInt32(limiteNaoInclusivo);
        }
    }
}
