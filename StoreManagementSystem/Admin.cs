using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagment
{
    internal class Admin : Person
    {
        private static int AutoIncremendId = 1;

        public Admin(string name) : base(name)
        {
            Id = AutoIncremendId++;
        }

        public int StoreId { get; set; }
    }
}
