

namespace flowshop_pm.GA
{
    public class Individuo
    {
        public  int QuantidadeTotalJobs;
        public int QuantidadeTotalMaquinas;
        public int[] SequenciaExecucaoJobs;
        protected int[][] ManutencaoPreventiva;
        private double[][] MatrizMakeSpan;

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

            ///Inicia array com as manutenções por máquinas           
            for(int m = 0; m < this.QuantidadeTotalMaquinas; m++)
            {
                var manutencoesMaximaDaMaquina = infoFlowShop.ManutencoesMaximaPorMaquina[m];
                this.ManutencaoPreventiva[m] = new int[manutencoesMaximaDaMaquina];
                for (int mx = 0; mx < manutencoesMaximaDaMaquina; mx++)
                {
                    this.ManutencaoPreventiva[m][mx] = -1;
                }
            }

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
                var tempoGastoProcessamento = 0.0;

                for (int j = 1; j < this.QuantidadeTotalJobs + 1; j++)
                {
                    var tempoProcessamentoJob = infoFlowShop.infoJobs.TempoProcessamentoPorMaquina[this.SequenciaExecucaoJobs[j - 1]][i - 1];

                    //// Está errado. Obtem a manutencao atual ou seja o indice da primeira com -1       
                    var manutencaoAtual = this.ManutencaoPreventiva[i - 1].ToList().IndexOf(-1);

                    //// Tempo para a manutenção na máquina
                    var tempoParaManutencao = infoFlowShop.TempoParaManutencao[i - 1];

                    var manutencaoNecessaria = (tempoGastoProcessamento + tempoProcessamentoJob) >= tempoParaManutencao;

                    //// Tempo para realizar a manutenção
                    var tempoProcessamentoManutencao = manutencaoNecessaria ? infoFlowShop.infoManutencaoMaquina.TempoManutencao[manutencaoAtual][i - 1] : 0;

                    if (manutencaoNecessaria)
                    {
                        tempoGastoProcessamento = tempoProcessamentoJob;
                        this.ManutencaoPreventiva[i - 1][manutencaoAtual] = j - 1;
                    }
                    else
                    {
                        tempoGastoProcessamento += tempoProcessamentoJob;
                    }

                    if (this.MatrizMakeSpan[i - 1][j] > (this.MatrizMakeSpan[i][j - 1] + tempoProcessamentoManutencao))
                    {
                        this.MatrizMakeSpan[i][j] = this.MatrizMakeSpan[i - 1][j] + tempoProcessamentoJob;
                    }
                    else
                    {
                        this.MatrizMakeSpan[i][j] = this.MatrizMakeSpan[i][j - 1] + tempoProcessamentoJob + tempoProcessamentoManutencao;
                    }
                }
            }
        }

        public double Fitness(){

            return this.MatrizMakeSpan[this.QuantidadeTotalMaquinas][this.QuantidadeTotalJobs];
        }

        public void ImprimirIndividuo()
        {
            Console.WriteLine("==================INICIO==================");
            foreach (var i in this.SequenciaExecucaoJobs)
            {
                Console.Write(i + " "); 
            }

            Console.WriteLine();

            for (int s = 0; s < this.ManutencaoPreventiva.Length; s++)
            {
                foreach (var j in this.ManutencaoPreventiva[s])
                {
                    if(j == -1)
                    {
                        continue;
                    }

                    Console.WriteLine(" Manutencao Maquina " + s + " =====  Entre a Sequência " + j + " e " + (j + 1) +  "  ===== Antes do Job: " + this.SequenciaExecucaoJobs[j]);
                }
            }

            for (int s = 0; s < this.MatrizMakeSpan.Length; s++)
            {
                for (int t = 0; t < this.MatrizMakeSpan[s].Length; t++)
                {
                    Console.Write(this.MatrizMakeSpan[s][t] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Fitness Makespan: " + this.Fitness());

            Console.WriteLine("===================FIM!===================");
        }
    }
}
