using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cliente
    {
        public int codigo { get; set; }
        public string nome { get; set; }
        public int nif { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string tipo { get; set; }
    }
}