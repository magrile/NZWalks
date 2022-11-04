using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this._walkRepository = walkRepository;
            this._mapper = mapper;
        }





        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walks = await _walkRepository.GetAllWalksAsync();

            var walksDto = _mapper.Map<List<WalkDto>>(walks);

            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{walkId:guid}")]
        [ActionName("")]

        public async Task<IActionResult> GetWalkById(Guid walkId)
        {

            var walks = await _walkRepository.GetWalkById(walkId);

            if (walks == null)
            {
                return NotFound();
            }

            var walksDto = _mapper.Map<WalkDto>(walks);

            return Ok(walksDto);
        }

        [HttpPost]
        
        public async Task<IActionResult> AddNewWalk([FromBody] AddNewWalkRequestDto addwalk)
        {
            // Convertir el dto en un objeto de modelo

            var walkModelo = new Walk()
            {
                Length = addwalk.Length,
                Name = addwalk.Name,
                WalkDifficultyId = addwalk.WalkDifficultyId,
                RegionId = addwalk.RegionId

            };

            // Pasar el objeto al repositorio para guardarlo

            walkModelo = await _walkRepository.AddNewWalk(walkModelo);

            // Convertir el modelo en dto

            var walkDto = new WalkDto()
            {
                Id = walkModelo.Id,
                Length = walkModelo.Length,
                Name = walkModelo.Name,
                WalkDifficultyId = walkModelo.WalkDifficultyId,
                RegionId = walkModelo.RegionId

            };

            // Enviar la respuesta del dto de vuelta al cliente

            return CreatedAtAction(nameof(GetAllWalks), new { walkId = walkDto.Id}, walkDto);

        }
    }
}
