using flowshop_pm.GA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flowshop_pm.Helpers
{
    public static class Mutacao
    {
        public static void Muta(Individuo individuo)
        {
            for (int i = 0; i < individuo.SequenciaExecucaoJobs.Length; i++)
            {
                var random = Constantes.Randomico.ProximoInt(101);
                if (random <= Constantes.TaxaMutacao)
                {
                    var quantidadeJobs = individuo.QuantidadeTotalJobs;

                    var pontoUm = Constantes.Randomico.ProximoInt(quantidadeJobs);
                    var pontoDois = Constantes.Randomico.ProximoInt(quantidadeJobs);

                    var temp = individuo.SequenciaExecucaoJobs[pontoUm];
                    individuo.SequenciaExecucaoJobs[pontoUm] = individuo.SequenciaExecucaoJobs[pontoDois];
                    individuo.SequenciaExecucaoJobs[pontoDois] = temp;
                }
            }
        }
    }
}
