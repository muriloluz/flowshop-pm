using flowshop_pm.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flowshop_pm.GA
{
    public class EstrategiaGA
    {

        public List<Individuo> Pais { get; private set; }

        public List<Individuo> Filhos { get; private set; }

        public Individuo MelhorIndividuo { get; private set; }

        public InfoFlowShop InfoFlowShop { get; private set; }

        public EstrategiaGA(InfoFlowShop infoFlowShop)
        {
            InfoFlowShop = infoFlowShop;
        }

        public void Iniciar()
        {
            this.Limpar();
            this.IniciaPopulacao();

            for (int g = 0; g < Constantes.Geracoes; g++)
            {
                for (int i = 0; i < Constantes.QuantidadeFilhosPorGeracao / 2; i++)
                {
                    var pai = this.SelecionaIndividuoTorneio(Constantes.ParticipantesTorneio);
                    var mae = this.SelecionaIndividuoTorneio(Constantes.ParticipantesTorneio);

                    ///// Crossover
                    var filhos = Recombinacao.CrossoverOrdenado(pai, mae);

                    ///// Mutacao
                    foreach (var f in filhos)
                    {
                        Mutacao.Muta(f);
                        f.AtualizarMakeSpan(this.InfoFlowShop);
                    }

                    this.Filhos.AddRange(filhos);
                }

                this.SelecionaMelhoresFilhos(Constantes.QuantidadeFilhosPorGeracao);
                this.MelhorIndividuo = this.Pais.OrderBy(x => x.Fitness()).First();


                if (g % Constantes.ImprimirACada == 0 && Constantes.ImprimirIndividuo)
                {
                    Console.WriteLine("Geração: " + g);
                    this.MelhorIndividuo.ImprimirIndividuo();
                }
            }

            this.MelhorIndividuo = this.Pais.OrderBy(x => x.Fitness()).First();
        }

        private void SelecionaMelhoresFilhos(int n)
        {
            var listaMelhoresPais = this.Pais.OrderBy(x => x.Fitness()).Take(Constantes.TamanhoPopulacao - n).ToList();
            var listaMelhoresFilhos = this.Filhos.OrderBy(x => x.Fitness()).Take(n).ToList();

            listaMelhoresPais.AddRange(listaMelhoresFilhos);

            this.Pais = listaMelhoresPais.ToList();
            this.Filhos.Clear();
        }

        private Individuo SelecionaIndividuoTorneio(int numeroParticipantes)
        {
            var numeroSorteados = new List<int>();
            var listaCandidatos = new List<Individuo>();

            while (numeroSorteados.Count < numeroParticipantes)
            {
                var sorteado = Constantes.Randomico.ProximoInt(Constantes.TamanhoPopulacao);

                if (numeroSorteados.Contains(sorteado))
                {
                    continue;
                }
                else
                {
                    numeroSorteados.Add(sorteado);
                }
            }

            foreach (var s in numeroSorteados)
            {
                listaCandidatos.Add(this.Pais.ElementAt(s));
            }

            return listaCandidatos.OrderBy(x => x.Fitness()).Take(1).First();
        }

        private int QuantidadeJobs()
        {
            return this.InfoFlowShop.TotalJobs;
        }

        private int QuantidadeMaquinas()
        {
            return this.InfoFlowShop.TotalMaquinas;
        }

        private void Limpar()
        {
            this.Pais = new List<Individuo>();
            this.Filhos = new List<Individuo>();
            this.MelhorIndividuo = null;
        }

        private void IniciaPopulacao()
        {
            for (int i = 0; i < Constantes.TamanhoPopulacao; i++)
            {
                var sequenciaJobs = HelperFlowShop.IniciarSequenciaAleatoria(this.QuantidadeJobs());
                var individuo = new Individuo(this.QuantidadeJobs(), this.QuantidadeMaquinas(), sequenciaJobs);
                individuo.AtualizarMakeSpan(this.InfoFlowShop);
                this.Pais.Add(individuo);
            }
        }
    }
}
