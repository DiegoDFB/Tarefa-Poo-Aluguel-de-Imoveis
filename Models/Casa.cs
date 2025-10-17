using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAluguelImovel.Models
{
    public class Casa : Imovel
{
        protected int AreaQuintal  { get; set; }
        protected int VagasGaragem  { get; set; }

        public Casa(int id, string endereco, int numero, Proprietario proprietario,
                    decimal valorBaseAluguel, int areaQuintal, int vagasGaragem)
                    : base(id, endereco, numero, proprietario, valorBaseAluguel)
        {
            AreaQuintal = areaQuintal;
            VagasGaragem = vagasGaragem;
        }

        public int GetAreaQuintal() => AreaQuintal;
        public int GetVagasGaragem() => VagasGaragem;
    
    public override string ObterStatusAluguel()
    {
        return Alugado ? "A casa está alugada" : "A casa está disponível";
    }

    public override string ContatoProprietario()
    {
        return $"Contato do proprietário da casa: {Proprietario.GetTelefone()}";
    }

        public override string ObterInformacoes()
        {
            return base.ObterInformacoes() +
                   $" | Tipo: Casa | Área do Quintal: {AreaQuintal}m² | Vagas Garagem: {VagasGaragem}";
        }
    
    public override decimal CalcularAluguel(int dias)
        {
            if (dias <= 0) return 0;

            decimal valorTotal = ValorBaseAluguel * dias;

            if (dias >= 1095)
                valorTotal *= 0.85m;
            else if (dias >= 730)
                valorTotal *= 0.90m;
            else if (dias >= 365)
                valorTotal *= 0.95m;

            return valorTotal;
        }
}
}