using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp6.R02
{
    class Programa
    {
        public void Main()
        {
            Console.WriteLine("2. Inicializadores De Propriedade Automática");

            Aluno aluno = new Aluno("Marty", "McFly", new DateTime(1968, 6, 12));
            Console.WriteLine(aluno.Nome);
            Console.WriteLine(aluno.Sobrenome);

            Console.WriteLine(aluno.ToString());
        }
    }

    class Aluno
    {
        public string Nome { get; }

        public string Sobrenome { get; }

        public DateTime DataNascimento { get; } = new DateTime(1990, 1, 1);

        public Aluno(string nome, string sobrenome)
        {
            Nome = nome;
            Sobrenome = sobrenome;
        }
        public Aluno(string nome, string sobrenome, DateTime dataNascimento) :
            this(nome, sobrenome)
        {
            this.DataNascimento = dataNascimento;
        }

    }
}
