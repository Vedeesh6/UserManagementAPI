using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> Users = new List<User>
{
new User
{
Id = 1,
Name = "Alice Johnson",
Email = "alice@example.com",
Department = "HR"
},
new User
{
Id = 2,
Name = "Bob Smith",
Email = "bob@example.com",
Department = "IT"
}
};
        private static int nextId = 3;

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(Users);
        }

        // GET: api/users/1
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            User? user = Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new
                {
                    error = "User not found."
                });
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            user.Id = nextId++;
            Users.Add(user);

            return CreatedAtAction(
                nameof(GetUser),
                new { id = user.Id },
                user
            );
        }

        // PUT: api/users/1
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            User? existingUser = Users.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return NotFound(new
                {
                    error = "User not found."
                });
            }

            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;
            existingUser.Department = updatedUser.Department;

            return Ok(existingUser);
        }

        // DELETE: api/users/1
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            User? user = Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new
                {
                    error = "User not found."
                });
            }

            Users.Remove(user);

            return Ok(new
            {
                message = "User deleted successfully."
            });
        }
    }

}
