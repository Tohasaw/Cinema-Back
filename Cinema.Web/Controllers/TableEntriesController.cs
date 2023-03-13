using Cinema.Data.Features.TableEntries.Commands.DeleteTableEntry;
using Cinema.Data.Features.TableEntries.Commands.UpdateTableEntry;
using Cinema.Data.Features.TableEntries.Queries.GetTableEntries;
using Cinema.Data.Features.TableEntries.Queries.GetTableEntriesMovies;
using Cinema.Data.Features.TableEntries.Queries.GetTableEntry;
using Cinema.Data.Features.TableEntries.Commands.CreateTableEntry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers;
[Route("api/entries")]
public class TableEntriesController : Controller
{
    //[HttpGet]
    //public async Task<IActionResult> GetTableEntriesAsync(
    //    [FromQuery] CancellationToken cancellationToken)
    //{
    //    return Ok(await Mediator.Send(new GetTableEntriesQuery(), cancellationToken));
    //}

    [HttpGet]
    [Authorize(Roles = "admin,cassir,controler")]
    public async Task<IActionResult> GetTableEntriesMoviesAsync(
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetTableEntriesMoviesQuery(), cancellationToken));
    }

    [HttpGet("{entryId:int}", Name = nameof(GetTableEntryAsync))]
    [Authorize(Roles = "admin,cassir,controler")]
    public async Task<IActionResult> GetTableEntryAsync(
        [FromRoute] int entryId,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetTableEntryQuery(entryId), cancellationToken));
    }
    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateTableEntryAsync(
        [FromBody] CreateTableEntryDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        var entryId = await Mediator.Send(
            new CreateTableEntryCommand(dto),
            cancellationToken);

        return CreatedAtRoute(
            nameof(GetTableEntryAsync),
            new
            {
                entryId
            },
            entryId);
    }

    [HttpPut("{entryId:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateTableEntryAsync(
    [FromRoute] int entryId,
    [FromBody] UpdateTableEntryDto dto,
    [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new UpdateTableEntryCommand(entryId, dto),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{entryId:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteTableEntryAsync(
        [FromRoute] int entryId,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteTableEntryCommand(entryId), cancellationToken);

        return NoContent();
    }
}

