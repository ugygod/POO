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
        public List<Cliente> GetAllClientes()
        {
            if (File.Exists(clienteFilePath))
            {
                var json = File.ReadAllText(clienteFilePath);
                return JsonConvert.DeserializeObject<List<Cliente>>(json);
            }
            return new List<Cliente>();
        }

        public void GravarClientes(List<Cliente> clientes)
        {
            var json = JsonConvert.SerializeObject(clientes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(clienteFilePath, json);
        }
        #endregion
    }
}
