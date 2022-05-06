using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CompanyEmployees.ActionFilters;
using CompanyEmployees.ModelBinders;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet("{id}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid id)
        {


            var company = _repository.Company.GetCompany(id, trackchanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id : {id} doesnt exist in database.");
                return NotFound();
            }
            else
            {
                var companiesDto = _mapper.Map<CompanyDto>(company);
                return Ok(companiesDto);
            }

        }


        [HttpGet("collection/({ids})", Name = "CompanyCollection")]
        public IActionResult GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var companyEntities = _repository.Company.GetByIds(ids, trackChanges: false);

            if (ids.Count() != companyEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            return Ok(companiesToReturn);
        }


        //[HttpPost("collection")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //public IActionResult CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
        //{

        //    var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
        //    foreach (var company in companyEntities)
        //    {
        //        _repository.Company.CreateCompany(company);
        //    }

        //     _repository.Save();
        //    var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        //    var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));
        //    return CreatedAtRoute("CompanyCollection", new { ids }, companyCollectionToReturn);

        //}

        [HttpDelete("{id}")]
        public  IActionResult DeleteCompany(Guid id)
        {
            var company =  _repository.Company.GetCompany(id, trackchanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Company.DeleteCompany(company);
             _repository.Save();
            return NoContent();

        }


        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public  IActionResult UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
        {
            var companyEntity =  _repository.Company.GetCompany(id, trackchanges: true);
            if (companyEntity == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(company, companyEntity);
            _repository.Save();
            return NoContent();
        }




        [HttpPost]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public  IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {

            if (company == null)
            {
                _logger.LogError("CompanyForCreationDto object sent from client is null.");
                return BadRequest("CompanyForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CompanyForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var companyEntity = _mapper.Map<Company>(company);
            _repository.Company.CreateCompany(companyEntity);
            _repository.Save();
            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
            return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id },
           companyToReturn);

        }

        [HttpOptions]
        public IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }



    }
}


//var companyEntity = _mapper.Map<Company>(company);
//_repository.Company.CreateCompany(companyEntity);
//           await _repository.SaveAsync();


//var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
//            return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id },
//            companyToReturn);