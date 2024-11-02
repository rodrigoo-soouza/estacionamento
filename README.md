# Gerenciamento de Estacionamento

Este é um sistema simples de gerenciamento de estacionamento em C# que permite registrar a entrada e saída de veículos, exibir registros e calcular o valor devido com base no tempo de permanência.

## Funcionalidades

- **Registrar Chegada**: Registra a chegada de um carro ou moto, juntamente com a placa e o horário de entrada.
- **Registrar Saída e Cobrar**: Calcula o valor total para carros e motos com base na permanência e exibe o valor devido.
- **Exibir Registros**: Mostra a lista de veículos atualmente estacionados e suas informações de chegada.
- **Limpar Tela**: Limpa o console.
- **Sair**: Encerra o programa.

## Estrutura do Projeto

O projeto possui as seguintes funcionalidades principais, que são chamadas a partir do `Main` no menu de opções:
- **RegistrarChegada()**: Registra a entrada de um veículo (carro ou moto) e atribui uma vaga disponível.
- **CobrarSaida()**: Calcula o valor devido com base no tempo de permanência e libera a vaga.
- **ExibirRegistros()**: Exibe todos os veículos estacionados no momento.

## Taxas de Estacionamento

As taxas de estacionamento são calculadas com base nos seguintes valores:

| Tipo  | Valor Inicial | Valor por Hora | Valor por Fração de 30 Min |
|-------|---------------|----------------|----------------------------|
| Carro | R$ 2,00      | R$ 4,80       | R$ 2,50                   |
| Moto  | R$ 1,50      | R$ 2,80       | R$ 1,50                   |

### Regras de Cálculo

1. Para permanência superior a 60 minutos, cobra-se o valor por hora mais a fração, se aplicável.
2. Para permanência entre 30 e 59 minutos, cobra-se o valor inicial mais a fração de 30 minutos.
3. Para permanência menor que 30 minutos, cobra-se apenas o valor inicial.

## Exemplo de Uso

Após iniciar o programa, você verá o menu principal:

