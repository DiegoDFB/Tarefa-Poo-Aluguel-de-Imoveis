using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAluguelImovel.Models
{
    public abstract class Imovel
    {
        protected int Id;
        protected string Endereco;
        protected int Numero;
        protected bool Alugado;
        protected Proprietario Proprietario;
        protected Inquilino InquilinoAtual;
        protected decimal ValorBaseAluguel;

        public Imovel(int id, string endereco, int numero, Proprietario proprietario, decimal valorBaseAluguel)
        {
            Id = id;
            Endereco = endereco;
            Numero = numero;
            Proprietario = proprietario;
            ValorBaseAluguel = valorBaseAluguel;
            Alugado = false;
            InquilinoAtual = null;
        }
        
        public int GetId() => Id;
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

        public virtual string ObterStatusAluguel()
        {
            return Alugado ? "O imóvel está alugado" : "O imóvel está disponível";
        }
        public abstract string ContatoProprietario();

        public abstract decimal CalcularAluguel(int dias);

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