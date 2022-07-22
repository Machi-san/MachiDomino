using ProyectoDomino;

namespace Cierre
{
    public class CierreBonificado<T> : IGanadorCierre<T> where T : IComparable, IEquatable<T>
    {
        // metodos de la interfaz IGanadorCierre
        public List<int> Ganador(List<IJugador<T>> jugadores)
        {
            List<int> pases = new List<int>();

            foreach (var i in jugadores)
                pases.Add(i.GetPases);

            List<int> ganadores = new List<int>();

            for (int i = 0; i < pases.Count(); i++)
            {
                if (pases[i] == MenorLista(pases))
                    ganadores.Add(i);
            }

            return ganadores;
        }

        // metodos de la clase
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