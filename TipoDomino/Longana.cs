using Juego;
using ProyectoDomino;

namespace TipoDomino
{
    public class Longana<T> : ITipoJuego<T> where T : IComparable, IEquatable<T>
    {
        // metodos de la clase
        public void Juego(IReglas<T> reglas)
        {
            int contadorSalida = 0;
            int contadorFin = 0;

            while (true)
            {
                for (int i = 0; i < Domino<T>._auxiliarJugadores.Count(); i++)
                {
                    contadorSalida++;

                    if (contadorSalida < Domino<T>._criterioSalida)
                        continue;

                    Domino<T>.PrintMesa();

                    Domino<T>.PrintMano(i);

                    int auxiliarMesa = Domino<T>._mesa.Count();

                    Domino<T>._auxiliarJugadores[i].Juega(Domino<T>._mesa, reglas);

                    Console.ReadKey();
                    Console.Clear();

                    if (Domino<T>._auxiliarReglas.Victoria(Domino<T>._auxiliarJugadores[i].GetAlmacenMano))
                    {
                        Console.Clear();

                        System.Console.WriteLine($"EL JUGADOR {i + 1} ES EL GANADOR ♥");
                        System.Console.WriteLine();

                        Domino<T>.PrintMesa();

                        contadorFin++;

                        break;
                    }

                    if (auxiliarMesa != Domino<T>._mesa.Count())
                    {
                        i--;

                        System.Console.WriteLine();

                        continue;
                    }

                    if (Domino<T>._auxiliarReglas.Fin(Domino<T>._criteriosFin[Domino<T>._criterioFin - 1], Domino<T>._auxiliarJugadores))
                    {
                        Console.Clear();

                        List<int> ganadores = new List<int>();
                        ganadores = Domino<T>._auxiliarReglas.GanadorCierre(Domino<T>._criteriosGanadorCierre[Domino<T>._criterioVictoria - 1], Domino<T>._auxiliarJugadores);

                        for (int j = 0; j < ganadores.Count(); j++)
                            System.Console.WriteLine($"EL JUGADOR {ganadores[j] + 1} ES EL GANADOR ♥ => PASES: {Domino<T>._auxiliarJugadores[ganadores[j]].GetPases}");

                        System.Console.WriteLine();

                        for (int j = 0; j < Domino<T>._auxiliarJugadores.Count(); j++)
                        {
                            Domino<T>.PrintMano(j);

                            System.Console.WriteLine();
                        }

                        Domino<T>.PrintMesa();

                        contadorFin++;

                        break;
                    }
                }

                if (contadorFin != 0)
                    break;
            }
        }
    }
}