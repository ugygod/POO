using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Model;

namespace DAL
{
    public class AlojamentoDAL
    {
        private const string alojamentoFilePath = "alojamentos.json";

        #region Alojamento
        /// <summary>
        /// Obtém a lista completa de alojamentos a partir do arquivo
        /// </summary>
        /// <returns> Uma lista de objetos Alojamento que representa todos os alojamentos armazenados </returns>
        public List<Alojamento> GetAllAlojamentos()
        {
            if (File.Exists(alojamentoFilePath))
            {
                var json = File.ReadAllText(alojamentoFilePath);
                return JsonConvert.DeserializeObject<List<Alojamento>>(json);
            }
            return new List<Alojamento>();
        }

        /// <summary>
        /// Grava a lista de alojamentos no arquivo
        /// </summary>
        /// <param name="alojamentos"> A lista de alojamentos a ser gravada </param>
        public void GravarAlojamentos(List<Alojamento> alojamentos)
        {
            var json = JsonConvert.SerializeObject(alojamentos, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(alojamentoFilePath, json);
        }
        #endregion
    }
}