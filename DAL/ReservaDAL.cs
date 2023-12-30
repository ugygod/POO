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
        /// <summary>
        /// Obtém a lista completa de reservas armazenadas no arquivo
        /// </summary>
        /// <returns> Uma lista de objetos Reserva que representa todas as reservas armazenadas </returns>
        public List<Reserva> GetAllReservas()
        {
            if (File.Exists(reservaFilePath))
            {
                var json = File.ReadAllText(reservaFilePath);
                return JsonConvert.DeserializeObject<List<Reserva>>(json);
            }
            return new List<Reserva>();
        }

        /// <summary>
        /// Grava a lista de reservas no arquivo
        /// </summary>
        /// <param name="reservas"> A lista de reservas a ser gravada no arquivo </param>
        public void GravarReservas(List<Reserva> reservas)
        {
            var json = JsonConvert.SerializeObject(reservas, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(reservaFilePath, json);
        }
        #endregion
    }
}
