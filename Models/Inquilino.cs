using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAluguelImovel.Models
{
    public class Inquilino
    {
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public string CPF { get; private set; }

        public Inquilino(string nome, string telefone, string cpf)
        {
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
        }

        public override string ToString()
        {
            return $"Inquilino: {Nome} | Telefone: {Telefone} | CPF: {CPF}";
        }
    }
}