using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flowshop_pm.Helpers
{
    public static class Constantes
    {
        public static int TamanhoPopulacao = 25;
        public static int Geracoes = 100;

        public static int TaxaMutacao = 2;
        public static int TaxaRecombinacao = 85;
        public static int QuantidadeFilhosPorGeracao = 10;

        public static bool ImprimirIndividuo = true;
        public static int ImprimirACada = 50;
        public static int ParticipantesTorneio = 2;
        public static Randomico Randomico = new Randomico();
    }
}
