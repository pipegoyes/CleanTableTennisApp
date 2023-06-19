using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Domain.Entities;

namespace CleanTableTennisApp.Application.Common.Converters;

public interface ITeamMatchConverter
{
    TeamMatchDto ToDto(TeamMatch match);
}