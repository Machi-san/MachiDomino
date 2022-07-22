using Juego;
using ProyectoDomino;

namespace Jugadores
{
    public class JugadorBotaGorda<T> : IJugador<T> where T : IComparable, IEquatable<T>
    {
        public JugadorBotaGorda()
        {
            _almacenMano = new List<T[]>();
        }

        // metodos de la interfaz IJugador
        public void Juega(List<T[]> mesa, IReglas<T> reglas)
        {
            OrdenaMano();

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

        // metodos de la clase
        private T[] OrdenaFicha(T cara, T[] ficha)
        {
            if (cara.Equals(ficha[0]))
                return ficha;

            Array.Reverse(ficha);

            return ficha;
        }

        private void OrdenaMano()
        {
            List<T> caras = new List<T>();

            for (int i = 0; i < Domino<T>._elementosFichas.Count(); i++)
                caras.Add((T)Domino<T>._elementosFichas[i]);

            caras.Sort();

            Dictionary<T, int> valorCaras = new Dictionary<T, int>();

            for (int i = 0; i < caras.Count(); i++)
                valorCaras.Add(caras[i], i);

            Dictionary<T[], int> valorFichas = new Dictionary<T[], int>();

            for (int i = 0; i < _almacenMano.Count(); i++)
                valorFichas.Add(_almacenMano[i], Sumatoria(ConvierteFicha(_almacenMano[i], valorCaras[_almacenMano[i][0]], valorCaras[_almacenMano[i][1]])));

            List<int> almacenValoresFichas = new List<int>();

            for (int i = 0; i < _almacenMano.Count(); i++)
                almacenValoresFichas.Add(valorFichas[_almacenMano[i]]);

            almacenValoresFichas.Sort();
            almacenValoresFichas.Reverse();

            List<T[]> auxiliar = new List<T[]>();

            for (int i = 0; i < _almacenMano.Count(); i++)
            {
                auxiliar.Add(GetLlave(valorFichas, almacenValoresFichas[i]));

                valorFichas.Remove(GetLlave(valorFichas, almacenValoresFichas[i]));
            }

            _almacenMano = auxiliar;
        }

        private int[] ConvierteFicha(T[] ficha, int izquierda, int derecha)
        {
            return new int[] { izquierda, derecha };
        }

        private int Sumatoria(int[] array)
        {
            int contador = 0;

            for (int i = 0; i < array.Length; i++)
                contador += array[i];

            return contador;
        }

        private T[] GetLlave(Dictionary<T[], int> diccionario, int valor)
        {
            T[] auxiliar = new T[0];

            foreach (var i in diccionario)
            {
                if (i.Value == valor)
                    auxiliar = i.Key;
            }

            return auxiliar;
        }

        // campos
        private int _estado;
        private List<T[]> _almacenMano;
        private int _pases;
    }
}