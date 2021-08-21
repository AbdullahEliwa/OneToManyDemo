using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
