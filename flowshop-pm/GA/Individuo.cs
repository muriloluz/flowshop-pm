

namespace flowshop_pm.GA
{
    public class Individuo
    {
        public  int QuantidadeTotalJobs;
        public int QuantidadeTotalMaquinas;
        public int[] SequenciaExecucaoJobs;
        protected int[][] ManutencaoPreventiva;
        private double[][] MatrizMakeSpan;
        private double[][] MatrizMakeSpanComManutencao;

        public Individuo(int quantidadeTotalJobs, int quantidadeTotalMaquinas, int[] sequenciaExecucao)
        {
            QuantidadeTotalJobs = quantidadeTotalJobs;
            QuantidadeTotalMaquinas = quantidadeTotalMaquinas;

            SequenciaExecucaoJobs = sequenciaExecucao;
            ManutencaoPreventiva = new int[this.QuantidadeTotalMaquinas][];
        }

        public void AtualizarMakeSpan(InfoFlowShop infoFlowShop)
        {
            this.MatrizMakeSpan = new double[this.QuantidadeTotalMaquinas + 1][];

            for (int i = 0; i < this.QuantidadeTotalMaquinas + 1; i++)
            {
                this.MatrizMakeSpan[i] = new double[QuantidadeTotalJobs + 1];
                this.MatrizMakeSpan[i][0] = 0.0;
            }
            for (int i = 0; i < this.QuantidadeTotalJobs + 1; i++)
            {
                this.MatrizMakeSpan[0][i] = 0.0;
            }

            for (int i = 1; i < this.QuantidadeTotalMaquinas + 1; i++)
            {
                //var tempoGastoProcessamento = 0.0;

                for (int j = 1; j < this.QuantidadeTotalJobs + 1; j++)
                {
                    if (this.MatrizMakeSpan[i - 1][j] > this.MatrizMakeSpan[i][j - 1])
                    {
                        this.MatrizMakeSpan[i][j] += this.MatrizMakeSpan[i - 1][j] + infoFlowShop.infoJobs.TempoProcessamentoPorMaquina[i - 1][this.SequenciaExecucaoJobs[j - 1]];
                    }
                    else
                    {
                        this.MatrizMakeSpan[i][j] += this.MatrizMakeSpan[i][j - 1] + infoFlowShop.infoJobs.TempoProcessamentoPorMaquina[i - 1][this.SequenciaExecucaoJobs[j - 1]];
                    }

                    //tempoGastoProcessamento += infoFlowShop.infoJobs.TempoProcessamentoPorMaquina[i - 1][this.SequenciaExecucaoJobs[j - 1]];

                    //var manutencaoAtual = this.ManutencaoPreventiva[i - 1] == null ? 0 : this.ManutencaoPreventiva[i - 1].Length - 1;
                    //var tempoManutencao = infoFlowShop.infoManutencaoMaquina.TempoManutencao[i - 1][manutencaoAtual];
                    //var tempoParaManutencao = 120;

                    //if ((tempoGastoProcessamento + infoFlowShop.infoJobs.TempoProcessamentoPorMaquina[i - 1][this.SequenciaExecucaoJobs[j - 1]]) >= tempoParaManutencao)
                    //{
                    //    tempoGastoProcessamento = infoFlowShop.infoJobs.TempoProcessamentoPorMaquina[i - 1][this.SequenciaExecucaoJobs[j - 1]];

                    //    this.MatrizMakeSpan[i][j - 1] += tempoManutencao;

                    //    if (this.ManutencaoPreventiva[i - 1] == null)
                    //    {
                    //        // TODO: remover máximo manutenção hardcoded
                    //        this.ManutencaoPreventiva[i - 1] = new int[2];
                    //        this.ManutencaoPreventiva[i - 1][0] = -1;
                    //        this.ManutencaoPreventiva[i - 1][1] = -1;
                    //    }

                    //    this.ManutencaoPreventiva[i - 1][manutencaoAtual] = j - 2;
                    //}
                    //else
                    //{
                    //    tempoGastoProcessamento += infoFlowShop.infoJobs.TempoProcessamentoPorMaquina[i - 1][this.SequenciaExecucaoJobs[j - 1]];
                    //}
                }
            }
        }

        public double Fitness(){

            return this.MatrizMakeSpan[this.QuantidadeTotalMaquinas][this.QuantidadeTotalJobs];
        }

        public void ImprimirIndividuo()
        {
            foreach(var i in this.SequenciaExecucaoJobs)
            {
                Console.Write(i + " "); 
            }

            Console.WriteLine();

            //for (int s = 0; s < this.ManutencaoPreventiva.Length; s++)
            //{
            //    foreach (var j in this.ManutencaoPreventiva[s])
            //    {
            //        Console.WriteLine(" Manutencao Maquina " + s + " Depois do job: " + j);
            //    }
            //}

            Console.WriteLine("Fitness Makespan: " + this.Fitness());
        }
    }
}
