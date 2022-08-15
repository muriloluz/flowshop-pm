using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flowshop_pm.GA
{
    public class InfoJobs
    {
        /// <summary>
        /// Tempo de processamento máquina i job j
        /// </summary>
        public int[][] TempoProcessamentoPorMaquina { get; private set; }

        public InfoJobs(int[][] tempoProcessamento)
        {
            this.TempoProcessamentoPorMaquina = tempoProcessamento;
        }
    }
}
