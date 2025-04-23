using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    //help-table for status of ticket
    public class StatusTicket
    {
        public int Id { get; set; }
        public string Status { get; set; } 
        public ICollection<Ticket> Tickets { get; set; }
    }
}
