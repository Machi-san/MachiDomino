using ProyectoDomino;
using Juego;

namespace Cierre
{
    public class CierreClasico<T> : IGanadorCierre<T> where T : IComparable, IEquatable<T>
    {
        // metodos de la interfaz IGanadorCierre
        public List<int> Ganador(List<IJugador<T>> jugadores)
        {
            Domino<T>._elementosFichas.Sort();

            Dictionary<T, int> valorElementos = new Dictionary<T, int>();

            for (int i = 0; i < Domino<T>._elementosFichas.Count(); i++)
                valorElementos.Add((T)Domino<T>._elementosFichas[i], i);

            List<int> valorMano = new List<int>();

            for (int i = 0; i < jugadores.Count(); i++)
                valorMano.Add(Sumatoria(ConvierteAInt(valorElementos, jugadores[i].GetAlmacenMano)));

            List<int> ganadores = new List<int>();

            for (int i = 0; i < valorMano.Count(); i++)
            {
                if (valorMano[i] == MenorLista(valorMano))
                    ganadores.Add(i);
            }

            return ganadores;
        }

        // metodos de la clase
        private List<int[]> ConvierteAInt(Dictionary<T, int> valorElementos, List<T[]> mano)
        {
            List<int[]> lista = new List<int[]>();

            for (int i = 0; i < mano.Count(); i++)
                lista.Add(new int[] { valorElementos[mano[i][0]], valorElementos[mano[i][1]] });

            return lista;
        }

        private int Sumatoria(List<int[]> lista)
        {
            int contador = 0;

            for (int i = 0; i < lista.Count(); i++)
                contador = contador + lista[i][0] + lista[i][1];

            return contador;
        }

        private int MenorLista(List<int> lista)
        {
            int contador = int.MaxValue;

            foreach (var i in lista)
            {
                if (i < contador)
                    contador = i;
            }

            return contador;
        }
    }
}