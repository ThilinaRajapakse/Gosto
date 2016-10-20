using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gosto.Models
{
    public class Reservation
    {
        public int ID { get; set; }

        public DateTime Time { get; set; }

        public string Comments { get; set; }

        public Customer Customer { get; set; }
    }
}
