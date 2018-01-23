using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.String;
using static System.DateTime;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Collections;

namespace CSharp6.R11
{
    class Programa
    {
        public async void Main()
        {
            WriteLine("11. Metodos De Extensão Para Inicializadores De Coleção");

            StreamWriter logAplicacao = new StreamWriter("LogAplicacao.txt");

            try
            {
                await logAplicacao.WriteLineAsync("Aplicação está iniciando...");
                Aluno aluno = new Aluno("Marty", "McFly", new DateTime(1968, 6, 12))
                {
                    Endereco = "9303 Lyon Drive Hill Valley CA",
                    Telefone = "555-4385"
                };

                await logAplicacao.WriteLineAsync("Aluno Marty McFly foi criado...");

                WriteLine(aluno.Nome);
                WriteLine(aluno.Sobrenome);

                WriteLine(aluno.NomeCompleto);
                WriteLine("Idade: {0}", aluno.GetIdade());
                WriteLine(aluno.DadosPessoais);

                aluno.AdicionarAvaliacao(new Avaliacao(1, "GEO", 8));
                aluno.AdicionarAvaliacao(new Avaliacao(1, "MAT", 7));
                aluno.AdicionarAvaliacao(new Avaliacao(1, "HIS", 9));

                foreach (var avaliacao in aluno.Avaliacoes)
                {
                    Console.WriteLine(avaliacao.ToString());
                }

                ImprimirMelhorNota(aluno);

                Aluno aluno2 = new Aluno("Bart", "Simpson");
                await logAplicacao.WriteLineAsync("Aluno Bart Simpson foi criado...");
                ImprimirMelhorNota(aluno2);

                aluno.PropertyChanged += Aluno_PropertyChanged;

                aluno.Endereco = "Rua Vergueiro, 3185";
                aluno.Telefone = "555-1234";

                Aluno aluno3 = new Aluno("Charlie", "Brown");
                await logAplicacao.WriteLineAsync("Aluno Charlie Brown foi criado...");

                ListaDeMatricula listaDeMatricula = new ListaDeMatricula
                {
                    aluno, aluno2, aluno3
                };

                Console.WriteLine("ALUNOS DA LISTA");
                Console.WriteLine("===============");

                foreach (var a in listaDeMatricula)
                {
                    Console.WriteLine(a.DadosPessoais);
                }
            }
            catch (ArgumentException exc) when (exc.Message.Contains("não informado"))
            {
                string msg = $"Parâmetro {exc.ParamName} não foi informado!";
                await logAplicacao.WriteLineAsync(msg);
                Console.WriteLine(msg);
            }
            catch (ArgumentException exc)
            {
                const string msg = "Parâmetro com problema!";
                await logAplicacao.WriteLineAsync(msg);
                Console.WriteLine(msg);
            }
            catch (Exception exc)
            {
                await logAplicacao.WriteLineAsync(exc.ToString());
                Console.WriteLine(exc.ToString());
            }
            finally
            {
                await logAplicacao.WriteLineAsync("Aplicação terminou.");
                logAplicacao.Dispose();
            }
        }

        private void Aluno_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine($"Propriedade {e.PropertyName} foi alterada!");
        }

        private static void ImprimirMelhorNota(Aluno aluno)
        {
            Console.WriteLine("Melhor nota: {0}",
                aluno?.MelhorAvaliacao?.Nota);
        }
    }

    class Aluno : INotifyPropertyChanged
    {
        public string Nome { get; }

        public string Sobrenome { get; }

        private string endereco;

        public string Endereco
        {
            get { return endereco; }
            set
            {
                if (endereco != value)
                {
                    endereco = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DadosPessoais));
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string telefone;

        public string Telefone
        {
            get { return telefone; }
            set
            {
                if (telefone != value)
                {
                    telefone = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DadosPessoais));
                }
            }
        }


        public string DadosPessoais =>
                 $"Nome: {NomeCompleto}, Endereço: {Endereco}, Telefone: {Telefone}, Data de Nascimento: {DataNascimento:dd/MM/yyyy}";

        public DateTime DataNascimento { get; } = new DateTime(1990, 1, 1);

        public string NomeCompleto => Nome + " " + Sobrenome;

        public int GetIdade()
            => (int)(((Now - DataNascimento).TotalDays) / 365.242199);

        public Aluno(string nome, string sobrenome)
        {
            VerificarParametroPreenchido(nome, nameof(nome));
            VerificarParametroPreenchido(sobrenome, nameof(sobrenome));

            Nome = nome;
            Sobrenome = sobrenome;
        }

        private static void VerificarParametroPreenchido(string valorParametro, string nomeParametro)
        {
            if (IsNullOrEmpty(valorParametro))
            {
                throw new ArgumentException("Parâmetro não informado!", nomeParametro);
            }
        }

        public Aluno(string nome, string sobrenome, DateTime dataNascimento) :
            this(nome, sobrenome)
        {
            this.DataNascimento = dataNascimento;
        }

        private IList<Avaliacao> avaliacoes = new List<Avaliacao>();

        public event PropertyChangedEventHandler PropertyChanged;

        public IReadOnlyCollection<Avaliacao> Avaliacoes
            => new ReadOnlyCollection<Avaliacao>(avaliacoes);

        public void AdicionarAvaliacao(Avaliacao avaliacao)
        {
            avaliacoes.Add(avaliacao);
        }

        public Avaliacao MelhorAvaliacao =>
            avaliacoes.OrderBy(a => a.Nota).LastOrDefault();
    }

    class Avaliacao
    {
        Dictionary<string, string> materias = new Dictionary<string, string>
        {
            ["MAT"] = "Matemática",
            ["LPL"] = "Língua Portuguesa",
            ["FIS"] = "Física",
            ["HIS"] = "História",
            ["GEO"] = "Geografia",
            ["QUI"] = "Química",
            ["BIO"] = "Biologia"
        };

        public Avaliacao(int bimestre, string codigoMateria, double nota)
        {
            Bimestre = bimestre;
            CodigoMateria = codigoMateria;
            Nota = nota;
        }

        public int Bimestre { get; }
        public string CodigoMateria { get; }
        public double Nota { get; }

        public override string ToString()
        {
            return $"Bimestre: {Bimestre}, Materia: {materias[CodigoMateria]}" +
                $", Nota: {Nota}";
        }
    }

    class ListaDeMatricula : IEnumerable<Aluno>
    {
        private List<Aluno> alunos = new List<Aluno>();

        public IEnumerator<Aluno> GetEnumerator()
        {
            return ((IEnumerable<Aluno>)alunos).GetEnumerator();
        }

        public void Matricular(Aluno aluno)
        {
            alunos.Add(aluno);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Aluno>)alunos).GetEnumerator();
        }
    }

    static class AlunoExtensions
    {
        public static void Add(this ListaDeMatricula lista, Aluno aluno)
            => lista.Matricular(aluno);
    }
}
