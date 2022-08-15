using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flowshop_pm.GA
{
    public class Parser
    {
        public InfoFlowShop Ler()
        {
            var totalJobs = 5;
            var totalMaquinas = 3;
            var tempoParaManutecao = 120;
            var manutencoesMaximas = 2;

            var tabelProcessamento = new int[totalMaquinas][];

            if (tabelProcessamento[0] == null)
            {
                tabelProcessamento[0] = new int[totalJobs];
            }

            tabelProcessamento[0][0] = 3;
            tabelProcessamento[0][1] = 45;
            tabelProcessamento[0][2] = 84;
            tabelProcessamento[0][3] = 24;
            tabelProcessamento[0][4] = 39;


            if (tabelProcessamento[1] == null)
            {
                tabelProcessamento[1] = new int[totalJobs];
            }

            tabelProcessamento[1][0] = 38;
            tabelProcessamento[1][1] = 51;
            tabelProcessamento[1][2] = 7;
            tabelProcessamento[1][3] = 21;
            tabelProcessamento[1][4] = 16;


            if (tabelProcessamento[2] == null)
            {
                tabelProcessamento[2] = new int[totalJobs];
            }

            tabelProcessamento[2][0] = 52;
            tabelProcessamento[2][1] = 83;
            tabelProcessamento[2][2] = 19;
            tabelProcessamento[2][3] = 6;
            tabelProcessamento[2][4] = 28;

            var tabelaManutecoes = new int[totalMaquinas][];

            if (tabelaManutecoes[0] == null)
            {
                tabelaManutecoes[0] = new int[manutencoesMaximas];
            }

            tabelaManutecoes[0][0] = 31;
            tabelaManutecoes[0][1] = 43;

            if (tabelaManutecoes[1] == null)
            {
                tabelaManutecoes[1] = new int[manutencoesMaximas];
            }

            tabelaManutecoes[1][0] = 45;
            tabelaManutecoes[1][1] = 42;

            if (tabelaManutecoes[2] == null)
            {
                tabelaManutecoes[2] = new int[manutencoesMaximas];
            }
            tabelaManutecoes[2][0] = 8;
            tabelaManutecoes[2][1] = 45;


            var infoJobs = new InfoJobs(tabelProcessamento);
            var infoManutencoes = new InfoManutencaoMaquina(tabelaManutecoes);

            var infoFlowShop = new InfoFlowShop(infoJobs, infoManutencoes, totalJobs, totalMaquinas, tempoParaManutecao, manutencoesMaximas);

            return infoFlowShop;
        }
    }
}
