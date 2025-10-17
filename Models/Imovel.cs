using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAluguelImovel.Models
{
    public abstract class Imovel
    {
        protected string Endereco;
        protected int Numero;
        protected bool Alugado;
        protected Proprietario Proprietario;
        protected Inquilino InquilinoAtual;
        protected decimal ValorBaseAluguel;

        public Imovel(string endereco, int numero, Proprietario proprietario, decimal valorBaseAluguel)
        {
            Endereco = endereco;
            Numero = numero;
            Proprietario = proprietario;
            ValorBaseAluguel = valorBaseAluguel;
            Alugado = false;
            InquilinoAtual = null;
        }

        public string GetEndereco() => Endereco;
        public int GetNumero() => Numero;
        public bool GetAlugado() => Alugado;
        public Proprietario GetProprietario() => Proprietario;
        public Inquilino GetInquilinoAtual() => InquilinoAtual;
        public decimal GetValorBaseAluguel() => ValorBaseAluguel;

        public void SetEndereco(string endereco)
        {
            if (!string.IsNullOrWhiteSpace(endereco))
                Endereco = endereco;
        }

        public void SetValorBaseAluguel(decimal valorBase)
        {
            if (valorBase > 0)
                ValorBaseAluguel = valorBase;
        }

        public abstract bool EstaAlugado();
        public abstract string ContatoProprietario();

        public virtual decimal CalcularAluguel(int qtdMeses)
        {
            if (qtdMeses <= 0) return 0;

            decimal valorTotal = ValorBaseAluguel * qtdMeses;

            if (qtdMeses >= 36)
                valorTotal *= 0.85m;
            else if (qtdMeses >= 24)
                valorTotal *= 0.90m;
            else if (qtdMeses >= 12)
                valorTotal *= 0.95m;

            return valorTotal;
        }

        public bool Alugar(Inquilino inquilino)
        {
            if (Alugado || inquilino == null)
                return false;

            Alugado = true;
            InquilinoAtual = inquilino;
            return true;
        }

        public bool Disponibilizar()
        {
            if (!Alugado)
                return false;

            Alugado = false;
            InquilinoAtual = null;
            return true;
        }

        public virtual string ObterInformacoes()
        {
            return $"Endereco: {Endereco}, {Numero} | " +
            $"Valor Base: R$ {ValorBaseAluguel:F2} | " +
            $"Status: {(Alugado ? "Alugado" : "Disponível")}";
        }
    }
}