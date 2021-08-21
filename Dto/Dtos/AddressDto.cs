using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dto.Dtos
{
    public class AddressDto
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Content { get; set; }

        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
