using ProyectoDomino;

namespace Fin
{
    public class FinPersonalizado<T> : IFin<T> where T : IComparable, IEquatable<T>
    {
        // metodos de la interfaz IFin
        public bool Fin(List<IJugador<T>> jugadores)
        {
            foreach (var i in jugadores)
            {
                if (i.GetAlmacenMano.Count() == 1)
                    return true;
            }

            return false;
        }
    }
}