using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dto.Dtos
{
    public class CustomerDto
    {
        public CustomerDto()
        {
            Addresses = new List<AddressDto>();

        }

        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        public List<AddressDto> Addresses { get; set; }
    }
}
