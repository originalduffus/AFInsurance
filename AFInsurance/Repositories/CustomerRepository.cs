using API.Context;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net;
using API.Interfaces;
using AFInsurance.Models;
using AFInsurance.Models.DB;
using System.Drawing;

namespace API.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private AppDbContext _dbContext;

		public CustomerRepository(AppDbContext dbContext) { 
			_dbContext = dbContext;
		}

		public RepositoryResponse Add(CustomerDTO dto)
		{
			try
			{
				_dbContext.Customers.Add(new Customer(dto));
				_dbContext.SaveChanges();
				return new RepositoryResponse
				{
					StatusCode = HttpStatusCode.OK,
					Message = $"Record inserted"
				};
			}
			catch (Exception ex)
			{
				return new RepositoryResponse
				{
					StatusCode = HttpStatusCode.InternalServerError,
					Message = ex.Message
				};
			}


		}

		public IEnumerable<Customer>? GetAll()
		{
			return _dbContext.Customers;
		}

		public Customer? GetById(int id)
		{
			var customer = _dbContext.Customers.FirstOrDefault(x => x.Id == id);
			return customer;
		}

		public RepositoryResponse Update(int id, Customer customerData)
		{
			var matchedCustomer = _dbContext.Customers.FirstOrDefault(x => x.Id == id);
			if (matchedCustomer != null)
			{
				try
				{
					_dbContext.Entry<Customer>(matchedCustomer).CurrentValues.SetValues(customerData);
					_dbContext.SaveChanges();
					return new RepositoryResponse
					{
						StatusCode = HttpStatusCode.OK,
						Message = $"Record updated"
					};
				}
				catch (Exception ex)
				{
					return new RepositoryResponse
					{
						StatusCode = HttpStatusCode.InternalServerError,
						Message = ex.Message
					};
				}
			}

			return new RepositoryResponse { StatusCode = HttpStatusCode.NotFound };
		}

		public RepositoryResponse Delete(int id)
		{
			var matchedCustomer = _dbContext.Customers.FirstOrDefault(x => x.Id == id);
			if (matchedCustomer != null)
			{
				try
				{
					_dbContext.Customers.Remove(matchedCustomer);
					_dbContext.SaveChanges();
					return new RepositoryResponse
					{
						StatusCode = HttpStatusCode.OK,
						Message = $"Record deleted"
					};
				}
				catch (Exception ex)
				{
					return new RepositoryResponse
					{
						StatusCode = HttpStatusCode.InternalServerError,
						Message = ex.Message
					};
				}
			}

			return new RepositoryResponse { StatusCode = HttpStatusCode.NotFound };
		}

	}
}
