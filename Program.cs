using Juego;
using TipoDomino;

namespace ProyectoDomino
{
    class Program
    {
        // Machi-Domino
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Banco<string> banco = new Banco<string>();
            Menu<string> menu = new Menu<string>();

            Clasico<string> clasico = new Clasico<string>();
            Longana<string> longana = new Longana<string>();

            Domino<string>.FlujoJuego(banco, menu);

            if (Domino<string>._domino == 1)
                VerJuego(clasico);

            if (Domino<string>._domino == 2)
                VerJuego(longana);
        }

        // metodos de la clase
        private static void VerJuego(ITipoJuego<string> tipoJuego)
        {
            tipoJuego.Juego(Domino<string>._auxiliarReglas);
        }
    }
}
