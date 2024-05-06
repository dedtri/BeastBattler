using BeastBattler.Backend.Database;
using BeastBattler.Backend.Database.DataModels;
using BeastBattler.Backend.Repositories;
using BeastBattler.Shared.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace BeastBattler.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly UserRepository userRepository;

        public UserController
            (
            UnitOfWork unitOfWork,
            UserRepository userRepository
            )
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
        }

        #region Login
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] User login)
        {
            if (login is null)
            {
                return BadRequest("Invalid client request");
            }

            var user = this.userRepository.ConfirmLogin(login);

            if (user is null)
                return Unauthorized();

            return Ok(user.Id);
        }
        #endregion 

        #region Create
        [HttpPost, Route("register")]
        public async Task<IActionResult> Create(User login)
        {
            var entity = await this.userRepository.GetByEmailAsync(login.Email);
            if (entity != null)
                return BadRequest("User already exists");

            login.Rank = UserRoleTypeEnum.CAGE_JANITOR;

            this.userRepository.Add(login);

            await this.unitOfWork.SaveChangesAsync();

            return Ok(login.Id);
        }
        #endregion

        #region GetById
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var login = await this.userRepository.GetAsync(id, true);

            if (login is null)
                return NotFound();

            login.Password = string.Empty;

            return Ok(login);
        }
        #endregion

        #region Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User login)
        {
            if (id != login.Id) 
                return BadRequest();

            this.userRepository.Update(login);

            await this.unitOfWork.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Delete
        [HttpDelete("delete/{entityId}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await userRepository.GetAsync(id);
            if (user == null)
                return NotFound();

            try
            {
                userRepository.Remove(user);

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
