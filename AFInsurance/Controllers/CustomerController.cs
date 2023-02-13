using AFInsurance.Models;
using AFInsurance.Models.DB;
using API.Interfaces;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace AFInsurance.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerRepository _customerRepository;

		public CustomerController(ICustomerRepository customerRepository )
		{
			_customerRepository = customerRepository;
		}

		[HttpGet("GetAll")]
		public ActionResult GetAll()
		{
			return Ok(_customerRepository.GetAll());
		}

		[HttpGet("{id}")]
		public ActionResult Get(int id)
		{
			var result = _customerRepository.GetById(id);
			if (result == null)
				return NotFound();

			return Ok(result);
		}

		[HttpPost]
		public ActionResult AddCustomer([FromBody] CustomerDTO dto)
		{
			if (ModelState.IsValid)
			{
				var result = _customerRepository.Add(dto);
				return StatusCode((int)result.StatusCode, result.Message);
			}
			return BadRequest();
		}

		[HttpPut("{id}")]
		public ActionResult Put(int id, [FromBody] CustomerDTO dto)
		{
			if (ModelState.IsValid)
			{
				var customerObj = new Customer(id, dto);
				var result = _customerRepository.Update(id, customerObj);
				return StatusCode((int)result.StatusCode, result.Message);
			}

			return BadRequest();
			
		}

		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			var result = _customerRepository.Delete(id);
			return StatusCode((int)result.StatusCode, result.Message);
		}
	}
}