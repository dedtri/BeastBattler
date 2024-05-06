using BeastBattler.Backend.Database;
using BeastBattler.Backend.Database.DataModels;
using BeastBattler.Backend.Database.QueryModels;
using BeastBattler.Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheGeneralStore.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CageController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly CageRepository cageRepository;

        public CageController(
            UnitOfWork unitOfWork,
            CageRepository cageRepository)
        {
            this.unitOfWork = unitOfWork;
            this.cageRepository = cageRepository;
        }

        #region Create
        [HttpPost()]
        public async Task<ActionResult<Cage>> Create([FromBody] Cage cage)
        {
            cage.Status = false;

            // Add entity
            this.cageRepository.Add(cage);

            // Save changes
            await this.unitOfWork.SaveChangesAsync();

            return Ok(cage);
        }
        #endregion

        #region Get
        [HttpGet("{entityId}")]
        public async Task<ActionResult<Cage>> Get(int entityId)
        {
            // Get entity
            var entity = await this.cageRepository.GetAsync(entityId, true);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }
        #endregion

        #region GetAll
        [HttpGet]
        public async Task<ActionResult<BaseQueryResult<Cage>>> GetAll([FromQuery] CageQuery query)
        {
            // Get entities
            var queryResult = await this.cageRepository.GetAllAsync(query, true);

            return queryResult;
        }
        #endregion

        #region Update
        [HttpPut("{entityId}")]
        public async Task<ActionResult> Update(int entityId, [FromBody] Cage cage)
        {
            // Get entitty
            var entity = await this.cageRepository.GetAsync(entityId, true);
            if (entity == null)
                return NotFound();

            // Update entity
            this.cageRepository.Update(cage);

            // Save changes
            await this.unitOfWork.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Delete
        [HttpDelete("delete/{entityId}")]
        public async Task<ActionResult> Delete(int entityId)
        {
            // Get entitty
            var entity = await cageRepository.GetAsync(entityId);
            if (entity == null)
                return NotFound();

            //Delete entity
            try
            {
                cageRepository.Remove(entity);

                await this.unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }
        #endregion
    }
}
