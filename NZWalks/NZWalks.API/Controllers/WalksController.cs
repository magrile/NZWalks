using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Drawing;

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

            return CreatedAtAction(nameof(GetAllWalks), new { walkId = walkDto.Id }, walkDto);

        }

        [HttpPut]
        [Route("{walkId:guid}")]

        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid walkId, [FromBody] UpdateWalkRequestDto walkRequest)
        {
            // Convertir DTO en modelo

            var walk = new Walk
            {

                Length = walkRequest.Length,
                Name = walkRequest.Name,
                RegionId = walkRequest.RegionId,
                WalkDifficultyId = walkRequest.WalkDifficultyId,
                
            };

            // updatear la region
            walk = await _walkRepository.UpdateWalk(walkId, walk);

            // si es nula devuelve not found

            if (walk == null)
            {
                return NotFound();
            }

            // Convertir modelo en dto

            var walkDto = new WalkDto
            {
                Id = walk.Id,
                Length = walk.Length,
                Name = walk.Name,
                WalkDifficultyId = walk.WalkDifficultyId,
                RegionId = walk.RegionId
            };
            // return Ok

            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{walkId:guid}")]

        public async Task<IActionResult> DeleteWalkById(Guid walkId)
        {

           var walkModel = await _walkRepository.DeleteWalk(walkId);

            if(walkModel == null)
            {
                return NotFound();
            }

            var walkDto = _mapper.Map<Walk>(walkModel);

            return Ok(walkDto);
        }
    }
}
