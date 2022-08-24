// See https://aka.ms/new-console-template for more information
using flowshop_pm.GA;
using flowshop_pm.Helpers;

Console.WriteLine("Inicio");


var parser = new Parser();

var problemas = parser.Ler();


foreach (var problema in problemas.OrderBy(x=>x.NomeArquivo))
{
    if (Constantes.ImprimirDetalhesExecucoes)
    {
        Console.WriteLine("==============================================================================");
        Console.WriteLine(problema.NomeArquivo);
        Console.WriteLine("Número de Jobs: {0} ============ Número de máquinas: {1}", problema.TotalJobs, problema.TotalMaquinas);
        Console.WriteLine("Tempo de processamento: ");


        for (int i = 0; i < problema.TotalMaquinas; i++)
        {
            for (int j = 0; j < problema.TotalJobs; j++)
            {
                Console.Write(problema.infoJobs.TempoProcessamentoPorMaquina[j][i] + "  ");
            }

            Console.WriteLine();
        }

        Console.WriteLine("Tempo processamento das manutenções: ");

        for (int i = 0; i < problema.TotalMaquinas; i++)
        {
            for (int j = 0; j < problema.ManutencoesMaximaPorMaquina[i]; j++)
            {
                Console.Write(problema.infoManutencaoMaquina.TempoManutencao[j][i] + "  ");
            }

            Console.WriteLine();
        }
    }

    var resultado = new List<double>();

    for (int e = 0; e < Constantes.QuantidadeExecucoes; e++)
    {
        var estrategiaAg = new EstrategiaGA(problema);
        estrategiaAg.Iniciar();
        resultado.Add(estrategiaAg.MelhorIndividuo.Fitness());
        /// estrategiaAg.MelhorIndividuo.ImprimirIndividuo();
    }

    Console.WriteLine(problema.NomeArquivo + "      " +  resultado.Min() +  "     media: " + resultado.Average());
}