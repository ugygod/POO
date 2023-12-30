using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class ClienteDAL
    {
        private const string clienteFilePath = "clientes.json";

        #region Cliente
        /// <summary>
        /// Obtém a lista completa de clientes a partir do arquivo
        /// </summary>
        /// <returns> Uma lista de objetos Cliente que representa todos os clientes armazenados </returns>
        public List<Cliente> GetAllClientes()
        {
            if (File.Exists(clienteFilePath))
            {
                var json = File.ReadAllText(clienteFilePath);
                return JsonConvert.DeserializeObject<List<Cliente>>(json);
            }
            return new List<Cliente>();
        }

        /// <summary>
        /// Grava a lista de clientes no arquivo
        /// </summary>
        /// <param name="clientes"> A lista de clientes a ser gravada </param>
        public void GravarClientes(List<Cliente> clientes)
        {
            var json = JsonConvert.SerializeObject(clientes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(clienteFilePath, json);
        }
        #endregion
    }
}
