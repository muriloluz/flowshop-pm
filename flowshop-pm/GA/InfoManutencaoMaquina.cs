using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flowshop_pm.GA
{
    public class InfoManutencaoMaquina
    {
        /// <summary>
        /// Tempo Manutenção máquina i número manutenção j
        /// </summary>
        public int[][] TempoManutencao { get; set; }

        public InfoManutencaoMaquina(int[][] tempoManutencao)
        {
            TempoManutencao = tempoManutencao;
        }
    }
}
