using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // Especificamos que este controlador es un controlador de API
    [ApiController]
    // Especificamos el nombre de la ruta
    [Route("[controller]")]
    public class RegionsController : Controller
    {

        
        private readonly IRegionRepository _regionsRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionsRepository, IMapper mapper)
        {
            this._regionsRepository = regionsRepository;
            this._mapper = mapper;
        }


        // Especificamos que este método es un método de acción get
        [HttpGet]
        public async Task<IActionResult> GetAllRegions() 
        {

            var listaRegiones = await _regionsRepository.GetAllRegionsAsync();

            // Devolver DTO Regions
            // Creamos una nueva lista de regionesDto

            //var regionsDto = new List<Models.DTO.RegionDto>();

            //// Recorremos la lista de regiones
            //listaRegiones.ToList().ForEach(region =>
            //{
            //    // Creamos una nueva regionDto

            //    var regionDTO = new Models.DTO.RegionDto()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population
            //    };

            //    // Añadimos la regionDto a la lista de regionesDto

            //    regionsDto.Add(regionDTO);
            //});

            var regionsDto = _mapper.Map<List<RegionDto>>(listaRegiones);
            
            return Ok(regionsDto);


        }

        // Metodo para obtener una region por su id
        [HttpGet]
        [Route("{regionId:guid}")]
        [ActionName("GetRegionById")]

        public async Task<IActionResult> GetRegionById(Guid regionId)
        {
            var regionSegunId = await _regionsRepository.GetRegionByIdAsync(regionId);

            if (regionSegunId == null)
            {
                return NotFound();
            }

            var regionSegunIDto = _mapper.Map<RegionDto>(regionSegunId);


            return Ok(regionSegunIDto);
            
        }

        // Metodo para obtener una lista de regiones segun su nombre
        [HttpGet]
        [Route("{regionName}")]

        public async Task<IActionResult> GetRegionByName(string regionName)
        {
            var regionSegunNombre = await _regionsRepository.GetRegionByNameAsync(regionName);

            if (regionSegunNombre == null)
            {
                return BadRequest();
            }

            var regionSegunNombreDto = _mapper.Map<List<RegionDto>>(regionSegunNombre);

            return Ok(regionSegunNombreDto);

        }

        [HttpPost]
        
        public async Task<IActionResult> PostNewRegion(AddRegionRequestDto addRegionRequest)
        {

            if(addRegionRequest == null)
            {
                return BadRequest(ModelState);
            }
            // Request(DTO) to Domain Model

            var regionNueva = new Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population

            };


            // Pasar los detalles de la nueva region al repositorio

            var respuesta = await _regionsRepository.AddRegionAsync(regionNueva);


            // Convertirlo en el DTO

            var regionDto = new RegionDto
            {   
                Id = respuesta.Id,
                Code = respuesta.Code,
                Name = respuesta.Name,
                Area = respuesta.Area,
                Lat = respuesta.Lat,
                Long = respuesta.Long,
                Population = respuesta.Population
            };

            return CreatedAtAction(nameof(GetRegionById), new { regionId = regionDto.Id }, regionDto);

        }

        // Eliminar

        [HttpDelete]
        [Route("{regionId:guid}")]

        public async Task<IActionResult> DeleteRegion(Guid regionId)
        {
            // Obtener la region de la ddbb

            var delete = await _regionsRepository.DeleteRegionAsync(regionId);
            // si es nula, not found
            if (delete == null)
            {
                return NotFound();
            }
            // convertir la respuesta en la DTO

            var regionDto = new RegionDto
            {
                Id = delete.Id,
                Code = delete.Code,
                Name = delete.Name,
                Area = delete.Area,
                Lat = delete.Lat,
                Long = delete.Long,
                Population = delete.Population
            };

            // devolver la dto

            return Ok(regionDto);
        }



        // Update region
        [HttpPut]
        [Route("{regionId:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid regionId, [FromBody] UpdateRegionRequestDto updateRegionRequest)
        {

            // Convertir DTO en modelo

            var region = new Region
            {
                
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population
            };

            // updatear la region
            region = await _regionsRepository.UpdateRegionAsync(regionId, region);
            
            // si es nula devuelve not found

            if(region == null)
            {
                return NotFound();
            }

            // Convertir modelo en dto

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            // return Ok

            return Ok(regionDto);
        }




    }
}
