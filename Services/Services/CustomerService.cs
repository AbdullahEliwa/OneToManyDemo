using AutoMapper;
using Core.Database;
using Core.Entities;
using Dto.Dtos;
using Microsoft.EntityFrameworkCore;
using Services.Common;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CustomerService : DbService, ICustomerService
    {
        #region CTOR, Fields.
        public readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Read Operations.
        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            return (await _context.Customers.ToListAsync()).Select(_mapper.Map<CustomerDto>);
        }

        public async Task<CustomerDto> Get(int id)
        {
            return _mapper.Map<CustomerDto>(await _context.Customers.Include(c => c.Addresses).FirstOrDefaultAsync(c => c.Id == id));
        }
        #endregion

        #region Create Operations.
        public async Task Create(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            customer.Id = (await _context.Customers.MaxAsync(c => (int?)c.Id) ?? 0) + 1;

            var lastAddressId = (await _context.Addresses.MaxAsync(a => (int?)a.Id) ?? 0);
            foreach (var address in customer.Addresses)
                address.Id = ++lastAddressId;

            await Insert(customer);
        }
        #endregion

        #region Update Operations.
        public async Task Update(CustomerDto customerDto)
        {
            var customerInDb = (await _context.Customers.Include(c => c.Addresses).FirstOrDefaultAsync(c => c.Id == customerDto.Id))
                            ?? throw new ArgumentException("Can't find that customer!");

            var lastAddressId = (await _context.Addresses.MaxAsync(a => (int?)a.Id) ?? 0);
            foreach (var address in customerDto.Addresses.Where(a => a.Id == null))
                address.Id = ++lastAddressId;

            var customer = _mapper.Map(customerDto, customerInDb);

            await Update(customer);

        }
        #endregion

        #region Delete Operations.
        public async Task Delete(int id)
        {
            var customer = (await _context.Customers.FirstOrDefaultAsync(c => c.Id == id))
                            ?? throw new ArgumentException("Can't find that customer!");
            await Delete(customer);
        }
        #endregion
    }
}
