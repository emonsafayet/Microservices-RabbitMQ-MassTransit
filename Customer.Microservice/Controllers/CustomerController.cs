using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Customer.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    { private readonly string connectionString = "data source=DESKTOP-QVEKPLI; initial catalog=customerDb;integrated security=true";

        // GET: api/<CustomerController>
        [HttpGet]
       public async Task<IEnumerable<Models.Customer>> GetCustomers()
        {
            IEnumerable<Models.Customer> customers;

            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlquery = "select *from Customer";
                customers = await connection.QueryAsync<Models.Customer>(sqlquery); 
            }
            return customers;
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        
        public async Task<Models.Customer> GetCustomerById(int id)
        {
            Models.Customer customer = new Models.Customer();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlquery = "select *from Customer Where Id = @Id";
                customer = await connection.QuerySingleAsync<Models.Customer>(sqlquery,new {Id=id });
            }
            return customer;
        }

        // POST api/<CustomerController>
        [HttpPost]
       public async Task<ActionResult> Create([FromBody] Models.Customer customer)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "Insert into customer(Name,Address,Telephone,Email) Values(@Name,@Address,@Telephone,@Email)";
                await connection.ExecuteAsync(sqlQuery,customer);
            }
            return Ok();
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] Models.Customer customer)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "Update Customer SET Name=@Name,Address=@Address,Telephone=@Telephone,Email=@Email WHERE Id=@Id";
                await connection.ExecuteAsync(sqlQuery, customer);
            }
            return Ok();
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "Delete Customer WHERE Id=@Id";
                await connection.ExecuteAsync(sqlQuery, new {Id =id });
            }
            return Ok();
        }
    }
}
