using System;

namespace Gerenciamento_Estacionamento
{
    class Estacionamento
    {

        static Dictionary<string, string> veiculosEstacionados = new();
        static int vagaCarro = 30;
        static int vagaMoto = 15;
        
        //Valores do Estacionamento
        const decimal valorMotoHora = 2.80m;
        const decimal valorCarroHora = 4.80m;
        const decimal valorMotoFracao = 1.50m;
        const decimal valorCarroFracao = 2.50m;
        const decimal valorMotoInicial = 1.50m;
        const decimal valorCarroInicial = 2.00m;
        
        // variáveis Globais 
        public static string? placa;
        public static string? horaEntrada;
        public static string? horaSaida;
        public static DateTime? primeiraHora;
        public static DateTime? segundaHora;

        static void Main()
        {
            Console.Title = "Estacionamento";

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Serviço de Gerenciamento de Estacionamento");
                Console.WriteLine("Informe a opção desejada");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("1 - Registrar chegada");
                Console.WriteLine("2 - Registrar saída e cobrar");
                Console.WriteLine("3 - Exibir registros");
                Console.WriteLine("4 - Limpar Tela");
                Console.WriteLine("5 - Sair");
                Console.WriteLine("------------------------------------");
                Console.Write("Digite sua Opção: ");
                

                var opcao = Convert.ToInt32(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        RegistrarChegada();
                        break;
                    case 2:
                        CobrarSaida();
                        break;
                    case 3:
                        Console.Clear();
                        ExibirRegistros();
                        break;
                    case 4:
                        Console.Clear();
                        break;
                    case 5:
                        Console.WriteLine("Saindo do programa.");
                        Thread.Sleep(4000);
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                        break;
                }


            }

        }

        static void RegistrarChegada()
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine("1 - Carro");
            Console.WriteLine("2 - Moto");
            Console.WriteLine("3 - Retorna ao menu principal");
            Console.WriteLine("------------------------------------");
            Console.Write("Digite sua opção: ");
            var tipoVeiculo = Convert.ToInt32(Console.ReadLine());

            switch (tipoVeiculo)
            {
                case 1:

                    if (vagaCarro > 0)
                    {
                        Console.Write("Digite a placa do Carro (Sem hífen): ");
                        placa = (Console.ReadLine() ?? string.Empty).Trim();

                        Console.Write("Digite a hora de chegada (HH:mm): ");
                        horaEntrada = Console.ReadLine() ?? string.Empty;
                        
                        
                        primeiraHora = DateTime.ParseExact(horaEntrada, "HH:mm", null);

                                                
                        veiculosEstacionados.Add(placa, horaEntrada);

                        vagaCarro--;

                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("Registro de chegada Realizado");
                        Console.WriteLine("-------------------------------");
                    }

                    else
                    {
                        Console.WriteLine("--------------------------------------------------------------");
                        Console.WriteLine("Todas as vagas estão ocupadas. Aguarde a saída de um véiculo.");
                        Console.WriteLine("--------------------------------------------------------------");
                    }

                    break;

                case 2:

                    if (vagaMoto > 0)
                    {
                        
                        Console.Write("Digite a placa da Moto (Sem hífen): ");
                        placa = (Console.ReadLine() ?? string.Empty).Trim();

                        Console.Write("Digite a hora de chegada (HH:mm): ");
                        horaEntrada = Console.ReadLine() ?? string.Empty;
                                                
                        primeiraHora = DateTime.ParseExact(horaEntrada, "HH:mm", null);

                        veiculosEstacionados.Add(placa, horaEntrada);

                        vagaMoto--;

                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("Registro de chegada Realizado");
                        Console.WriteLine("-------------------------------");
                    }

                    else
                    {
                        Console.WriteLine("--------------------------------------------------------------");
                        Console.WriteLine("Todas as vagas estão ocupadas. Aguarde a saída de um véiculo.");
                        Console.WriteLine("--------------------------------------------------------------");

                    }
                    break;

                case 3:
                    return;

                default:
                    Console.WriteLine("Código Inválido!!!");
                    break;

            }
                
        }


        static void CobrarSaida()
        {
            Console.WriteLine("Registro de Saída");
            Console.WriteLine();
            Console.WriteLine("------------------------------------");
            Console.WriteLine("1 - Carro");
            Console.WriteLine("2 - Moto");
            Console.WriteLine("3 - Retorna ao menu principal");
            Console.WriteLine("------------------------------------");
            Console.Write("Digite sua opção: ");
            var tipoVeiculo = Convert.ToInt32(Console.ReadLine());

            switch (tipoVeiculo)
            {
                case 1:

                    Console.Write("Digite a placa do Carro(Sem hífen ou espaços): ");
                    placa = (Console.ReadLine() ?? string.Empty).Trim();

                    if (veiculosEstacionados.ContainsKey(placa))
                    {
                        Console.Write("Digite a Hora de Saída: ");
                        horaSaida = Console.ReadLine() ?? string.Empty;
                                                
                        veiculosEstacionados.Remove(placa);
                        vagaCarro++;

                        try
                        {
                            segundaHora = DateTime.ParseExact(horaSaida, "HH:mm", null);

                            if (segundaHora < primeiraHora)
                            {
                                Console.WriteLine("Erro: O horário de saída não pode ser menor que o horário de entrada.");
                                return;
                            }
                        }                      

                        catch(FormatException)
                        {
                            Console.WriteLine("Erro: Formato de horário inválido.");
                            return;
                        }

                        TimeSpan diferenca = (TimeSpan)(segundaHora - primeiraHora);

                        var minutosTotais = Convert.ToInt32(diferenca.TotalMinutes);

                        
                        Console.WriteLine($"Valor Inicial do Estacionamento para Carro: R${valorCarroInicial:F2}");
                        Console.WriteLine();
                        Console.WriteLine($"Valor da Fração de Estacionamento para Carro de 30 minutos: R${valorCarroFracao:F2}");
                        Console.WriteLine();
                        Console.WriteLine($"Valor da Hora do Estacionamento para Moto: R${valorCarroHora:F2}");
                        Console.WriteLine();

                        decimal valorCalculadoCarro;

                        if (minutosTotais >= 60)
                        {
                            int horas = minutosTotais / 60;
                            int minutosRestantes = minutosTotais % 60;

                            valorCalculadoCarro = ((horas * valorCarroHora) + valorCarroInicial) + ((minutosRestantes > 0) ? valorCarroFracao : 0);
                        }
                        
                        else if (minutosTotais >= 30 && minutosTotais < 60)
                        {
                            valorCalculadoCarro = (valorCarroFracao + valorCarroInicial);
                        }

                        else
                        {
                            valorCalculadoCarro = valorCarroInicial;
                        }

                        Console.WriteLine("---------------------------------------------------------");
                        Console.WriteLine($"A diferença entre os horários é de: {diferenca}");
                        Console.WriteLine($"Valor calculado para carro: R${valorCalculadoCarro:F2}");
                        Console.WriteLine("---------------------------------------------------------");

                        

                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("Registro Removido com Sucesso");
                        Console.WriteLine("-------------------------------");
                    }

                        
                    else
                    {
                        Console.WriteLine("--------------------------------------------------------------");
                        Console.WriteLine("Carro não encontrado");
                        Console.WriteLine("--------------------------------------------------------------");
                    }
                    
                    break;

                case 2:
                    Console.Write("Digite a placa da Moto(Sem hífen ou espaços): ");
                    placa = (Console.ReadLine() ?? string.Empty).Trim();

                    if (veiculosEstacionados.ContainsKey(placa))
                    {
                        Console.Write("Digite a Hora de Saída: ");
                        horaSaida = Console.ReadLine() ?? string.Empty;

                        veiculosEstacionados.Remove(placa);
                        vagaMoto++;

                        try
                        {
                            segundaHora = DateTime.ParseExact(horaSaida, "HH:mm", null);

                            if (segundaHora < primeiraHora)
                            {
                                Console.WriteLine("Erro: O horário de saída não pode ser menor que o horário de entrada.");
                                return;
                            }
                        }

                        catch (FormatException)
                        {
                            Console.WriteLine("Erro: Formato de horário inválido.");
                            return;
                        }

                        TimeSpan diferenca = (TimeSpan)(segundaHora - primeiraHora);

                        var minutosTotais = Convert.ToInt32(diferenca.TotalMinutes);


                        Console.WriteLine($"Valor Inicial do Estacionamento para Carro: R${valorMotoInicial:F2}");
                        Console.WriteLine();
                        Console.WriteLine($"Valor da Fração de Estacionamento para Carro de 30 minutos: R${valorMotoFracao:F2}");
                        Console.WriteLine();
                        Console.WriteLine($"Valor da Hora do Estacionamento para Moto: R${valorMotoHora:F2}");
                        Console.WriteLine();

                        decimal valorCalculadoMoto;

                        if (minutosTotais >= 60)
                        {
                            int horas = minutosTotais / 60;
                            int minutosRestantes = minutosTotais % 60;

                            valorCalculadoMoto = ((horas * valorCarroHora) + valorCarroInicial) + ((minutosRestantes > 0) ? valorCarroFracao : 0);
                        }

                        else if (minutosTotais >= 30 && minutosTotais < 60)
                        {
                            valorCalculadoMoto = (valorCarroFracao + valorCarroInicial);
                        }

                        else
                        {
                            valorCalculadoMoto = valorCarroInicial;
                        }

                        Console.WriteLine("---------------------------------------------------------");
                        Console.WriteLine($"A diferença entre os horários é de: {diferenca}");
                        Console.WriteLine();
                        Console.WriteLine($"Valor calculado para carro: R${valorCalculadoMoto:F2}");
                        Console.WriteLine("---------------------------------------------------------");



                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("Registro Removido com Sucesso");
                        Console.WriteLine("-------------------------------");
                    }


                    else
                    {
                        Console.WriteLine("--------------------------------------------------------------");
                        Console.WriteLine("Moto não encontrada");
                        Console.WriteLine("--------------------------------------------------------------");
                    }


                    break;

                case 3:
                    return;

                default:
                    Console.WriteLine("Código Inválido!!!");
                    break;

            }
        }

        static void ExibirRegistros()
        {
            Console.WriteLine("Registros de Veículos Estacionados:");
            Console.WriteLine();
            Console.WriteLine($"Temos um Total de {veiculosEstacionados.Count} Veículo(s) Estacionado(s).");
            Console.WriteLine($"Desses {15 - vagaMoto} são Motos e {30 - vagaCarro} são Carros.");
            Console.WriteLine();
            foreach (var registro in veiculosEstacionados)
            {
                Console.WriteLine($"## Placa: {registro.Key},## Horário de Chegada: {registro.Value}");
            }
        }

    }
        
}
