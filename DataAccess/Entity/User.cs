﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public int Bonuses { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
