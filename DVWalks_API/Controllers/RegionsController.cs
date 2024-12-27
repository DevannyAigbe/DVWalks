using DVWalks_API.Data;
using DVWalks_API.Models.Domain;
using DVWalks_API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace DVWalks_API.Controllers
{
    // https://localhost:7233/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly DVWalksDbContext dbContext;

        public RegionsController(DVWalksDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        // Get All REGIONS
        // Get : https://localhost: portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get Data From Database - Domain models

           var regionsDomain = dbContext.Regions.ToList();

            // Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl

                });
            }
            //Return DTOs
            return Ok(regionsDto);
            }

        // GET SINGLE REGIONS (Get  Region By ID)
        //GET: https://localhost:portnumer/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById(Guid  id) 
        
        {
                //var region = dbContext.Region.Find(id);
                // Get Region Domain Model From Database
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id ==id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map/Convert Region Domain Model to Region DTO
            //
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            //Return DTO back to client
            return Ok(regionDto);
        }
            // POST To  Create New Region
            // POST: https://localhost:portnumber/api/regions
            [HttpPost]
            public IActionResult Create([FromBody]AddRegionRequestDto addRegionRequestDto)
            {
                //Map or Convert DTO to Domain Model
                var regionDomainModel = new Region
                {
                    Code = addRegionRequestDto.Code,
                    Name = addRegionRequestDto.Name,
                    RegionImageUrl = addRegionRequestDto.RegionImageUrl,
                };

                // Use Domain Model to create Region
                dbContext.Regions.Add(regionDomainModel);
                dbContext.SaveChanges();

            //Map Domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };



                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDomainModel);
            }
    }
}
