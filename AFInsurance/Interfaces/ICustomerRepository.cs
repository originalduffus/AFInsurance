using AFInsurance.Models;
using AFInsurance.Models.DB;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface ICustomerRepository
	{
		IEnumerable<Customer>? GetAll();
		Customer? GetById(int id);
		RepositoryResponse Add(CustomerDTO dto);
		RepositoryResponse Update(int id, Customer customerData);
		RepositoryResponse Delete(int id);
	}
}
