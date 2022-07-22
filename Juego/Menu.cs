namespace Juego
{
    public class Menu<T> where T : IComparable, IEquatable<T>
    {
        public void TipoJuego()
        {
            System.Console.WriteLine("Que tipo de domino desea jugar");
            System.Console.WriteLine("1. Clasico \n(Se refiere al domino clasico conocido)");
            System.Console.WriteLine("2. Longana \n(Personalizacion donde cada jugador puede jugar siempre que pueda sin pasar de turno");
        }

        public void Presentacion()
        {
            Console.Clear();
            System.Console.WriteLine("Hola! Bienvenido a Machi-Domino. Esta a punto de comenzar una partida de domino virtual \nResponda las siguientes interrogantes");
            System.Console.WriteLine("----------------------------------------------------------------------------------------");
        }

        public void CantidadJugadores()
        {
            System.Console.WriteLine("Con cuantos jugadores desea iniciar su partida");
        }

        public void ManoInicial(int variante)
        {
            System.Console.WriteLine("Cuantas fichas desea que tenga cada jugador en su mano");
        }

        public void Continuacion()
        {
            Console.Clear();

            System.Console.WriteLine("Perfecto. Ahora ingrese algunas de las personalizaciones que desee");
            System.Console.WriteLine("---------------------------------------------------------------------");
        }

        public void CriterioDeSalida(int indice, int opcionUsuario)
        {
            System.Console.WriteLine($"Jugador {indice}");

            if (indice == opcionUsuario)
                return;

            CriterioDeSalida(indice + 1, opcionUsuario);
        }

        public void MensajeFinal()
        {
            Console.Clear();

            System.Console.WriteLine("Gracias por elegir Machi-Domino. Disfrute la partida ♥");
            System.Console.WriteLine("Pulse una letra para continuar => => => => =>");

            Console.ReadKey();
            Console.Clear();
        }

        public void EstrategiaJugador()
        {
            System.Console.WriteLine("Que tipo de estrategia desea implementar para cada jugador");
            System.Console.WriteLine("1. Bota gorda");
            System.Console.WriteLine("2. Random");
            System.Console.WriteLine("3. Usuario");
        }

        public void VarianteDoble()
        {
            Console.Clear();

            System.Console.WriteLine("Que variante desea utilizar (variante es el numero que refleja el tipo de data y de este dependen la cantidad de fichas)");

            Banco<T> banco = new Banco<T>();

            List<string> variantes = new List<string>();

            for (int i = 1; i < Domino<T>._elementosFichas.Count(); i++)
            {
                banco.GeneraFichas(Domino<T>._elementosFichas, i);

                int totalFichas = banco.GetAlmacenFichas.Count();

                variantes.Add($"{i} - {totalFichas} fichas");
            }

            foreach (var i in variantes)
                System.Console.WriteLine(i);

        }

        public void CriterioDeVictoria()
        {
            Console.Clear();

            System.Console.WriteLine("Que criterio de victoria desea utilizar cuando el juego finalice y ningun jugador pueda realizar jugadas");
            System.Console.WriteLine("1. Clasico \n(Gana el jugador que tenga la menor cantidad de puntos)");
            System.Console.WriteLine("2. Bonificado \n(Gana el jugador que menos se ha pasado)");
        }

        public void CriterioFin()
        {
            Console.Clear();

            System.Console.WriteLine("Bajo que condiciones desea que finalice la partida");
            System.Console.WriteLine("1. Clasico \n(Se refiere al criterio de fin clasico cuando ningun jugador puede realizar jugadas)");
            System.Console.WriteLine("2. Personalizado \n(El juego termina cuando al menos un jugador tiene solo una ficha)");
        }
    }
}