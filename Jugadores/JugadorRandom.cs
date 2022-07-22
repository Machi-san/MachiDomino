using ProyectoDomino;

namespace Jugadores
{
    public class JugadorRandom<T> : IJugador<T> where T : IComparable, IEquatable<T>
    {
        public JugadorRandom()
        {
            _almacenMano = new List<T[]>();
        }

        // metodos de la interfaz IJugador
        public void Juega(List<T[]> mesa, IReglas<T> reglas)
        {
            if (mesa.Count() == 0)
            {
                mesa.Add(_almacenMano[0]);

                _estado = 0;

                _almacenMano.Remove(_almacenMano[0]);

                return;
            }

            T cara1 = mesa[0][0];
            T cara2 = mesa[mesa.Count() - 1][1];

            for (int i = 0; i < _almacenMano.Count(); i++)
            {
                if (reglas.JugadaValida(mesa, _almacenMano[i]))
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (_almacenMano[i][j].Equals(cara2))
                        {
                            _almacenMano[i] = OrdenaFicha(cara2, _almacenMano[i]);

                            mesa.Add(_almacenMano[i]);

                            _estado = 0;

                            _almacenMano.Remove(_almacenMano[i]);

                            return;
                        }

                        if (_almacenMano[i][j].Equals(cara1))
                        {
                            _almacenMano[i] = OrdenaFicha(cara1, _almacenMano[i]);

                            Array.Reverse(_almacenMano[i]);

                            mesa.Insert(0, _almacenMano[i]);

                            _estado = 0;

                            _almacenMano.Remove(_almacenMano[i]);

                            return;
                        }
                    }

                }
            }

            System.Console.WriteLine("EL JUGADOR NO LLEVA ☺");

            _estado++;
            _pases++;
        }

        public void RecibeMano(List<T[]> mano)
        {
            _almacenMano = mano;
        }

        public int GetEstado { get { return _estado; } }
        public List<T[]> GetAlmacenMano { get { return _almacenMano; } }
        public int GetPases { get { return _pases; } }

        //metodos de la clase
        private T[] OrdenaFicha(T cara, T[] ficha)
        {
            if (cara.Equals(ficha[0]))
                return ficha;

            Array.Reverse(ficha);

            return ficha;
        }

        // campos
        private int _estado;
        public List<T[]> _almacenMano;
        private int _pases;
    }
}