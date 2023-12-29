using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class ReservaBLL
    {

        private ReservaDAL reservaDAL;

        public ReservaBLL()
        {
            reservaDAL = new ReservaDAL();
        }

        #region Reservas
        /// <summary>
        /// Manipular Reservas
        /// </summary>
        /// <returns></returns>
        public List<Reserva> GetAllReservas()
        {
            return reservaDAL.GetAllReservas();
        }
        /// <summary>
        /// Adicionar Reservas
        /// </summary>
        /// <param name="reserva"></param>
        public void AdicionaReservas(Reserva reserva)
        {
            var reservas = GetAllReservas();
            reservas.Add(reserva);
            reservaDAL.GravarReservas(reservas);
        }

        public void EliminarReserva(List<Reserva> reservas, int nifCliente)
        {
            int codigo = 0;

            for (int i = 0; i < reservas.Count; i++)
            {
                if (reservas[i].cliente.nif == nifCliente)
                {
                    Console.WriteLine("\n################## RESERVA Nº{0} ##################\n", i + 1);
                    Console.WriteLine("\nCódigo: {0}", reservas[i].codigo);
                    Console.WriteLine("Data de Check-in: {0}", reservas[i].checkin);
                    Console.WriteLine("Data de Check-out: {0}", reservas[i].checkout);
                    Console.WriteLine("Código do Alojamento: {0}", reservas[i].alojamento.codigo);
                    Console.WriteLine("Nome do Alojamento: {0}", reservas[i].alojamento.nome);
                    Console.WriteLine("Preço da Reserva: {0}", reservas[i].alojamento.preco * reservas[i].num_pessoas);                   
                }
            }
            Console.WriteLine("\n################## ++++++++++++++ ##################\n");
            Console.Write("Insira o código da reserva que deseja cancelar: ");
            codigo = Convert.ToInt32(Console.ReadLine());

            // Procura a reserva com base no código
            var reservaParaEliminar = reservas.FirstOrDefault(r => r.codigo == codigo && r.cliente.nif == nifCliente);

            if (reservaParaEliminar != null)
            {
                reservas.Remove(reservaParaEliminar);
                reservaDAL.GravarReservas(reservas);
                Console.WriteLine("\nReserva cancelada com sucesso!");
            }
            else
            {
                Console.WriteLine($"Reserva com código {codigo} não encontrada.");
            }
        }

        public int GetReserva(List<Reserva> reservas, int codigo)
        {
            int pos = -1;
            for (int i = 0; i < reservas.Count; i++)
            {
                if (reservas[i].codigo == codigo)
                {
                    pos = i;
                }
            }
            return pos;
        }

        public void EditarReserva(List<Reserva> reservas, DateTime checkin, DateTime checkout, int alojamento, int num_pessoas, int pos)
        {
            reservas[pos].checkin = checkin;
            reservas[pos].checkout = checkout;
            reservas[pos].alojamento.codigo = alojamento;
            reservas[pos].num_pessoas = num_pessoas;

            reservaDAL.GravarReservas(reservas);
        }
        #endregion


    }
}