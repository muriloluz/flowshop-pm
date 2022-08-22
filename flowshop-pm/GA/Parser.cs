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
        public InfoFlowShop Ler()
        {
            var currentDir = Environment.CurrentDirectory + "\\data\\instancias_disp\\";

            var prefixoArquivo = "RGd_20_5_2_25_1";

            var files = Directory.GetFiles(currentDir, prefixoArquivo + ".txt", SearchOption.AllDirectories);


            foreach (var file in files)
            {
                var linhasArquivo = File.ReadAllLines(file);
                var linhaCorrente = 0;

                var totalJobs = int.Parse(linhasArquivo[linhaCorrente].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries)[0].Trim());
                var totalMaquinas = int.Parse(linhasArquivo[linhaCorrente].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].Trim());

                var tabelProcessamento = new int[totalJobs][];

            
                for(int j = 0; j < totalJobs; j++)
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
                if(linhasArquivo[linhaCorrente].Trim() != "TPM")
                {
                    throw new Exception("Algo inesperado no arquivo");
                }

                linhaCorrente++;

                /// TODO: Valores do tempo máximo da manutenção
                var tabelaPeriodoManutecao = new int[totalMaquinas];


                linhaCorrente++;

                //// Agora deve iniciar a leitura do TPM
                if (linhasArquivo[linhaCorrente].Trim() != "PMT")
                {
                    throw new Exception("Algo inesperado no arquivo");
                }

                //// Tempo gasto das manuteções

                linhaCorrente++;

                //// Quantidade manutenções estimadas
                var tabelaManutencoes = new int[totalJobs / 2][];

                var sequenciaManutencao = 0;

                while (linhaCorrente < linhasArquivo.Count())
                {
                    var valores = linhasArquivo[linhaCorrente].Trim().Split(" ", System.StringSplitOptions.RemoveEmptyEntries);

                    var posicaoEquivalente = 1;

                    tabelaManutencoes[sequenciaManutencao] = new int[totalMaquinas];

                    for (int m = 0; m < totalMaquinas; m++)
                    {
                        /// O arquivos contém o proprio índice intercalado
                        tabelaManutencoes[sequenciaManutencao][m] = int.Parse(valores[posicaoEquivalente]);
                        posicaoEquivalente += 2;
                    }

                    sequenciaManutencao++;
                    linhaCorrente++;
                }
            }

            var tempoParaManutecao = 120;
            var manutencoesMaximas = 2;

            //var infoJobs = new InfoJobs(tabelProcessamento);
            //var infoManutencoes = new InfoManutencaoMaquina(tabelaManutecoes);

            //var infoFlowShop = new InfoFlowShop(infoJobs, infoManutencoes, totalJobs, totalMaquinas, tempoParaManutecao, manutencoesMaximas);

            return null;
        }
    }
}
