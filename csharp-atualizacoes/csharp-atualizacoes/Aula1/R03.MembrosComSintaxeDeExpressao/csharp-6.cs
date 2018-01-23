using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp6.R03
{
    class Programa
    {
        public void Main()
        {
            Console.WriteLine("3. Membros Com Corpo De Expressão");

            Aluno aluno = new Aluno("Marty", "McFly", new DateTime(1968, 6, 12));
            Console.WriteLine(aluno.Nome);
            Console.WriteLine(aluno.Sobrenome);

            Console.WriteLine(aluno.NomeCompleto);
            Console.WriteLine("Idade: {0}", aluno.GetIdade());
        }
    }

    class Aluno
    {
        public string Nome { get; }

        public string Sobrenome { get; }

        public DateTime DataNascimento { get; } = new DateTime(1990, 1, 1);

        public string NomeCompleto => Nome + " " + Sobrenome; 
        
        public int GetIdade()
            => (int)(((DateTime.Now - DataNascimento).TotalDays) / 365.242199);

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
