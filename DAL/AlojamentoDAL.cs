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
        public List<Alojamento> GetAllAlojamentos()
        {
            if (File.Exists(alojamentoFilePath))
            {
                var json = File.ReadAllText(alojamentoFilePath);
                return JsonConvert.DeserializeObject<List<Alojamento>>(json);
            }
            return new List<Alojamento>();
        }

        public void GravarAlojamentos(List<Alojamento> alojamentos)
        {
            var json = JsonConvert.SerializeObject(alojamentos, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(alojamentoFilePath, json);
        }
        #endregion
    }
}