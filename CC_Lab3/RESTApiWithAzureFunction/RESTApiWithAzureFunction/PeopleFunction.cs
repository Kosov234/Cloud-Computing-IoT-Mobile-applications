using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RESTApiWithAzureFunction.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RESTApiWithAzureFunction
{
    class PeopleFunction
    {
        [FunctionName("GetPeople")]
        public static async Task<IActionResult> GetPeople(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "people")] HttpRequest req, ILogger log)
        {
            List<PersonModel> PersonList = new List<PersonModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"Select * from People";
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        PersonModel task = new PersonModel()
                        {
                            PersonId = (int)reader["PersonId"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString()
                        };
                        PersonList.Add(task);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            if (PersonList.Count > 0)
            {
                return new OkObjectResult(PersonList);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        [FunctionName("GetPersonById")]
        public static IActionResult GetPersonById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "people/{id}")] HttpRequest req, ILogger log, int id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"Select * from People Where PersonId = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dt);
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            if (dt.Rows.Count == 0)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(dt);
        }

        [FunctionName("CreatePerson")]
        public static async Task<IActionResult> CreatePerson(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "people")] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<CreatePersonModel>(requestBody);
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    if (!String.IsNullOrEmpty(input.FirstName) && !String.IsNullOrEmpty(input.LastName) && !String.IsNullOrEmpty(input.PhoneNumber))
                    {
                        var query = $"INSERT INTO People VALUES('{input.FirstName}', '{input.LastName}' , '{input.PhoneNumber}')";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
            return new OkResult();
        }

        [FunctionName("DeletePerson")]
        public static IActionResult DeletePerson(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "people/{id}")] HttpRequest req, ILogger log, int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"Delete from People Where PersonId = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
            return new OkResult();
        }
    }
}
