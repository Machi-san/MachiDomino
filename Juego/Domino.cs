using Cierre;
using Jugadores;
using ProyectoDomino;
using Fin;

namespace Juego
{
    public static class Domino<T> where T : IComparable, IEquatable<T>
    {
        static Domino()
        {
            _mesa = new List<T[]>();
            _elementosFichas = new List<object>();
            _auxiliarJugadores = new List<IJugador<T>>();
            _auxiliarReglas = new Reglas<T>();
            _auxiliarCierreClasico = new CierreClasico<T>();
            _auxiliarCierreBonificado = new CierreBonificado<T>();
            _auxiliarFinClasico = new FinClasico<T>();
            _auxiliarFinPersonalizado = new FinPersonalizado<T>();
            _criteriosFin = new List<IFin<T>>();
            _criteriosGanadorCierre = new List<IGanadorCierre<T>>();

            _criteriosFin.Add(_auxiliarFinClasico);
            _criteriosFin.Add(_auxiliarFinPersonalizado);

            _criteriosGanadorCierre.Add(_auxiliarCierreClasico);
            _criteriosGanadorCierre.Add(_auxiliarCierreBonificado);

            /*_elementosFichas.Add(0);
            _elementosFichas.Add(1);
            _elementosFichas.Add(2);
            _elementosFichas.Add(3);
            _elementosFichas.Add(4);
            _elementosFichas.Add(5);
            _elementosFichas.Add(6);
            _elementosFichas.Add(7);
            _elementosFichas.Add(8);
            _elementosFichas.Add(9);*/

            _elementosFichas.Add("☺");
            _elementosFichas.Add("☻");
            _elementosFichas.Add("♥");
            _elementosFichas.Add("♦");
            _elementosFichas.Add("♣");
            _elementosFichas.Add("♠");
            _elementosFichas.Add("++");
            _elementosFichas.Add("**");
            _elementosFichas.Add("$$");
            _elementosFichas.Add("&&");
        }

        // Guia del juego
        public static void FlujoJuego(IBanco<T> banco, Menu<T> menu)
        {
            string palabra = "";

            menu.Presentacion();

            menu.TipoJuego();
            palabra = Console.ReadLine();

            while (!Domino<string>.EsNumero(palabra) || (palabra != "1" && palabra != "2"))
            {
                System.Console.WriteLine("La opcion no es valida");
                palabra = Console.ReadLine();
            }

            _domino = int.Parse(palabra);

            menu.VarianteDoble();
            palabra = Console.ReadLine();

            while (!EsNumero(palabra) || palabra == "")
            {
                System.Console.WriteLine("La opcion no es valida");
                palabra = Console.ReadLine();
            }

            _variante = int.Parse(palabra);

            while (_variante > _elementosFichas.Count() - 1 || _variante < 1)
            {
                System.Console.WriteLine($"La opcion no esta entre las sugeridas");
                palabra = Console.ReadLine();

                while (!EsNumero(palabra) || palabra == "")
                {
                    System.Console.WriteLine("La opcion no es valida");
                    palabra = Console.ReadLine();
                }

                _variante = int.Parse(palabra);
            }

            Console.Clear();

            menu.CantidadJugadores();
            palabra = Console.ReadLine();

            while (!EsNumero(palabra) || palabra == "")
            {
                System.Console.WriteLine("La opcion no es valida");
                palabra = Console.ReadLine();
            }

            _cantidadJugadores = int.Parse(palabra);

            banco.GeneraFichas(_elementosFichas, _variante);

            int totalFichas = banco.GetAlmacenFichas.Count();

            while (_cantidadJugadores > totalFichas)
            {
                System.Console.WriteLine($"Ingrese un numero limitado de jugadores hasta {totalFichas}");
                palabra = Console.ReadLine();

                while (!EsNumero(palabra) || palabra == "")
                {
                    System.Console.WriteLine("La opcion no es valida");
                    palabra = Console.ReadLine();
                }

                _cantidadJugadores = int.Parse(palabra);
            }

            Console.Clear();

            menu.ManoInicial(_variante);
            palabra = Console.ReadLine();

            while (!EsNumero(palabra) || palabra == "")
            {
                System.Console.WriteLine("La opcion no es valida");
                palabra = Console.ReadLine();
            }

            _mano = int.Parse(palabra);

            while (_mano > FichasMano(_cantidadJugadores, totalFichas) || _mano < 1)
            {
                System.Console.WriteLine($"Ingrese un numero de fichas limitado hasta {(int)(totalFichas / _cantidadJugadores)}");
                palabra = Console.ReadLine();

                while (!EsNumero(palabra) || palabra == "")
                {
                    System.Console.WriteLine("La opcion no es valida");
                    palabra = Console.ReadLine();
                }

                _mano = int.Parse(palabra);
            }

            Console.Clear();

            menu.EstrategiaJugador();
            palabra = Console.ReadLine();

            while (!EsNumero(palabra) || palabra == "")
            {
                System.Console.WriteLine("La opcion no es valida");
                palabra = Console.ReadLine();
            }

            int estrategia = int.Parse(palabra); ;

            for (int i = 0; i < _cantidadJugadores; i++)
            {
                while (estrategia < 1 || estrategia > 3)
                {
                    System.Console.WriteLine("Ingrese una opcion valida");
                    palabra = Console.ReadLine();

                    while (!EsNumero(palabra) || palabra == "")
                    {
                        System.Console.WriteLine("La opcion no es valida");
                        palabra = Console.ReadLine();
                    }

                    estrategia = int.Parse(palabra);
                }

                if (estrategia == 1)
                {
                    JugadorBotaGorda<T> jugador1 = new JugadorBotaGorda<T>();

                    _auxiliarJugadores.Add(jugador1);

                    if (i == _cantidadJugadores - 1)
                        break;

                    palabra = Console.ReadLine();

                    while (!EsNumero(palabra) || palabra == "")
                    {
                        System.Console.WriteLine("La opcion no es valida");
                        palabra = Console.ReadLine();
                    }

                    estrategia = int.Parse(palabra);

                    continue;
                }

                if (estrategia == 2)
                {
                    JugadorRandom<T> jugador2 = new JugadorRandom<T>();

                    _auxiliarJugadores.Add(jugador2);

                    if (i == _cantidadJugadores - 1)
                        break;

                    palabra = Console.ReadLine();

                    while (!EsNumero(palabra) || palabra == "")
                    {
                        System.Console.WriteLine("La opcion no es valida");
                        palabra = Console.ReadLine();
                    }

                    estrategia = int.Parse(palabra);

                    continue;
                }

                if (estrategia == 3)
                {
                    JugadorUsuario<T> jugador3 = new JugadorUsuario<T>();

                    _auxiliarJugadores.Add(jugador3);

                    if (i == _cantidadJugadores - 1)
                        break;

                    palabra = Console.ReadLine();

                    while (!EsNumero(palabra) || palabra == "")
                    {
                        System.Console.WriteLine("La opcion no es valida");
                        palabra = Console.ReadLine();
                    }

                    estrategia = int.Parse(palabra);

                    continue;
                }
            }

            banco = new Banco<T>();

            banco.GeneraFichas(_elementosFichas, _variante);

            menu.Continuacion();

            System.Console.WriteLine("Decida quien realiza la primera jugada");

            menu.CriterioDeSalida(1, _cantidadJugadores);
            palabra = Console.ReadLine();

            while (!EsNumero(palabra) || palabra == "")
            {
                System.Console.WriteLine("La opcion no es valida");
                palabra = Console.ReadLine();
            }

            _criterioSalida = int.Parse(palabra);

            while (_criterioSalida > _cantidadJugadores || _criterioSalida < 1)
            {
                System.Console.WriteLine("Ingrese una de las opciones antes establecidas");
                palabra = Console.ReadLine();

                while (!EsNumero(palabra) || palabra == "")
                {
                    System.Console.WriteLine("La opcion no es valida");
                    palabra = Console.ReadLine();
                }

                _criterioSalida = int.Parse(palabra);
            }

            menu.CriterioDeVictoria();
            palabra = Console.ReadLine();

            while (!EsNumero(palabra) || palabra == "")
            {
                System.Console.WriteLine("La opcion no es valida");
                palabra = Console.ReadLine();
            }

            _criterioVictoria = int.Parse(palabra);

            while (_criterioVictoria != 1 && _criterioVictoria != 2)
            {
                System.Console.WriteLine("Ingrese una de las opciones antes establecidas");
                palabra = Console.ReadLine();

                while (!EsNumero(palabra) || palabra == "")
                {
                    System.Console.WriteLine("La opcion no es valida");
                    palabra = Console.ReadLine();
                }

                _criterioVictoria = int.Parse(palabra);
            }

            menu.CriterioFin();
            palabra = Console.ReadLine();

            while (!EsNumero(palabra) || palabra == "")
            {
                System.Console.WriteLine("La opcion no es valida");
                palabra = Console.ReadLine();
            }

            _criterioFin = int.Parse(palabra);

            while (_criterioFin != 1 && _criterioFin != 2)
            {
                System.Console.WriteLine("Ingrese una de las opciones antes establecidas");
                palabra = Console.ReadLine();

                while (!EsNumero(palabra) || palabra == "")
                {
                    System.Console.WriteLine("La opcion no es valida");
                    palabra = Console.ReadLine();
                }

                _criterioFin = int.Parse(palabra);
            }

            banco.GeneraMano(_auxiliarJugadores);

            menu.MensajeFinal();
        }

        // metodos de la clase
        private static int FichasMano(int cantidadJugadores, int totalFichas)
        {
            return (int)(totalFichas / cantidadJugadores);
        }

        public static bool EsNumero(string entrada)
        {
            for (int i = 0; i < entrada.Length; i++)
            {
                if (entrada[i] != '0' && entrada[i] != '1' && entrada[i] != '2' && entrada[i] != '3' && entrada[i] != '4' && entrada[i] != '5' && entrada[i] != '6' && entrada[i] != '7' && entrada[i] != '8' && entrada[i] != '9')
                    return false;
            }

            return true;
        }

        public static void PrintMesa()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;

            System.Console.WriteLine("☻☻☻☻☻ MESA ☻☻☻☻☻");

            for (int i = 0; i < Domino<T>._mesa.Count(); i++)
                Console.Write($"[{_mesa[i][0]} <|> {_mesa[i][1]}]");

            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine();
        }

        public static void PrintMano(int i)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;

            System.Console.WriteLine($"♣♣♣♣♣ MANO DEL JUGADOR {i + 1} ♣♣♣♣♣");
            System.Console.WriteLine("♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠");

            for (int j = 0; j < Domino<T>._auxiliarJugadores[i].GetAlmacenMano.Count(); j++)
                Console.Write($"[{_auxiliarJugadores[i].GetAlmacenMano[j][0]} <|> {_auxiliarJugadores[i].GetAlmacenMano[j][1]}]  ");

            System.Console.WriteLine();
            System.Console.WriteLine("♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠♠");
        }

        //campos
        public static List<object> _elementosFichas;
        public static int _cantidadJugadores;
        public static int _variante;
        public static int _mano;
        public static List<T[]> _mesa;
        public static List<IJugador<T>> _auxiliarJugadores;
        public static int _criterioSalida;
        public static Reglas<T> _auxiliarReglas;
        public static int _criterioVictoria;
        public static int _domino;
        private static CierreClasico<T> _auxiliarCierreClasico;
        private static CierreBonificado<T> _auxiliarCierreBonificado;
        private static FinClasico<T> _auxiliarFinClasico;
        private static FinPersonalizado<T> _auxiliarFinPersonalizado;
        public static List<IFin<T>> _criteriosFin;
        public static List<IGanadorCierre<T>> _criteriosGanadorCierre;
        public static int _criterioFin;
    }
}
