using Microsoft.AspNetCore.Mvc;
using TravelTime.Backend.Database;
using TravelTime.Backend.Database.DataModels;
using TravelTime.Backend.Database.QueryModels;
using TravelTime.Backend.Repositories;

namespace TravelTime.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly TripRepository _tripRepository;

        public TripController(
            UnitOfWork unitOfWork,
            TripRepository tripRepository)
        {
            _unitOfWork = unitOfWork;
            _tripRepository = tripRepository;
        }

        #region Create
        [HttpPost()]
        public async Task<ActionResult<Trip>> Create([FromBody] Trip trip)
        {
            // Add entity
            _tripRepository.Add(trip);

            trip.CreatedAt = DateTime.Now;

            // Save changes
            await _unitOfWork.SaveChangesAsync();

            return Ok(trip);
        }
        #endregion

        #region Get
        [HttpGet("{entityId}")]
        public async Task<ActionResult<Trip>> Get(int entityId)
        {
            // Get entity
            var entity = await _tripRepository.GetAsync(entityId, true);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }
        #endregion

        #region GetAll
        [HttpGet]
        public async Task<ActionResult<BaseQueryResult<Trip>>> GetAll([FromQuery] TripQuery query)
        {
            // Get entities
            var queryResult = await _tripRepository.GetAllAsync(query, true);

            return queryResult;
        }
        #endregion

        #region Update
        [HttpPut("{entityId}")]
        public async Task<ActionResult> Update(int entityId, [FromBody] Trip trip)
        {
            // Get entitty
            var entity = await _tripRepository.GetAsync(entityId, true);
            if (entity == null)
                return NotFound();

            // Update entity
            _tripRepository.Update(trip);

            // Save changes
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Delete
        [HttpDelete("delete/{entityId}")]
        public async Task<ActionResult> Delete(int entityId)
        {
            // Get entitty
            var entity = await _tripRepository.GetAsync(entityId);
            if (entity == null)
                return NotFound();

            //Delete entity
            try
            {
                _tripRepository.Remove(entity);

                await _unitOfWork.SaveChangesAsync();
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
