using Model;
using BLL;
using DAL;

internal class Program
{
    static Cliente sessaoCliente;
    static void Main(string[] args)
    {

        var alojamentoBLL = new AlojamentoBLL();
        var reservaBLL = new ReservaBLL();
        var clienteBLL = new ClienteBLL();

        int continuar = 1;
        int oplogin, op;
        int nif = 0, cod = 0;
        string nome = "", password = "";
       
        
        while (continuar == 1)
        {
            MenuLogin();
            oplogin = int.Parse(Console.ReadLine());

            switch (oplogin)
            {
                case 1:
                    Console.WriteLine("\n################## Iniciar sessão ##################\n");
                    Console.Write("NIF: ");
                    nif = int.Parse(Console.ReadLine());
                    Console.Write("Password: ");
                    password = Console.ReadLine();

                    sessaoCliente = clienteBLL.ExisteCliente(clienteBLL.GetAllClientes(),nif, password);
                    if (sessaoCliente.nome == null)
                    {
                        Console.WriteLine("Dados introduzidos inválidos");
                    }
                    else
                    {
                        continuar = 0;
                    }
                    break;

                case 2:
                    bool res = false;
                    Console.WriteLine("\n################## Registar-se ##################");
                    Console.Write("Nome: ");
                    nome = Console.ReadLine();
                    Console.Write("NIF: ");
                    nif = Convert.ToInt32(Console.ReadLine());
                    res = clienteBLL.ExisteClienteNIF(clienteBLL.GetAllClientes(), nif);
                    if (res == false)
                    {
                        Console.Write("Password: ");
                        password = Console.ReadLine();
                        Console.Write("Email: ");
                        string email = Console.ReadLine();

                        Cliente cliente = new Cliente
                        {
                            codigo = clienteBLL.GetAllClientes().Count + 1,
                            nome = nome,
                            nif = nif,
                            password = password,
                            email = email,
                            tipo = "Cliente"
                        };

                        clienteBLL.AdicionaClientes(cliente);
                        Console.WriteLine("Registo efetuado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("O NIF introduzido já está associado a uma conta!");
                    }
                    break;
                case 3:
                    continuar = 0;
                    break;
                default:
                    break;
            }
        }

        if (sessaoCliente != null && sessaoCliente.tipo == "Gestor")
        {
            continuar = 1;
            while (continuar == 1)
            {
                MenuGestor();
                op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        ListaAlojamento(alojamentoBLL.GetAllAlojamentos());
                        break;
                    case 2:
                        EditarAlojamentos(alojamentoBLL.GetAllAlojamentos(), alojamentoBLL);
                        break;
                       
                    case 3:
                        AdicionaAlojamentos(alojamentoBLL);
                        break;
                    case 4:
                        EditarClientes(clienteBLL.GetAllClientes(), clienteBLL);
                        break;
                    case 5:
                        ListaReserva(reservaBLL.GetAllReservas());
                        break;
                    case 6:
                        EditarReservas(reservaBLL.GetAllReservas(), reservaBLL, alojamentoBLL);
                        break;
                    case 7:
                        EliminarAlojamentos(alojamentoBLL.GetAllAlojamentos(), alojamentoBLL);
                        break;
                    case 8:
                        EliminarClientes(clienteBLL.GetAllClientes(), clienteBLL);
                        break;
                    case 9:
                        EliminarReservas(reservaBLL.GetAllReservas(), reservaBLL, sessaoCliente);
                        break;
                    case 10:
                        continuar = 0;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        else if (sessaoCliente != null && sessaoCliente.tipo == "Cliente")
        {
            continuar = 1;
            while (continuar == 1)
            {
                MenuCliente();
                op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        ListaAlojamentosDisponiveis(alojamentoBLL.GetAllAlojamentos(), reservaBLL.GetAllReservas());
                        break;
                    case 2:
                        AdicionaReservas(reservaBLL, alojamentoBLL, alojamentoBLL.GetAllAlojamentos());
                        break;
                    case 3:
                        HistoricoReservas(reservaBLL.GetAllReservas(), sessaoCliente);
                        break;
                    case 4:
                        EliminarReservas(reservaBLL.GetAllReservas(), reservaBLL, sessaoCliente);
                        break;
                    case 5:
                        EditarClientes(clienteBLL.GetAllClientes(), clienteBLL);
                        break;
                    case 6:
                        continuar = 0;
                        break;        
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }       
    }

    static void ListaAlojamento(List<Alojamento> alojamentos)
    {
        Console.WriteLine("\nLista de alojamentos:");
        foreach (var alojamento in alojamentos)
        {
            Console.WriteLine($"\nCódigo: {alojamento.codigo} \n" +
                    $"Nome do Alojamento: {alojamento.nome} \n" +
                    $"Número de camas: {alojamento.camas} \n" +
                    $"Espaço em m2: {alojamento.espaco} \n" +
                    $"Preço por pessoa: {alojamento.preco} \n");
        }
    }
    static void ListaAlojamentosDisponiveis(List<Alojamento> alojamentos, List<Reserva> reservas)
    {
        Console.WriteLine("\nLista de alojamentos disponíveis: ");

        foreach (var alojamento in alojamentos)
        {
            bool disponivel = true;

            foreach (var reserva in reservas)
            {
                if (alojamento.codigo == reserva.alojamento.codigo)
                {
                    disponivel = false;
                    break; 
                }
            }
            if (disponivel)
            {
                Console.WriteLine($"\nCódigo: {alojamento.codigo} \n" +
                                  $"Nome do Alojamento: {alojamento.nome} \n" +
                                  $"Número de camas: {alojamento.camas} \n" +
                                  $"Espaço em m2: {alojamento.espaco} \n" +
                                  $"Preço por pessoa: {alojamento.preco} \n");
            }
        }
    }


    static void AdicionaAlojamentos(AlojamentoBLL alojamentoBLL)
    {
        Console.Write("Nome do alojamento: ");
        string nome = Console.ReadLine();
        Console.Write("Número de camas: ");
        int camas = Convert.ToInt32(Console.ReadLine());
        Console.Write("Espaço em m2: ");
        double espaco = Convert.ToDouble(Console.ReadLine());
        Console.Write("Preço por pessoa: ");
        double preco = Convert.ToDouble(Console.ReadLine());

        Alojamento alojamento = new Alojamento
        {
            codigo = alojamentoBLL.GetAllAlojamentos().Count + 1,
            nome = nome,
            camas = camas,
            espaco = espaco,
            preco = preco
        };

        alojamentoBLL.AdicionaAlojamentos(alojamento);
        Console.WriteLine("Alojamento adicionado com sucesso!");
    }

    static void EditarAlojamentos(List<Alojamento> alojamentos, AlojamentoBLL alojamentoBLL)
    {
        Console.Write("Código do alojamento: ");
        int codigo = Convert.ToInt32(Console.ReadLine());

        int pos = alojamentoBLL.GetAlojamento(alojamentos, codigo);

        Console.Write("Nome do alojamento: ");
        string nome = Console.ReadLine();
        Console.Write("Número de camas: ");
        int camas = Convert.ToInt32(Console.ReadLine());
        Console.Write("Espaço em m2: ");
        double espaco = Convert.ToDouble(Console.ReadLine());
        Console.Write("Preço por pessoa: ");
        double preco = Convert.ToDouble(Console.ReadLine());

        alojamentoBLL.EditaAlojamento(alojamentos, nome, camas, espaco, preco, pos);
    }

    static void ListaReserva(List<Reserva> reservas)
    {
        Console.WriteLine("\nLista de reservas:");
        foreach (var reserva in reservas)
        {
            Console.WriteLine($"\nCódigo: {reserva.codigo} \n" +
                $"Data de Check-in: {reserva.checkin} \n" +
                $"Data de Check-out: {reserva.checkout} \n" +
                $"Nome do Cliente: {reserva.cliente.nome} \n" +
                $"NIF do Cliente: {reserva.cliente.nif} \n" +
                $"Nome do Alojamento: {reserva.alojamento.nome}\n" +
                $"Preço da Reserva: {reserva.alojamento.preco * reserva.num_pessoas}\n");
        }
    }

    static void HistoricoReservas(List<Reserva> reservas, Cliente cliente)
    {
        Console.WriteLine("\nHistórico de reservas:");
        foreach (var reserva in reservas)
        {
            if (reserva.cliente.codigo == cliente.codigo)
            {
                Console.WriteLine($"\nCódigo: {reserva.codigo} \n" +
                $"Data de Check-in: {reserva.checkin} \n" +
                $"Data de Check-out: {reserva.checkout} \n" +
                $"Nome do Cliente: {reserva.cliente.nome} \n" +
                $"NIF do Cliente: {reserva.cliente.nif} \n" +
                $"Nome do Alojamento: {reserva.alojamento.nome}\n" +
                $"Preço da Reserva: {reserva.alojamento.preco * reserva.num_pessoas}\n");
            }        
        }
    }

    static void EliminarReservas(List<Reserva> reservas, ReservaBLL reservaBLL, Cliente cliente)
    {
        if (sessaoCliente.tipo == "Gestor")
        {
            Console.Write("NIF do cliente que deseja cancelar uma reserva: ");
            int nifCliente = Convert.ToInt32(Console.ReadLine());
            reservaBLL.EliminarReserva(reservas, nifCliente);
        }
        else
        {
            reservaBLL.EliminarReserva(reservas, sessaoCliente.nif);
        }
    }

    static void AdicionaReservas(ReservaBLL reservaBLL, AlojamentoBLL alojamentoBLL, List<Alojamento>alojamentos)
    {
        bool res = false;

        Console.Write("Codigo do Alojamento: ");
        int alojamento = Convert.ToInt32(Console.ReadLine());

        int pos = alojamentoBLL.GetAlojamento(alojamentos, alojamento);

        if (pos != -1)
        {
            Console.Write("Data de Checkin: ");
            DateTime checkin = Convert.ToDateTime(Console.ReadLine());

            res = reservaBLL.ExisteReservaPorAlojamentoEData(reservaBLL.GetAllReservas(), alojamento, checkin);

            if (res == true)
            {
                Console.Write("Data de Checkout: ");
                DateTime checkout = Convert.ToDateTime(Console.ReadLine());

                if (checkout > checkin)
                {
                    Console.Write("Numero de Pessoas incluidas na Reserva: ");
                    int num_pessoas = Convert.ToInt32(Console.ReadLine());

                    Reserva reserva = new Reserva
                    {
                        codigo = reservaBLL.GetAllReservas().Count + 1,
                        checkin = checkin,
                        checkout = checkout,
                        cliente = sessaoCliente,
                        alojamento = alojamentos[pos],
                        num_pessoas = num_pessoas
                    };

                    reservaBLL.AdicionaReservas(reserva);
                    Console.WriteLine("Reserva adicionada com sucesso!");
                }
                else
                {
                    Console.WriteLine("Data de checkout inválida");
                }
               
            }
            else
            {
                Console.WriteLine("O Alojamento introduzido já se encontra reservado para a data inserida!");
            }
           
        }
        else
        {
            Console.WriteLine("O Código do Alojamento introduzido não é válido!");
        }
    }

    static void EditarReservas(List<Reserva> reservas, ReservaBLL reservaBLL, AlojamentoBLL alojamentoBLL) 
    {
        int pos = 0, pos_alojamento = 0;
        bool res = false;
        Console.Write("Insira o Código da Reserva que deseja editar: ");
        int codigo = Convert.ToInt32(Console.ReadLine());
        pos = reservaBLL.GetReserva(reservaBLL.GetAllReservas(), codigo);

        if (pos != -1)
        {
            Console.Write("Codigo do Alojamento: ");
            int alojamento = Convert.ToInt32(Console.ReadLine());
            pos_alojamento = alojamentoBLL.GetAlojamento(alojamentoBLL.GetAllAlojamentos(), alojamento);

            if (pos_alojamento != -1)
            {
                Console.Write("Data de Checkin: ");
                DateTime checkin = Convert.ToDateTime(Console.ReadLine());
                res = reservaBLL.ExisteReservaPorAlojamentoEData(reservaBLL.GetAllReservas(), alojamento, checkin);

                if (res == true)
                {
                    Console.Write("Data de Checkout: ");
                    DateTime checkout = Convert.ToDateTime(Console.ReadLine());

                    if (checkout > checkin)
                    {
                        Console.Write("Numero de Pessoas incluidas na Reserva: ");
                        int num_pessoas = Convert.ToInt32(Console.ReadLine());

                        reservaBLL.EditarReserva(reservaBLL.GetAllReservas(), checkin, checkout, alojamento, num_pessoas, pos);
                        Console.WriteLine("Reserva editada com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Data de checkout inválida");
                    }               
                }
                else
                {
                    Console.WriteLine("O Alojamento introduzido já se encontra reservado para a data inserida!");
                }
            }
            else
            {
                Console.WriteLine("Codigo do Alojamento inválido! ");
            }
        }                     
        else
        {
            Console.WriteLine("O código da reserva inserido não é válido!");
        }
    }

    static void EditarClientes(List<Cliente> clientes, ClienteBLL clienteBLL)
    {
        bool res = false;
        int pos = 0;
       
        if (sessaoCliente.tipo == "Gestor")
        {
            Console.Write("Código do Cliente: ");
            int codigo = Convert.ToInt32(Console.ReadLine());
            pos = clienteBLL.GetCliente(clientes, codigo);
        }
        else
        {
            pos = clienteBLL.GetCliente(clientes, sessaoCliente.codigo);
        }
              
        Console.Write("Novo Nome do Cliente: ");
        string nome = Console.ReadLine();
        Console.Write("Novo NIF: ");
        int nif = Convert.ToInt32(Console.ReadLine());
        res = clienteBLL.ExisteClienteNIF(clienteBLL.GetAllClientes(), nif);

        if (res == false) 
        {
            Console.Write("Nova password: ");
            string password = Console.ReadLine();
            Console.Write("Novo Email: ");
            string email = Console.ReadLine();

            if (sessaoCliente.tipo == "Gestor")
            {
                Console.Write("Tipo (Cliente/Gestor): ");
                string tipo = Console.ReadLine();
                clienteBLL.EditaCliente(clientes, nome, nif, password, email, tipo, pos);
            }
            else
            {
                clienteBLL.EditaCliente(clientes, nome, nif, password, email, "Cliente", pos);
            }
        }
        else
        {
            Console.WriteLine("O NIF introduzido já está associado a uma conta!");
        }
    }

    static void EliminarAlojamentos(List<Alojamento> alojamentos, AlojamentoBLL alojamentoBLL)
    {
        Console.Write("Insira o codigo do Alojamento que deseja remover: ");
        int codigo = Convert.ToInt32(Console.ReadLine());

        alojamentoBLL.EliminarAlojamento(alojamentos,codigo);
    }

    static void EliminarClientes(List<Cliente> clientes, ClienteBLL clienteBLL)
    {
        Console.Write("Insira o nif do Cliente que deseja remover: ");
        int nif = Convert.ToInt32(Console.ReadLine());

        clienteBLL.EliminarCliente(clientes, nif);
    }
    static void MenuLogin()
    {
        Console.WriteLine("\n################## Menu Login ##################\n");
        Console.WriteLine("1 - Iniciar Sessão");
        Console.WriteLine("2 - Registar-se");
        Console.WriteLine("3 - Sair");
        Console.WriteLine("\n################## ++++++++++++++ ##################\n");
        Console.Write("Escolha uma opção: ");
    }
    static void MenuGestor()
    {
        Console.WriteLine("\n################## Menu Gestor ##################\n");
        Console.WriteLine("1  - Listar Alojamentos");
        Console.WriteLine("2  - Editar Alojamento");
        Console.WriteLine("3  - Inserir Alojamento");
        Console.WriteLine("4  - Editar Cliente");
        Console.WriteLine("5  - Listar Reservas");
        Console.WriteLine("6  - Editar Reserva");
        Console.WriteLine("7  - Remover Alojamento");
        Console.WriteLine("8  - Remover Cliente");
        Console.WriteLine("9  - Remover Reserva");
        Console.WriteLine("10 - Sair");
        Console.WriteLine("\n################## ++++++++++++++ ##################\n");
        Console.Write("Escolha uma opção? ");
    }

    static void MenuCliente()
    {
        Console.WriteLine("\n################## Menu Cliente ##################\n");
        Console.WriteLine("1 - Listar Alojamentos Disponiveis");
        Console.WriteLine("2 - Efetuar Reserva");
        Console.WriteLine("3 - Historico de Reservas");
        Console.WriteLine("4 - Cancelar Reserva");
        Console.WriteLine("5 - Editar Dados");
        Console.WriteLine("6 - Sair");
        Console.WriteLine("\n################## ++++++++++++++ ##################\n");
        Console.Write("Escolha uma opção? ");
    }

}

