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
        /// Obtém a lista completa de clientes
        /// </summary>
        /// <returns> Uma lista de objetos Cliente que representa todos os clientes armazenados </returns>
        public List<Cliente> GetAllClientes()
        {
            return clienteDAL.GetAllClientes();
        }

        /// <summary>
        /// Adiciona um novo cliente à lista de clientes
        /// </summary>
        /// <param name="cliente"> O objeto Cliente a ser adicionado </param>
        public void AdicionaClientes(Cliente cliente)
        {
            var clientes = GetAllClientes();
            clientes.Add(cliente);
            clienteDAL.GravarClientes(clientes);
        }

        /// <summary>
        /// Verifica se existe um cliente com um NIF e senha específicos na lista de clientes
        /// </summary>
        /// <param name="clientes"> A lista de clientes a ser verificada </param>
        /// <param name="nif"> O NIF do cliente a ser procurado </param>
        /// <param name="password"> A senha do cliente a ser verificada </param>
        /// <returns> O cliente encontrado ou um novo objeto Cliente se nenhum correspondente for encontrado </returns>
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

        /// <summary>
        /// Obtém a posição na lista de um cliente com um código específico
        /// </summary>
        /// <param name="clientes"> A lista de clientes a ser pesquisada </param>
        /// <param name="codigo"> O código do cliente a ser procurado </param>
        /// <returns> A posição na lista do cliente encontrado ou -1 se nenhum correspondente for encontrado </returns>
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

        /// <summary>
        /// Edita as informações de um cliente na lista
        /// </summary>
        /// <param name="clientes"> A lista de clientes a ser modificada </param>
        /// <param name="nome"> O novo nome do cliente </param>
        /// <param name="nif"> O novo NIF do cliente </param>
        /// <param name="password"> A nova senha do cliente </param>
        /// <param name="email"> O novo email do cliente </param>
        /// <param name="tipo"> O novo tipo do cliente </param>
        /// <param name="pos"> A posição na lista do cliente a ser editado </param>
        public void EditaCliente(List<Cliente> clientes, string nome, int nif, string password, string email, string tipo, int pos)
        {
            clientes[pos].nome = nome;
            clientes[pos].nif = nif;
            clientes[pos].password = password;
            clientes[pos].email = email;
            clientes[pos].tipo = tipo;

            clienteDAL.GravarClientes(clientes);
        }

        /// <summary>
        /// Elimina um cliente da lista com base no NIF
        /// </summary>
        /// <param name="clientes"> A lista de clientes a ser modificada </param>
        /// <param name="nif"> O NIF do cliente a ser removido </param>
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