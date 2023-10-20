using System;
using System.Collections.Generic;

namespace BancoAppTeste
{
    class Program
    {
        static List<Cliente> clientes = new List<Cliente>();

        static void Main(string[] args)
        {
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("Bem-vindo ao banco!");
                Console.WriteLine("1 - Cadastrar Cliente");
                Console.WriteLine("2 - Acessar Conta");
                Console.WriteLine("3 - Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarCliente();
                        break;
                    case "2":
                        AcessarConta();
                        break;
                    case "3":
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void CadastrarCliente()
        {
            Console.Write("Digite o CPF do cliente: ");
            string cpf = Console.ReadLine();

            if (ClienteExiste(cpf))
            {
                Console.WriteLine("Cliente já cadastrado com este CPF.");
                return;
            }

            Console.Write("Digite o nome do cliente: ");
            string nome = Console.ReadLine();

            Cliente novoCliente = new Cliente(cpf, nome);
            // Criar a conta e associar ao cliente
            novoCliente.AssociarConta(new Conta(novoCliente));
            clientes.Add(novoCliente);
            Console.WriteLine("Cliente cadastrado com sucesso.");
        }

        static void AcessarConta()
        {
            Console.Write("Digite o CPF do cliente: ");
            string cpf = Console.ReadLine();

            Cliente cliente = clientes.Find(c => c.Cpf == cpf);

            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }

            // Verifica se o cliente possui uma conta associada
            if (cliente.Conta == null)
            {
                Console.WriteLine("Cliente não possui uma conta associada.");
                return;
            }

            Console.WriteLine($"Bem-vindo(a) {cliente.Nome}!");
            Console.WriteLine("1 - Depositar");
            Console.WriteLine("2 - Sacar");
            Console.WriteLine("3 - Consultar Saldo");
            Console.WriteLine("4 - Transferir");
            Console.WriteLine("5 - Voltar");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            // Resto do código...
        }

        static bool ClienteExiste(string cpf)
        {
            return clientes.Exists(c => c.Cpf == cpf);
        }
    }

    class Cliente
    {
        public string Cpf { get; private set; }
        public string Nome { get; private set; }
        public Conta Conta { get; private set; }

        public Cliente(string cpf, string nome)
        {
            Cpf = cpf;
            Nome = nome;
        }

        public void AssociarConta(Conta conta)
        {
            Conta = conta;
        }
    }

    class Conta
    {
        public int Numero { get; private set; }
        public double Saldo { get; private set; }
        public Cliente Titular { get; private set; }

        public Conta(Cliente titular)
        {
            Numero = GerarNumeroConta();
            Saldo = 0;
            Titular = titular;
        }

        public void Depositar(double valor)
        {
            Saldo += valor;
        }

        public bool Sacar(double valor)
        {
            if (Saldo >= valor)
            {
                Saldo -= valor;
                return true;
            }
            return false;
        }

        public double ConsultarSaldo()
        {
            return Saldo;
        }

        public bool Transferir(double valor, Conta contaDestino)
        {
            if (Saldo >= valor)
            {
                Saldo -= valor;
                contaDestino.Saldo += valor;
                return true;
            }
            return false;
        }

        private static int GerarNumeroConta()
        {
            Random random = new Random();
            return random.Next(1000, 9999);
        }

    }
}
