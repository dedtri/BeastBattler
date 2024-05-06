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
    public class CreatureController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly CreatureRepository creatureRepository;

        public CreatureController(
            UnitOfWork unitOfWork,
            CreatureRepository creatureRepository)
        {
            this.unitOfWork = unitOfWork;
            this.creatureRepository = creatureRepository;
        }

        #region Create
        [HttpPost()]
        public async Task<ActionResult<Creature>> Create([FromBody] Creature creature)
        {
            Random random = new Random();

            creature.Status = false;

            creature.Hitpoints = random.Next(1, 100);

            creature.Attack = random.Next(1, 101);

            // Add entity
            this.creatureRepository.Add(creature);

            // Save changes
            await this.unitOfWork.SaveChangesAsync();

            return Ok(creature);
        }
        #endregion

        #region Get
        [HttpGet("{entityId}")]
        public async Task<ActionResult<Creature>> Get(int entityId)
        {
            // Get entity
            var entity = await this.creatureRepository.GetAsync(entityId, true);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }
        #endregion

        #region GetAll
        [HttpGet]
        public async Task<ActionResult<BaseQueryResult<Creature>>> GetAll([FromQuery] CreatureQuery query)
        {
            // Get entities
            var queryResult = await this.creatureRepository.GetAllAsync(query, true);

            return queryResult;
        }
        #endregion

        #region Update
        [HttpPut("{entityId}")]
        public async Task<ActionResult> Update(int entityId, [FromBody] Creature creature)
        {
            // Get entitty
            var entity = await this.creatureRepository.GetAsync(entityId, true);
            if (entity == null)
                return NotFound();

            entity.Name = creature.Name;
            entity.Race = creature.Race;
            entity.CageId = creature.CageId;
            if (creature.UserId != null)
            {
                entity.UserId = creature.UserId;
                entity.Status = true;
            }
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
            var entity = await creatureRepository.GetAsync(entityId);
            if (entity == null)
                return NotFound();

            //Delete entity
            try
            {
                creatureRepository.Remove(entity);

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
