
using System.Data.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }



        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            var customersDtos = _context.Customers
                    .Include(c => c.MembershipType)
                    .ToList()
                    .Select(Mapper.Map<Customer, CustomerDto>); ;
            return customersDtos;
        }



        [HttpGet("{id:int}")]
        public ActionResult<CustomerDto> GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok( Mapper.Map<Customer, CustomerDto>(customer) );
        }


        [HttpPost]
        public ActionResult<CustomerDto> CreateCustomer(CustomerDto customerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return Created( new Uri(Request.GetDisplayUrl() + "/" + customer.Id), customerDto);
        }

        [HttpPut]
        public ActionResult<CustomerDto> UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerInDb = _context.Customers.SingleOrDefault(c=>c.Id == id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();

            return Ok(customerInDb);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<MovieDto> DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            
            _context.SaveChanges();

            return Ok(customerInDb);
        }

    }
}
