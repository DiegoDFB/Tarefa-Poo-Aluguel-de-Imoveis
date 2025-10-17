using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAluguelImovel.Models
{
    public class Apartamento : Imovel
{
        protected int NumeroApto { get; set; }
        protected int Andar  { get; set; }
        protected bool PossuiElevador  { get; set; }

        public Apartamento(int id, string endereco, int numero, Proprietario proprietario,
                          decimal valorBaseAluguel, int numeroApto, int andar, bool possuiElevador)
                          : base(id, endereco, numero, proprietario, valorBaseAluguel)
        {
            NumeroApto = numeroApto;
            Andar = andar;
            PossuiElevador = possuiElevador;
        }

    
    public override string ObterStatusAluguel()
    {
        string status = Alugado ? "está alugado" : "está disponível";
        return $"O apartamento de número {NumeroApto} {status}";
    }

    public override string ContatoProprietario()
    {
        return $"Contato do proprietário do apartamento: {Proprietario.GetTelefone()}";
    }

        public override string ObterInformacoes()
        {
            return base.ObterInformacoes() +
                   $" | Tipo: Apartamento | Andar: {Andar} | Elevador: {(PossuiElevador ? "Sim" : "Não")}";
        }
    
    public override decimal CalcularAluguel(int dias)
        {
            if (dias <= 0) return 0;

            decimal valorTotal = ValorBaseAluguel * dias;

            if (dias >= 1095)
                valorTotal *= 0.90m;
            else if (dias >= 730)
                valorTotal *= 0.92m;
            else if (dias >= 365)
                valorTotal *= 0.98m;

            return valorTotal;
        }
}
}