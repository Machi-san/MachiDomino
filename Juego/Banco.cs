using ProyectoDomino;

namespace Juego
{
    public class Banco<T> : IBanco<T> where T : IComparable, IEquatable<T>
    {
        public Banco()
        {
            _almacenFichas = new List<T[]>();
        }

        // metodos de la interfaz IBanco
        public void GeneraFichas(List<object> listaElementos, int variante)
        {
            for (int i = 0; i <= variante; i++)
            {
                for (int j = 0; j <= variante; j++)
                {
                    T[] ficha = new T[] { (T)listaElementos[i], (T)listaElementos[j] };

                    Array.Sort(ficha);

                    _almacenFichas.Add(ficha);
                }
            }

            _almacenFichas = EliminaElementosRepetidos(_almacenFichas);
        }

        public void GeneraMano(List<IJugador<T>> jugadores)
        {
            Random random = new Random();

            for (int i = 0; i < Domino<T>._cantidadJugadores; i++)
            {
                List<T[]> auxiliar = new List<T[]>();

                for (int j = 0; j < Domino<T>._mano; j++)
                {
                    int auxiliarRandom = random.Next(_almacenFichas.Count());

                    auxiliar.Add(_almacenFichas[auxiliarRandom]);

                    _almacenFichas.RemoveAt(auxiliarRandom);
                }

                jugadores[i].RecibeMano(auxiliar);
            }
        }

        // metodos de la clase
        private List<T[]> EliminaElementosRepetidos(List<T[]> array)
        {
            List<T[]> auxiliar = new List<T[]>();

            auxiliar.Add(array[0]);

            for (int i = 0; i < array.Count(); i++)
            {
                int contador = 0;

                for (int j = 0; j < auxiliar.Count(); j++)
                {
                    if (IgualaArrays(auxiliar[j], array[i]))
                        break;

                    contador++;
                }

                if (contador == auxiliar.Count)
                    auxiliar.Add(array[i]);
            }

            return auxiliar;
        }

        private bool IgualaArrays(T[] array1, T[] array2)
        {
            for (int i = 0; i < array1.Length; i++)
            {
                if (!array1[i].Equals(array2[i]))
                    return false;
            }

            return true;
        }

        // campos
        private List<T[]> _almacenFichas;

        // metodos de acceso
        public List<T[]> GetAlmacenFichas { get { return _almacenFichas; } }
    }
}