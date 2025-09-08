namespace MarstonX.Api.Controllers.v1.Users;

[ApiController]
[ApiVersion("1.0")]
[Route("api/MarstonX/v1/[controller]")]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }


    //[Authorize(Policy = "UserAccess")]
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult> GetAllUsers()
    {
        var query = new GetAllUsersQuery();
        var users = await _mediator.Send(query);
        return Ok(users);
    }

    //[Authorize(Policy = "UserAccess")]
    [HttpGet("GetUserById")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var query = new GetUserQuery(id);
        var user = await _mediator.Send(query);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    //[Authorize(Policy = "UserAccess")]
    [HttpPost("createUser")]
    public async Task<ActionResult<int>> CreateUser(CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUser), new { id = userId }, userId);
    }

    //[Authorize(Policy = "UserAccess")]
    [HttpPut("updateUser")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");

        var success = await _mediator.Send(command);

        if (!success)
            return NotFound();

        return NoContent();
    }

    //[Authorize(Policy = "UserAccess")]
    [HttpDelete("DeleteUserById")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var success = await _mediator.Send(new DeleteUserCommand(id));

        if (!success)
            return NotFound();

        return NoContent();
    }
}
