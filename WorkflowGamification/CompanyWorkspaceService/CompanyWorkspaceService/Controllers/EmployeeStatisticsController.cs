using Application.Common.Models;
using Application.Statistics.Commands;
using Application.Statistics.Queries;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWorkspaceService.Controllers
{
    [ApiController]
    public class EmployeeStatisticsController(ISender sender) : BaseController(sender)
    {
        [Authorize(Policy = Polices.MustBeAdministrator)]
        [HttpPost]
        public async Task<Guid> CreateEmployeeStatistics([FromBody] CreateEmployeeStatisticsCommand command)
            => await _sender.Send(command);

        [Authorize]
        [HttpGet]
        public async Task<EmployeeStatisticsVM> GetEmployeeStatisticsAsync([FromHeader] Guid employeeId) 
            => await _sender.Send( new GetEmployeeStatisticsQuery() { EmployeeId = employeeId});

        [Authorize]
        [HttpGet]
        public async Task<IList<EmployeeStatisticsVM>> GetStatisticsRatingInDepartmentAsync([FromBody] Guid departmentId)
            => await _sender.Send(new GetEmployeesStatisticsRatingInDepartmentQuery { DepartmentId = departmentId });

        [Authorize]
        [HttpPut]
        public async Task UpdateEmployeeStatisticsAsync([FromBody] UpdateEmployeeStatisticsCommand command)
            => await _sender.Send(command);

        [Authorize]
        [HttpDelete]
        public async Task DeleteEmployeeStatisticsAsync([FromHeader] Guid employeeId)
            => await _sender.Send(new DeleteEmployeeStatisticsCommand() { EmployeeId = employeeId });

    }
}
