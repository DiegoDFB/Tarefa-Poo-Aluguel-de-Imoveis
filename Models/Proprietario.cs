using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAluguelImovel.Models
{
    public class Proprietario
    {
        private string Nome;
        private string Telefone;
        private string CPF;

        public Proprietario(string nome, string telefone, string cpf)
        {
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
        }

        public string GetNome() => Nome;
        public string GetTelefone() => Telefone;
        public string GetCPF() => CPF;

        public override string ToString()
        {
            return $"Proprietário: {Nome} | Telefone: {Telefone} | CPF: {CPF}";
        }
    }
}