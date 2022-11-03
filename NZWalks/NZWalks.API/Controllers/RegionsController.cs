using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
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

            var regionsDto = _mapper.Map<List<Region>>(listaRegiones);
            
            return Ok(regionsDto);


        }
            
        
    }
}
