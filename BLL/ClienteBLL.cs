using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class ClienteBLL
    {

        private ClienteDAL clienteDAL;

        public ClienteBLL()
        {
            clienteDAL = new ClienteDAL();
        }

        #region Clientes
        /// <summary>
        /// Manipular Clientes
        /// </summary>
        /// <returns></returns>
        public List<Cliente> GetAllClientes()
        {
            return clienteDAL.GetAllClientes();
        }
        /// <summary>
        /// Adicionar Clientes
        /// </summary>
        /// <param name="cliente"></param>
        public void AdicionaClientes(Cliente cliente)
        {
            var clientes = GetAllClientes();
            clientes.Add(cliente);
            clienteDAL.GravarClientes(clientes);
        }

        public Cliente ExisteCliente(List<Cliente> clientes, int nif, string password)
        {
            Cliente cliente = new Cliente();
            for (int i = 0; i < clientes.Count ; i++)
            {
                if (clientes[i].nif == nif && clientes[i].password == password)
                {
                    cliente = clientes[i];
                }

            }
            return cliente;
        }

        public int GetCliente(List<Cliente> clientes, int codigo)
        {
            int pos = -1;
            for (int i = 0; i < clientes.Count; i++)
            {
                if (clientes[i].codigo == codigo)
                {
                    pos = i;
                }
            }
            return pos;
        }

        public void EditaCliente(List<Cliente> clientes, string nome, int nif, string password, string email, string tipo, int pos)
        {
            clientes[pos].nome = nome;
            clientes[pos].nif = nif;
            clientes[pos].password = password;
            clientes[pos].email = email;
            clientes[pos].tipo = tipo;

            clienteDAL.GravarClientes(clientes);
        }

        public void EliminarCliente(List<Cliente> clientes, int nif)
        {
            // Procura o cliente com base no nif
            var clienteParaEliminar = clientes.FirstOrDefault(r => r.nif == nif);

            if (clienteParaEliminar != null)
            {
                clientes.Remove(clienteParaEliminar);
                clienteDAL.GravarClientes(clientes);
                Console.WriteLine("\nCliente removido com sucesso!");
            }
            else
            {
                Console.WriteLine($"Cliente com nif {nif} não encontrado.");
            }
        }


        #endregion


    }
}