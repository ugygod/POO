using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class ReservaDAL
    {
        private const string reservaFilePath = "reservas.json";

        #region Reserva
        public List<Reserva> GetAllReservas()
        {
            if (File.Exists(reservaFilePath))
            {
                var json = File.ReadAllText(reservaFilePath);
                return JsonConvert.DeserializeObject<List<Reserva>>(json);
            }
            return new List<Reserva>();
        }

        public void GravarReservas(List<Reserva> reservas)
        {
            var json = JsonConvert.SerializeObject(reservas, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(reservaFilePath, json);
        }
        #endregion
    }
}
