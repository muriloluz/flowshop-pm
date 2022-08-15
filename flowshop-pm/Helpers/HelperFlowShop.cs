using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flowshop_pm.Helpers
{
    public static class HelperFlowShop
    {
        public static int[] IniciarSequenciaAleatoria(int quantidadeItens)
        {
            var itens = new int[quantidadeItens];

            for (int i = 0; i < quantidadeItens; i++)
            {
                itens[i] = i;
            }

            int n = quantidadeItens - 1;
            while (n > 1)
            {
                n--;
                int posicaoParaEmbaralhar = Constantes.Randomico.ProximoInt(quantidadeItens);

                int k = itens[n];
                itens[n] = itens[posicaoParaEmbaralhar];
                itens[posicaoParaEmbaralhar] = k;
            }

            return itens;
        }
    }
}
