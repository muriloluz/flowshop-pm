// See https://aka.ms/new-console-template for more information
using flowshop_pm.GA;

Console.WriteLine("Inicio");


var parser = new Parser();
var problema = parser.Ler();

Console.WriteLine("Número de Jobs: {0} ============ Número de máquinas: {1}", problema.TotalJobs, problema.TotalMaquinas);

Console.WriteLine("Tempo de processamento: ");

for (int i = 0; i < problema.TotalMaquinas; i++)
{
    for (int j = 0; j < problema.TotalJobs; j++)
    {
        Console.Write(problema.infoJobs.TempoProcessamentoPorMaquina[i][j] + "  ");
    }

    Console.WriteLine();
}

Console.WriteLine("Tempo processamento das manutenções: ");

for (int i = 0; i < problema.TotalMaquinas; i++)
{
    for (int j = 0; j < problema.ManutencoesMaximaPorMaquina; j++)
    {
        Console.Write(problema.infoManutencaoMaquina.TempoManutencao[i][j] + "  ");
    }

    Console.WriteLine();
}

int tempoParaManutencao = 120;

Console.WriteLine("Tempo máximo para manutenção: " + tempoParaManutencao);

var estrategiaAg = new EstrategiaGA(problema);
estrategiaAg.Iniciar();



//int[] sequencia = new int[] { 0, 1, 2, 4, 3 };


//var individuo = new Individuo(5, 3, sequencia);
//individuo.AtualizarMakeSpan(estrategiaAg.InfoFlowShop);
//individuo.ImprimirIndividuo();