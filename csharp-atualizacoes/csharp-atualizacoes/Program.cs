using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_atualizacoes
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] menus = new string[] {
                "1. Propriedades Automáticas Somente-Leitura",
                "2. Inicializadores De Propriedade Automática",
                "3. Membros Com Corpo De Expressão"
            };

            Console.WriteLine("ÍNDICE DE PROGRAMAS");
            Console.WriteLine("===================");


            int programa = 0;
            string line;
            do
            {
                foreach (var menu in menus)
                {
                    Console.WriteLine(menu);
                }

                Console.WriteLine();
                Console.WriteLine("Escolha um programa:");

                line = Console.ReadLine();
                Int32.TryParse(line, out programa);
                switch (programa)
                {
                    case 1:
                        new CSharp6.R01.Programa().Main();
                        break;
                    case 2:
                        new CSharp6.R02.Programa().Main();
                        break;
                    case 3:
                        new CSharp6.R03.Programa().Main();
                        break;
                    default:
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("PRESSIONE UMA TECLA PARA CONTINUAR...");
                Console.ReadKey();
                Console.Clear();
            } while (line.Length > 0);
        }
    }
}
