using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flowshop_pm.GA
{
    public class Parser
    {
        public List<InfoFlowShop> Ler()
        {
            var retorno = new List<InfoFlowShop>();

            var currentDir = Environment.CurrentDirectory + "\\data\\instancias_disp\\";

            var prefixoArquivo = "RGd_20_*";

            var files = Directory.GetFiles(currentDir, prefixoArquivo + ".txt", SearchOption.AllDirectories);


            foreach (var file in files)
            {
                var linhasArquivo = File.ReadAllLines(file);
                var linhaCorrente = 0;

                var totalJobs = int.Parse(linhasArquivo[linhaCorrente].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries)[0].Trim());
                var totalMaquinas = int.Parse(linhasArquivo[linhaCorrente].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].Trim());

                var tabelProcessamento = new int[totalJobs][];


                for (int j = 0; j < totalJobs; j++)
                {
                    linhaCorrente++;
                    tabelProcessamento[j] = new int[totalMaquinas];
                    var valores = linhasArquivo[linhaCorrente].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    var posicaoEquivalente = 1;
                    for (int m = 0; m < totalMaquinas; m++)
                    {
                        /// O arquivos contém o proprio índice intercalado
                        tabelProcessamento[j][m] = int.Parse(valores[posicaoEquivalente]);
                        posicaoEquivalente += 2;
                    }
                }

                linhaCorrente++;

                //// Agora deve iniciar a leitura do TPM
                if (linhasArquivo[linhaCorrente].Trim() != "TPM")
                {
                    throw new Exception("Algo inesperado no arquivo");
                }

                linhaCorrente++;

                /// Valores do tempo máximo da manutenção
                var tabelaPeriodoManutecao = new int[totalMaquinas];


                for (int m = 0; m < totalMaquinas; m++)
                {
                    var valores = linhasArquivo[linhaCorrente].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    var posicaoEquivalente = 1;
                    tabelaPeriodoManutecao[m] = int.Parse(valores[posicaoEquivalente]);
                    posicaoEquivalente += 2;
                }

                linhaCorrente++;

                //// Agora deve iniciar a leitura do TPM
                if (linhasArquivo[linhaCorrente].Trim() != "PMT")
                {
                    throw new Exception("Algo inesperado no arquivo");
                }

                //// Tempo gasto das manuteções

                linhaCorrente++;

                //// Quantidade manutenções estimadas
                var tabelaManutencoes = new double[totalJobs / 2][];

                var sequenciaManutencao = 0;

                while (linhaCorrente < linhasArquivo.Count())
                {
                    var valores = linhasArquivo[linhaCorrente].Trim().Split(" ", System.StringSplitOptions.RemoveEmptyEntries);

                    var posicaoEquivalente = 1;

                    tabelaManutencoes[sequenciaManutencao] = new double[totalMaquinas];

                    for (int m = 0; m < totalMaquinas; m++)
                    {
                        /// O arquivos contém o proprio índice intercalado
                        tabelaManutencoes[sequenciaManutencao][m] = int.Parse(valores[posicaoEquivalente]);
                        posicaoEquivalente += 2;
                    }

                    sequenciaManutencao++;
                    linhaCorrente++;
                }

                /// calcular a media de tempo gasto em cada job

                var tempoTotal = 0;

                for (int j = 0; j < totalJobs; j++)
                {
                    for (int m = 0; m < totalMaquinas; m++)
                    {
                        tempoTotal += tabelProcessamento[j][m];
                    }
                }

                var media = Math.Ceiling(((double)tempoTotal / (double)(totalJobs * totalMaquinas)));

                foreach (var manutencao in tabelaManutencoes)
                {
                    if(manutencao != null)
                    {
                        for(int m =0; m < manutencao.Length; m++)
                        {
                            var percentual = ((double)(manutencao[m] / 100));
                            manutencao[m] = Math.Ceiling(percentual * media);
                        }
                    }
                }

                var infoJobs = new InfoJobs(tabelProcessamento);
                var infoManutencoes = new InfoManutencaoMaquina(tabelaManutencoes);

                var tabelaManutencoesMaximas = new int[totalMaquinas];

                for (int m = 0; m < totalMaquinas; m++)
                {
                    var tempoGasto = 0;

                    for (int j = 0; j < totalJobs; j++)
                    {
                        tempoGasto += tabelProcessamento[j][m];
                    }

                    tabelaManutencoesMaximas[m] = (int)Math.Ceiling(((double)tempoGasto /(double)tabelaPeriodoManutecao[m]));
                }

                var nomeArquivo = file.Substring(file.LastIndexOf("\\")).Replace("\\", string.Empty).Replace(".txt",string.Empty);
                var infoFlowShop = new InfoFlowShop(infoJobs, infoManutencoes, totalJobs, totalMaquinas, tabelaPeriodoManutecao, tabelaManutencoesMaximas, nomeArquivo);

                retorno.Add(infoFlowShop);
            }

            return retorno;
        }
    }
}
