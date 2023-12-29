using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Reserva
    {
        public int codigo { get; set; }
        public DateTime checkin { get; set; }
        public DateTime checkout { get; set; }
        public Cliente cliente { get; set; }
        public Alojamento alojamento { get; set; }
        public int num_pessoas { get; set; }

    }
}
