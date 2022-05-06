﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompanyEmployees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        

        public WeatherForecastController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //_repository.Company.AnyMethodFromCompanyRepository();
            //_repository.Employee.AnyMethodFromEmployeeRepository();
            //_logger.LogInfo("Here is info message from our values controller.");
            //_logger.LogDebug("Here is debug message from our values controller.");
            //_logger.LogWarn("Here is warn message from our values controller.");
            //_logger.LogError("Here is an error message from our values controller.");

            return new string[] { "value1", "value2" };
        }
    }
}
