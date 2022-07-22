using ProyectoDomino;

namespace Juego
{
    public class Reglas<T> : IReglas<T> where T : IComparable, IEquatable<T>
    {
        // metodos de la interfaz IReglas
        public bool Victoria(List<T[]> mano)
        {
            if (mano.Count() == 0)
                return true;

            return false;
        }

        public bool Fin(IFin<T> criterio, List<IJugador<T>> jugadores)
        {
          return criterio.Fin(jugadores);
        }

        public bool JugadaValida(List<T[]> mesa, T[] ficha)
        {
            T cara1 = mesa[0][0];
            T cara2 = mesa[mesa.Count() - 1][1];

            if (ficha[0].Equals(cara1) || ficha[1].Equals(cara1) || ficha[0].Equals(cara2) || ficha[1].Equals(cara2))
                return true;

            return false;
        }

        public List<int> GanadorCierre(IGanadorCierre<T> ganador, List<IJugador<T>> jugadores)
        {
            return ganador.Ganador(jugadores);
        }
    }
}