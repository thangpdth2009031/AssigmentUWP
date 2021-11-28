﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssigmentPhamDucThangT2009M1.Entities
{
    public class Song
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string singer { get; set; }
        public string author { get; set; }
        public string thumbnail { get; set; }
        public string link { get; set; }
        public int account_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string message { get; set; }
    }
}
