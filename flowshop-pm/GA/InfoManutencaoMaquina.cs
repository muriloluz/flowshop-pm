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
        public double[][] TempoManutencao { get; set; }

        public InfoManutencaoMaquina(double[][] tempoManutencao)
        {
            TempoManutencao = tempoManutencao;
        }
    }
}
