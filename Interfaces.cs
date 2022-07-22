namespace ProyectoDomino
{
    public interface IJugador<T> where T : IComparable, IEquatable<T>
    {
        void Juega(List<T[]> mesa, IReglas<T> reglas);
        int GetEstado { get; }
        void RecibeMano(List<T[]> mano);
        List<T[]> GetAlmacenMano { get; }
        int GetPases { get; }
    }

    public interface IReglas<T> where T : IComparable, IEquatable<T>
    {
        bool Victoria(List<T[]> mano);
        bool Fin(IFin<T> criterio, List<IJugador<T>> jugadores);
        bool JugadaValida(List<T[]> mesa, T[] ficha);
        List<int> GanadorCierre(IGanadorCierre<T> ganador, List<IJugador<T>> jugadores);
    }

    public interface IBanco<T> where T : IComparable, IEquatable<T>
    {
        void GeneraFichas(List<object> listaElementos, int variante);
        void GeneraMano(List<IJugador<T>> jugadores);
        List<T[]> GetAlmacenFichas { get; }
    }

    public interface IGanadorCierre<T> where T : IComparable, IEquatable<T>
    {
        List<int> Ganador(List<IJugador<T>> jugadores);
    }

    public interface ITipoJuego<T> where T : IComparable, IEquatable<T>
    {
        void Juego(IReglas<T> reglas);
    }

    public interface IFin<T> where T :IComparable, IEquatable<T>
    {
        bool Fin(List<IJugador<T>> jugadores);
    }
}