# Sistema de Aluguel de Imóveis

### Classe abstrata Imovel
    - Além dos atributos de Endereço, Número, Alugado e Proprietário, foram adicionados os atributos InquilinoAtual e 
      ValorBaseAluguel para implementação de métodos adicionais de consulta e cálculo de descontos.
      
    - Os atributos estão encapsulados (protected) e há métodos públicos (get e set com validação básica) para 
      obtenção e manipulação dos dados.
      
    - Os métodos EstaAlugado() e ContatoProprietario() estão definidos como abstract nesta classe para implementação 
      nas classes filhas, enquanto o método CalcularAluguel(int) é definido como virtual e já conta com uma estrutura
      padrão do cálculo, incluindo descontos por quantidade de meses alugados.
      
    - Por fim, os métodos Alugar(Inquilino) e Disponibilizar() são responsáveis pelas suas respectivas ações e o 
      método ObterInformacoes() é definido como virtual, já possuindo estrutura padrão e sendo posteriormente 
      incrementado nas classes filhas.

### Adicionais

    - As classes Casa e Apartamento, além de herdarem de Imovel, possuem atributos específicos.
    
    - Foi adicionada a classe Inquilino.
    
    - No menu, foram adicionadas as funções de listar todos os imóveis e listar os imóveis alugados.
    
    - Ao iniciar o programa, são cadastrados dados iniciais (proprietários, inquilinos e imóveis) para 
      testes mais rápidos das funcionalidades.
      
    - Foi adicionada a função de descontos por quantidade de meses alugados -> a partir de 1 ano = 5%;
      a partir de 2 anos = 10%; a partir de 3 anos = 15%
