using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class WalksDifficultysController : Controller
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper _mapper;

        public WalksDifficultysController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this._walkDifficultyRepository = walkDifficultyRepository;
            this._mapper = mapper;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllWalksDifficulties()
        {


           var listaWalkDiificulties =  await _walkDifficultyRepository.GetWalkDifficulties();

            if(listaWalkDiificulties == null)
            {
                return null;
            }

            var listaWalkDifficultiesDto = _mapper.Map<List< WalkDifficulty >>(listaWalkDiificulties);

            return Ok(listaWalkDifficultiesDto);


        }

        [HttpGet]
        [Route("{walkDifficultyId:guid}")]
        [ActionName("GetWalkDifficultyByIdAsync")]

        public async Task<IActionResult> GetWalkDifficultyByIdAsync(Guid walkDifficultyId)
        {
            var walkDificultyById = await _walkDifficultyRepository.GetWalkDifficultyById(walkDifficultyId);

            if(walkDificultyById == null)
            {
                return NotFound();
            }

            var mapWalkDifficulty = _mapper.Map<WalkDifficulty>(walkDificultyById);

            return Ok(mapWalkDifficulty);
        }



        [HttpPost]

        public async Task<IActionResult> AddWalkDifficulty(AddNewWalkDifficultyRequestDto addWalkDifficultyRequest)
        {
            // Convertir dto a modelo

            var modelWalkDifficulty = new WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code,
            };

            // Llamamos a la funcion del repositorio

            modelWalkDifficulty = await _walkDifficultyRepository.AddWalkDifficulty(modelWalkDifficulty);

            // Convertir modelo en dto

            var walkDifficultyDto = _mapper.Map<WalkDifficulty>(modelWalkDifficulty);

            return CreatedAtAction(nameof(GetWalkDifficultyByIdAsync), new { walkDifficultyId = walkDifficultyDto.Id }, walkDifficultyDto);

        }

        [HttpPut]
        [Route("{walkDifficultyId:guid}")]

        public async Task<IActionResult> UpdateWalkDifficulty([FromRoute] Guid walkDifficultyId, [FromBody] AddNewWalkDifficultyRequestDto walkDifficultyRequestDto)
        {

            var modelWalkDifficulty = new WalkDifficulty()
            {
                Code = walkDifficultyRequestDto.Code,
            };

            // Call repository to update

            modelWalkDifficulty = await _walkDifficultyRepository.UpdateWalkDifficulty(walkDifficultyId, modelWalkDifficulty);

            if(modelWalkDifficulty == null)
            {
                return NotFound();
            }

            var walkDifficultyDto = _mapper.Map<WalkDifficulty>(modelWalkDifficulty);

            return Ok(walkDifficultyDto);
        }


        [HttpDelete]
        [Route("{walkDifficultyId:guid}")]

        public async Task<IActionResult> RemoveWalkDifficulty([FromRoute] Guid walkDifficultyId)

        {
            var existingWalkDifficulty = await _walkDifficultyRepository.DeleteWalkDifficulty(walkDifficultyId);

            if (existingWalkDifficulty == null)
            {
                return NotFound();
            }

            var existingWalkDifficultyDto = _mapper.Map<WalkDifficulty>(existingWalkDifficulty);

            return Ok(existingWalkDifficultyDto);

        }





    }


}
