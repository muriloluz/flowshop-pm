using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flowshop_pm.GA
{
    public class InfoFlowShop
    {
        public InfoJobs infoJobs { get; set; }
        public InfoManutencaoMaquina infoManutencaoMaquina { get; set; }
        public int TotalJobs { get; set; }
        public int TotalMaquinas { get; set; }
        public int TempoParaManutencao { get; set; }
        public int ManutencoesMaximaPorMaquina {  get; set; }   

        public InfoFlowShop(InfoJobs infoJobs, InfoManutencaoMaquina infoManutencaoMaquina, int totalJobs, int totalMaquinas, int tempoParaManutencao, int manutencoesMaximaPorMaquina)
        {
            this.infoJobs = infoJobs;
            this.infoManutencaoMaquina = infoManutencaoMaquina;
            TotalJobs = totalJobs;
            TotalMaquinas = totalMaquinas;
            TempoParaManutencao = tempoParaManutencao;
            ManutencoesMaximaPorMaquina = manutencoesMaximaPorMaquina;
        }
    }
}
