using ProyectoDomino;

namespace Fin
{
    public class FinClasico<T> : IFin<T> where T : IComparable, IEquatable<T>
    {
        // metodos de la interfaz IFin
        public bool Fin(List<IJugador<T>> jugadores)
        {
            for (int i = 0; i < jugadores.Count(); i++)
            {
                if (jugadores[i].GetEstado == 0)
                    return false;
            }

            return true;
        }
    }
}