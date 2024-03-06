﻿
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using ReactApp2.Server.Models;
using ReactApp2.Server.Respository;
using System.Security.Claims;

namespace ReactApp2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly CustomerDataAccess _dataAccess;

        public ClientController(CustomerDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpPost("{id}/RiskType")]
        public ActionResult UpdateRiskType(int id, [FromBody] string riskType)
        {
            try
            {
                _dataAccess.UpdateRiskType(id, riskType);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id}")]
        public ActionResult<Customer> GetClient(int id)
        {
            // TODO: Implement this method
            return NotFound();
        }

    }
}
