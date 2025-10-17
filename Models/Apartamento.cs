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

        public Apartamento(string endereco, int numero, Proprietario proprietario,
                          decimal valorBaseAluguel, int numeroApto, int andar, bool possuiElevador)
                          : base(endereco, numero, proprietario, valorBaseAluguel)
        {
            NumeroApto = numeroApto;
            Andar = andar;
            PossuiElevador = possuiElevador;
        }

    
    public override bool EstaAlugado()
    {
        return Alugado;
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

    public string ObterStatus()
    {
        string status = Alugado ? "está alugado" : "está disponível";
        return $"O apartamento de número {NumeroApto} {status}";
    }
}
}