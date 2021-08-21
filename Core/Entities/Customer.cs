using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Customer
    {
        public Customer()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
