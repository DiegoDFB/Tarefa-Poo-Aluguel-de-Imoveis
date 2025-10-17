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

        public Casa(string endereco, int numero, Proprietario proprietario,
                    decimal valorBaseAluguel, int areaQuintal, int vagasGaragem)
                    : base(endereco, numero, proprietario, valorBaseAluguel)
        {
            AreaQuintal = areaQuintal;
            VagasGaragem = vagasGaragem;
        }

        public int GetAreaQuintal() => AreaQuintal;
        public int GetVagasGaragem() => VagasGaragem;
    
    public override bool EstaAlugado()
    {
        return Alugado;
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

    public string ObterStatus()
    {
        return Alugado ? "A casa está alugada" : "A casa está disponível";
    }
}
}