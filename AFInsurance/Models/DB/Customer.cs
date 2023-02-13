using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AFInsurance.Models.DB
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
		[MaxLength(50)]
		[MinLength(3)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		[MinLength(3)]
		public string Surname { get; set; }

		[Required]
		public string PolicyReferenceNumber { get; set; }

		public DateTime? DateOfBirth { get; set; }

		public string? EmailAddress { get; set; }


		public Customer() { }

        public Customer(CustomerDTO dto)
        {
            FirstName = dto.FirstName;
            Surname = dto.Surname;
            PolicyReferenceNumber = dto.PolicyReferenceNumber;
            DateOfBirth = dto.DateOfBirth;
            EmailAddress = dto.EmailAddress;
        }

		public Customer(int id, CustomerDTO dto)
		{
			Id = id;
			FirstName = dto.FirstName;
			Surname = dto.Surname;
			PolicyReferenceNumber = dto.PolicyReferenceNumber;
			DateOfBirth = dto.DateOfBirth;
			EmailAddress = dto.EmailAddress;
		}
	}
}
