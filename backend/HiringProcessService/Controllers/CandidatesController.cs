using Application.Candidates.Commands;
using Application.Candidates.Queries;
using Domain.Entities;
using HiringProcessService.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HiringProcessService.API.Controllers;

[Route("api/candidates")]
[ApiController]
public class CandidatesController : ControllerBase
{
    private readonly IMediator _mediator;
    public CandidatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CandidateModel>>> GetCandidates()
    {
        var query = new GetCandidatesQuery();

        var candidatesResult = await _mediator.Send(query);

        var candidates = candidatesResult.Value;

        if (candidates is null) return NotFound();

        return Ok(candidatesResult.Value);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CandidateModel>> GetCandidate(Guid id)
    {
        var query = new GetCandidateByIdQuery(id);
        var candidateResult = await _mediator.Send(query);

        if (candidateResult is null ||
            (!candidateResult.IsSuccess
                && candidateResult.Error is not null
                && candidateResult.Error.Type.Equals("Candidate.NotFound", StringComparison.CurrentCultureIgnoreCase)))
            return NotFound();

        return Ok(candidateResult.Value);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCandidate(CandidateModel candidate)
    {
        var createCandidateCommand = new CreateCandidateCommand(candidate.Name, candidate.Stage, candidate.Phone, candidate.Email);
        var id = (await _mediator.Send(createCandidateCommand)).Value;
        return CreatedAtAction("GetCandidate", new { id }, id);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCandidate(Guid id, CandidateModel candidate)
    {
        var command = new UpdateCandidateCommand(id, Candidate.Create(id, candidate.Name, candidate.Stage, candidate.Phone, candidate.Email));

        if (id != command.candidateId)
        {
            return BadRequest();
        }

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCandidate(Guid id)
    {
        await _mediator.Send(new DeleteCandidateCommand(id));
        return NoContent();
    }
}
