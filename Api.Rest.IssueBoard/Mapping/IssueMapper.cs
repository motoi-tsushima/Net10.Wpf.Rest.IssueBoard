using Api.Rest.IssueBoard.Models;
using Shared.Rest.IssueBoard;

namespace Api.Rest.IssueBoard.Mapping;

public static class IssueMapper
{
    public static IssueDto ToDto(Issue model)
    {
        return new IssueDto
        {
            Id = model.Id,
            AuthorName = model.AuthorName,
            CreatedAt = model.CreatedAt,
            Category = model.Category,
            Title = model.Title,
            Description = model.Description,
            Status = (IssueStatus)model.Status,
            Resolution = model.Resolution,
            ResolverName = model.ResolverName,
            ResolvedAt = model.ResolvedAt
        };
    }

    public static Issue ToModel(CreateIssueDto dto)
    {
        return new Issue
        {
            AuthorName = dto.AuthorName,
            CreatedAt = DateTime.Now,
            Category = dto.Category,
            Title = dto.Title,
            Description = dto.Description,
            Status = (int)IssueStatus.NotStarted,
            Resolution = null,
            ResolverName = null,
            ResolvedAt = null
        };
    }

    public static void UpdateModel(Issue model, UpdateIssueDto dto)
    {
        model.Category = dto.Category;
        model.Title = dto.Title;
        model.Description = dto.Description;
        model.Status = (int)dto.Status;
        model.Resolution = dto.Resolution;
        model.ResolverName = dto.ResolverName;
        model.ResolvedAt = DateTime.Now;
    }
}
