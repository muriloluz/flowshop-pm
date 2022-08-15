using flowshop_pm.GA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flowshop_pm.Helpers
{
    public static class Recombinacao
    {
        public static List<Individuo> CrossoverOrdenado(
                            Individuo pai,
                            Individuo mae)
        {
            var random = Constantes.Randomico.ProximoInt(101);

            Individuo filho1;
            Individuo filho2;

            var quantidadeJobs = pai.QuantidadeTotalJobs;

            if (random <= Constantes.TaxaRecombinacao)
            {
                var pontoUm = Constantes.Randomico.ProximoInt(quantidadeJobs);
                var pontoDois = Constantes.Randomico.ProximoInt(quantidadeJobs);
                var pontoMenor = (pontoUm < pontoDois) ? pontoUm : pontoDois;
                var pontoMaior = (pontoUm >= pontoDois) ? pontoUm : pontoDois;

                var sequenciaFilho1 = new int[quantidadeJobs];
                var sequenciaFilho2 = new int[quantidadeJobs];

                for(int i = 0; i < quantidadeJobs; i++)
                {
                    sequenciaFilho1[i] = -1;
                    sequenciaFilho2[i] = -1;
                }

                for (int i = pontoMenor; i <= pontoMaior; i++)
                {
                    sequenciaFilho1[i] = pai.SequenciaExecucaoJobs[i];
                    sequenciaFilho2[i] = mae.SequenciaExecucaoJobs[i];
                }

                for(int i = 0; i < quantidadeJobs; i++)
                {
                    if (!sequenciaFilho1.Contains(mae.SequenciaExecucaoJobs[i]))
                    {
                        var indice = sequenciaFilho1.ToList().IndexOf(-1);
                        sequenciaFilho1[indice] = mae.SequenciaExecucaoJobs[i];
                    }

                    if (!sequenciaFilho2.Contains(pai.SequenciaExecucaoJobs[i]))
                    {
                        var indice = sequenciaFilho2.ToList().IndexOf(-1);
                        sequenciaFilho2[indice] = pai.SequenciaExecucaoJobs[i];
                    }
                }

                filho1 = new Individuo(pai.QuantidadeTotalJobs, pai.QuantidadeTotalMaquinas, sequenciaFilho1);
                filho2 = new Individuo(pai.QuantidadeTotalJobs, pai.QuantidadeTotalMaquinas, sequenciaFilho2);
            }

            else
            {
                filho1 = Clona(pai, quantidadeJobs);
                filho2 = Clona(mae, quantidadeJobs);
            }

            var retorno = new List<Individuo>();

            retorno.Add(filho1);
            retorno.Add(filho2);

            return retorno;
        }

        private static Individuo Clona(Individuo pai, int quantidadeJobs)
        {
            var sequenciaFilho1 = new int[quantidadeJobs];

            for (int i = 0; i < quantidadeJobs; i++)
            {
                sequenciaFilho1[i] = pai.SequenciaExecucaoJobs[i];
            }

            var filho = new Individuo(pai.QuantidadeTotalJobs, pai.QuantidadeTotalMaquinas, sequenciaFilho1);

            return filho;
        }
    }
}
