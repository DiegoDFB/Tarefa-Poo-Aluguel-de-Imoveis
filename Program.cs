using SistemaAluguelImovel.Models;

class Program
{
    private static List<Imovel> imoveis = new List<Imovel>();
    private static List<Proprietario> proprietarios = new List<Proprietario>();
    private static List<Inquilino> inquilinos = new List<Inquilino>();

    static void Main(string[] args)
    {
        Console.WriteLine("=== SISTEMA DE ALUGUEL DE IMÓVEIS ===");
        
        CadastrarDadosIniciais();

        while (true)
        {
            ExibirMenu();
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1": CadastrarImovel(); break;
                case "2": ListarImoveis(); break;
                case "3": AlugarImovel(); break;
                case "4": DisponibilizarImovel(); break;
                case "5": CalcularAluguelPeriodo(); break;
                case "6": ListarImoveisAlugados(); break;
                case "7": DeletarImovel(); break;
                case "0": 
                    Console.WriteLine("Saindo do sistema...");
                    return;
                default: 
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    static void ExibirMenu()
    {
        Console.Clear();
        Console.WriteLine("=== MENU PRINCIPAL ===");
        Console.WriteLine("1 - Cadastrar Imóvel");
        Console.WriteLine("2 - Listar Todos os Imóveis");
        Console.WriteLine("3 - Alugar Imóvel");
        Console.WriteLine("4 - Disponibilizar Imóvel");
        Console.WriteLine("5 - Calcular Aluguel por Período");
        Console.WriteLine("6 - Listar Imóveis Alugados");
        Console.WriteLine("7 - Deletar Imóvel");
        Console.WriteLine("0 - Sair");
        Console.Write("Escolha uma opção: ");
    }

    static void CadastrarDadosIniciais()
    {
        var prop1 = new Proprietario("Diego Blanco", "(11) 9999-8888", "123.456.789-11");
        var prop2 = new Proprietario("Lucas Paiva", "(11) 1111-2222", "987.654.321-22");
        proprietarios.AddRange(new[] { prop1, prop2 });

        var inq1 = new Inquilino("Gabriela Rodrigues", "(11) 3333-4444", "321.654.987-33");
        var inq2 = new Inquilino("Pedro Henrique Silva", "(11) 5555-6666", "789.456.123-44");
        inquilinos.AddRange(new[] { inq1, inq2 });

        imoveis.Add(new Casa("Rua Belém", 118, prop1, 1500.00m, 100, 2));
        imoveis.Add(new Apartamento("Av. Celso Garcia", 1907, prop2, 2000.00m, 64, 6, true));
    }

    static void CadastrarImovel()
    {
        Console.Clear();
        Console.WriteLine("=== CADASTRAR IMÓVEL ===");
        Console.WriteLine("1 - Casa");
        Console.WriteLine("2 - Apartamento");
        Console.Write("Escolha o tipo: ");
        var tipo = Console.ReadLine();

        Console.Write("Endereço (sem número): ");
        var endereco = Console.ReadLine();

        Console.Write("Número: ");
        int numero = int.Parse(Console.ReadLine());

        Console.Write("Valor base do aluguel: ");
        decimal valor = decimal.Parse(Console.ReadLine());

        var proprietario = BuscarOuCadastrarProprietario();

        if (tipo == "1")
        {
            Console.Write("Área do quintal (em m²): ");
            int areaQuintal = int.Parse(Console.ReadLine());
            Console.Write("Quantidade de vagas na garagem: ");
            int garagem = int.Parse(Console.ReadLine());

            var casa = new Casa(endereco, numero, proprietario, valor, areaQuintal, garagem);
            imoveis.Add(casa);
            Console.WriteLine("Casa cadastrada com sucesso!");
        }
        else if (tipo == "2")
        {
            bool elevador = false;

            Console.Write("Andar: ");
            int andar = int.Parse(Console.ReadLine());
            Console.Write("Número do Apartamento: ");
            int numeroApto = int.Parse(Console.ReadLine());
            Console.Write("Possui elevador? (S/N): ");
            if (Console.ReadLine().ToUpper() == "S")
            {
                elevador = true;
            }

            var apto = new Apartamento(endereco, numero, proprietario, valor, numeroApto, andar, elevador);
            imoveis.Add(apto);
            Console.WriteLine("Apartamento cadastrado com sucesso!");
        }
    }

    static Proprietario BuscarOuCadastrarProprietario()
    {
        while (true)
        {
            Console.WriteLine("\n--- Proprietários Cadastrados ---");
            for (int i = 0; i < proprietarios.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {proprietarios[i]}");
            }

            Console.WriteLine("0 - Cadastrar novo proprietário");
            Console.Write("Escolha: ");
            var opcao = Console.ReadLine();

            if (opcao == "0")
            {
                Console.Write("Nome: ");
                var nome = Console.ReadLine();
                Console.Write("Telefone: ");
                var telefone = Console.ReadLine();
                Console.Write("CPF: ");
                var cpf = Console.ReadLine();

                var novoProp = new Proprietario(nome, telefone, cpf);
                proprietarios.Add(novoProp);
                return novoProp;
            }
            else
            {
                int index = int.Parse(opcao) - 1;
                if (index >= 0 && index < proprietarios.Count)
                {
                    return proprietarios[index];
                }
                else
                {
                    Console.Write("Escolha de proprietário inválida.");
                }
            }
        }
    }

    static void ListarImoveis()
    {
        Console.Clear();
        Console.WriteLine("=== TODOS OS IMÓVEIS ===");
        
        if (!imoveis.Any())
        {
            Console.WriteLine("Nenhum imóvel cadastrado.");
            return;
        }

        for (int i = 0; i < imoveis.Count; i++)
        {
            Console.WriteLine($"\n{i + 1} - {imoveis[i].ObterInformacoes()}");
            
            if (imoveis[i] is Casa casa)
                Console.WriteLine($"   Status: {casa.ObterStatus()}");
            else if (imoveis[i] is Apartamento apto)
                Console.WriteLine($"   Status: {apto.ObterStatus()}");

            if (imoveis[i].GetAlugado())
            {
                Console.WriteLine($"   {imoveis[i].GetInquilinoAtual()}");
                Console.WriteLine($"   {imoveis[i].ContatoProprietario()}");
            }
        }
    }

    static void AlugarImovel()
    {
        Console.Clear();
        Console.WriteLine("=== ALUGAR IMÓVEL ===");
        var imoveisDisponiveis = imoveis.Where(i => !i.GetAlugado()).ToList();

        if (!imoveisDisponiveis.Any())
        {
            Console.WriteLine("Nenhum imóvel disponível para alugar.");
            return;
        }

        Console.WriteLine("Imóveis disponíveis:");
        for (int i = 0; i < imoveisDisponiveis.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {imoveisDisponiveis[i].ObterInformacoes()}");
        }

        Console.Write("Escolha o imóvel: ");
        int indexImovel = int.Parse(Console.ReadLine()) - 1;

        if (indexImovel < 0 || indexImovel >= imoveisDisponiveis.Count)
        {
            Console.WriteLine("Índice inválido!");
            return;
        }

        var inquilino = BuscarOuCadastrarInquilino();

        if (imoveisDisponiveis[indexImovel].Alugar(inquilino))
        {
            Console.WriteLine("Imóvel alugado com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro ao alugar imóvel.");
        }
    }

    static Inquilino BuscarOuCadastrarInquilino()
    {
        while (true)
        {
            Console.WriteLine("\n--- Inquilinos Cadastrados ---");
            for (int i = 0; i < inquilinos.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {inquilinos[i]}");
            }

            Console.WriteLine("0 - Cadastrar novo inquilino");
            Console.Write("Escolha: ");
            var opcao = Console.ReadLine();

            if (opcao == "0")
            {
                Console.Write("Nome: ");
                var nome = Console.ReadLine();
                Console.Write("Telefone: ");
                var telefone = Console.ReadLine();
                Console.Write("CPF: ");
                var cpf = Console.ReadLine();

                var novoInq = new Inquilino(nome, telefone, cpf);
                inquilinos.Add(novoInq);
                return novoInq;
            }
            else
            {
                int index = int.Parse(opcao) - 1;
                    if (index >= 0 && index < inquilinos.Count)
                    {
                        return inquilinos[index];
                    }
                    else
                    {
                        Console.Write("Escolha de inquilino inválida.");
                    }
            }
        }
    }

    static void DisponibilizarImovel()
    {
        Console.Clear();
        Console.WriteLine("=== DISPONIBILIZAR IMÓVEL ===");
        var imoveisAlugados = imoveis.Where(i => i.GetAlugado()).ToList();

        if (!imoveisAlugados.Any())
        {
            Console.WriteLine("Nenhum imóvel alugado no momento.");
            return;
        }

        Console.WriteLine("Imóveis alugados:");
        for (int i = 0; i < imoveisAlugados.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {imoveisAlugados[i].ObterInformacoes()}");
        }

        Console.Write("Escolha o imóvel para disponibilizar: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < imoveisAlugados.Count && imoveisAlugados[index].Disponibilizar())
        {
            Console.WriteLine("Imóvel disponibilizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro ao disponibilzar imóvel.");
        }
    }

    static void CalcularAluguelPeriodo()
    {
        Console.Clear();
        Console.WriteLine("=== CALCULAR ALUGUEL ===");
        
        if (!imoveis.Any())
        {
            Console.WriteLine("Nenhum imóvel cadastrado.");
            return;
        }

        for (int i = 0; i < imoveis.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {imoveis[i].ObterInformacoes()}");
        }

        Console.Write("Escolha o imóvel: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index < 0 || index >= imoveis.Count)
        {
            Console.WriteLine("Índice inválido!");
            return;
        }

        Console.Write("Quantidade de meses: ");
        int meses = int.Parse(Console.ReadLine());

        decimal valorTotal = imoveis[index].CalcularAluguel(meses);
        decimal valorSemDesconto = imoveis[index].GetValorBaseAluguel() * meses;

        Console.WriteLine($"\nVALOR DO ALUGUEL");
        Console.WriteLine($"Valor base mensal: R$ {imoveis[index].GetValorBaseAluguel():F2}");
        Console.WriteLine($"Período: {meses} meses");
        Console.WriteLine($"Valor sem desconto: R$ {valorSemDesconto:F2}");
        Console.WriteLine($"Valor total com desconto: R$ {valorTotal:F2}");

        if (valorTotal < valorSemDesconto)
        {
            decimal desconto = valorSemDesconto - valorTotal;
            decimal percentual = ((valorSemDesconto - valorTotal) / valorSemDesconto) * 100;
            Console.WriteLine($"Desconto aplicado: R$ {desconto:F2} ({percentual:F1}%)");
        }
    }

    static void ListarImoveisAlugados()
    {
        Console.Clear();
        Console.WriteLine("=== IMÓVEIS ALUGADOS ===");
        var imoveisAlugados = imoveis.Where(i => i.GetAlugado()).ToList();

        if (!imoveisAlugados.Any())
        {
            Console.WriteLine("Nenhum imóvel alugado no momento.");
            return;
        }

        foreach (var imovel in imoveisAlugados)
        {
            Console.WriteLine($"\n{imovel.ObterInformacoes()}");
            Console.WriteLine($"   {imovel.GetInquilinoAtual()}");
            Console.WriteLine($"   {imovel.ContatoProprietario()}");
        }
    }

    static void DeletarImovel()
    {
        Console.Clear();
        Console.WriteLine("=== DELETAR IMÓVEL ===");
        
        if (!imoveis.Any())
        {
            Console.WriteLine("Nenhum imóvel cadastrado.");
            return;
        }

        for (int i = 0; i < imoveis.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {imoveis[i].ObterInformacoes()}");
        }

        Console.Write("Escolha o imóvel para deletar: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < imoveis.Count)
        {
            imoveis.RemoveAt(index);
            Console.WriteLine("Imóvel deletado com sucesso!");
        }
        else
        {
            Console.WriteLine("Índice inválido!");
        }
    }
}