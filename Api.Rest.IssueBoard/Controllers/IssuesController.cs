using Api.Rest.IssueBoard.Data;
using Api.Rest.IssueBoard.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Rest.IssueBoard;

namespace Api.Rest.IssueBoard.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssuesController : ControllerBase
{
    private readonly IssuesDbContext _context;

    public IssuesController(IssuesDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssues()
    {
        var issues = await _context.Issues
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync();

        return Ok(issues.Select(IssueMapper.ToDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IssueDto>> GetIssue(int id)
    {
        var issue = await _context.Issues.FindAsync(id);

        if (issue == null)
        {
            return NotFound();
        }

        return Ok(IssueMapper.ToDto(issue));
    }

    [HttpPost]
    public async Task<ActionResult<IssueDto>> CreateIssue(CreateIssueDto dto)
    {
        var issue = IssueMapper.ToModel(dto);
        _context.Issues.Add(issue);
        await _context.SaveChangesAsync();

        var resultDto = IssueMapper.ToDto(issue);
        return CreatedAtAction(nameof(GetIssue), new { id = issue.Id }, resultDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIssue(int id, UpdateIssueDto dto)
    {
        var issue = await _context.Issues.FindAsync(id);

        if (issue == null)
        {
            return NotFound();
        }

        IssueMapper.UpdateModel(issue, dto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIssue(int id)
    {
        var issue = await _context.Issues.FindAsync(id);

        if (issue == null)
        {
            return NotFound();
        }

        _context.Issues.Remove(issue);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
