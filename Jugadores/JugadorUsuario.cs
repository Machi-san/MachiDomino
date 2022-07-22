using Juego;
using ProyectoDomino;

namespace Jugadores
{
    public class JugadorUsuario<T> : IJugador<T> where T : IComparable, IEquatable<T>
    {
        public JugadorUsuario()
        {
            _almacenMano = new List<T[]>();
        }

        // metodos de la interfaz IJugador
        public void RecibeMano(List<T[]> mano)
        {
            _almacenMano = mano;
        }

        public void Juega(List<T[]> mesa, IReglas<T> reglas)
        {
            string palabra = "";

            if (NoLleva(_almacenMano, mesa, mesa.Count()))
            {
                System.Console.WriteLine("EL JUGADOR NO LLEVA ☺");

                _estado++;
                _pases++;

                return;
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Para jugar una ficha marque el numero correspondiente a la posicion de la ficha en su mano");
            System.Console.WriteLine("Tome como referencia que la primera ficha corresponde a la posicion 0");
            palabra = Console.ReadLine();

            while (!Domino<T>.EsNumero(palabra) || palabra == "")
            {
                System.Console.WriteLine("La opcion no es valida");
                palabra = Console.ReadLine();
            }

            int jugada = int.Parse(palabra);

            while (jugada < 0 || jugada >= _almacenMano.Count())
            {
                System.Console.WriteLine("La jugada no es valida");
                palabra = Console.ReadLine();

                while (!Domino<T>.EsNumero(palabra) || palabra == "")
                {
                    System.Console.WriteLine("La opcion no es valida");
                    palabra = Console.ReadLine();
                }

                jugada = int.Parse(palabra);
            }

            if (mesa.Count() == 0)
            {
                mesa.Add(_almacenMano[jugada]);

                _almacenMano.Remove(_almacenMano[jugada]);

                _estado = 0;

                return;
            }

            while (true)
            {
                if (reglas.JugadaValida(mesa, _almacenMano[jugada]))
                {
                    System.Console.WriteLine("Indique con la letra (i) si desea jugar por la izquierda y con la letra (d) si desea jugar por la derecha");
                    string decision = Console.ReadLine();

                    while (decision.ToLower() != "i" && decision.ToLower() != "d")
                    {
                        System.Console.WriteLine("La opcion no es valida");
                        decision = Console.ReadLine();
                    }

                    if (decision.ToLower() == "d")
                    {
                        if (!CabeFicha(mesa[mesa.Count() - 1][1], _almacenMano[jugada]))
                        {
                            System.Console.WriteLine("La opcion no es valida");
                            continue;
                        }

                        _almacenMano[jugada] = OrdenaFicha(mesa[mesa.Count() - 1][1], _almacenMano[jugada]);

                        mesa.Add(_almacenMano[jugada]);

                        _estado = 0;

                        _almacenMano.Remove(_almacenMano[jugada]);

                        return;
                    }

                    if (decision.ToLower() == "i")
                    {
                        if (!CabeFicha(mesa[0][0], _almacenMano[jugada]))
                        {
                            System.Console.WriteLine("La opcion no es valida");
                            continue;
                        }

                        _almacenMano[jugada] = OrdenaFicha(mesa[0][0], _almacenMano[jugada]);

                        Array.Reverse(_almacenMano[jugada]);

                        mesa.Insert(0, _almacenMano[jugada]);

                        _estado = 0;

                        _almacenMano.Remove(_almacenMano[jugada]);

                        return;
                    }
                }

                System.Console.WriteLine("La jugada no es valida");
                palabra = Console.ReadLine();

                while (!Domino<T>.EsNumero(palabra) || palabra == "")
                {
                    System.Console.WriteLine("La opcion no es valida");
                    palabra = Console.ReadLine();
                }

                jugada = int.Parse(palabra);

                while (jugada < 0 || jugada >= _almacenMano.Count())
                {
                    System.Console.WriteLine("La jugada no es valida");
                    palabra = Console.ReadLine();

                    while (!Domino<T>.EsNumero(palabra) || palabra == "")
                    {
                        System.Console.WriteLine("La opcion no es valida");
                        palabra = Console.ReadLine();
                    }

                    jugada = int.Parse(palabra);
                }
            }
        }

        public int GetEstado { get { return _estado; } }
        public List<T[]> GetAlmacenMano { get { return _almacenMano; } }
        public int GetPases { get { return _pases; } }

        // metodos de la clase
        private bool NoLleva(List<T[]> mano, List<T[]> mesa, int fichasMesa)
        {
            if (fichasMesa != 0)
            {
                T izquierda = mesa[0][0];
                T derecha = mesa[mesa.Count() - 1][1];

                for (int i = 0; i < mano.Count; i++)
                {
                    if (mano[i][0].Equals(izquierda) || mano[i][1].Equals(izquierda) || mano[i][0].Equals(derecha) || mano[i][1].Equals(derecha))
                        return false;
                }

                return true;
            }

            return false;
        }

        private bool CabeFicha(T cabeza, T[] ficha)
        {
            foreach (T i in ficha)
            {
                if (i.Equals(cabeza))
                    return true;
            }

            return false;
        }

        private T[] OrdenaFicha(T cara, T[] ficha)
        {
            if (cara.Equals(ficha[0]))
                return ficha;

            Array.Reverse(ficha);

            return ficha;
        }

        // campos
        private List<T[]> _almacenMano;
        private int _estado;
        private int _pases;
    }
}