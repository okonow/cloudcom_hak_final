using Application.Common.Models.DepartmentModels;
using Application.Departments.Commands;
using Application.Departments.Commands.PatchCommands;
using Application.Departments.Commands.PurgeCommands;
using Application.Departments.Queries;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWorkspaceService.Controllers
{
    [ApiController]
    public class DepartmentController(ISender sender) : BaseController(sender)
    {
        [Authorize(Policy = Polices.MustBeAdministrator)]
        [HttpPost]
        public async Task<Guid> CreateDepartmentAsync([FromBody] CreateDepartmentCommand command)
            => await _sender.Send(command);

        #region Get queriests

        [Authorize]
        [HttpGet]
        public async Task<DepartmentMainInfoVM> GetDepartmentInformationAsync([FromHeader] Guid departmentId)
            => await _sender.Send(new GetDepartmentMainInfoQuery { DepartmentId = departmentId });

        [Authorize]
        [HttpGet]
        public async Task<DepartmentVM> GetDepartmentFullInformationAsync([FromHeader] Guid departmentId)
            => await _sender.Send(new GetDepartmentQuery { DepartmentId = departmentId });

        [Authorize]
        [HttpGet]
        public async Task<IList<DepartmentVM>> GetDepartmentsFullInformationAsync()
            => await _sender.Send(new GetDepartmentsQuery());

        [Authorize]
        [HttpGet]
        public async Task<DepartmentStaffVM> GetDepartmentStaffAsync([FromHeader] Guid departmentId)
            => await _sender.Send(new GetDepartmentStaffQuery { DepartmentId = departmentId });

        [Authorize]
        [HttpGet]
        public async Task<DepartmentVM> GetDepartmentOfUserAsync([FromHeader] Guid userId)
            => await _sender.Send(new GetUserDepartmentQuery { UserId = userId });

        #endregion

        #region Update commands

        [Authorize(Policy = Polices.MustBeDirectorOrAdministrator)]
        [HttpPut]
        public async Task UpdateDepartmentAsync([FromBody] UpdateDepartmentCommand command)
            => await _sender.Send(command);

        [Authorize(Policy = Polices.MustBeDirectorOrAdministrator)]
        [HttpPatch]
        public async Task AddEmployeeInDepartmentAsync([FromBody] AddEmployeeInDepartmentCommand command)
            => await _sender.Send(command);

        [Authorize(Policy = Polices.MustBeAdministrator)]
        [HttpPatch]
        public async Task ChangeDirectorInDepartmentAsync([FromBody] ChangeDirectorInDepartmentCommand command)
            => await _sender.Send(command);

        [Authorize(Policy = Polices.MustBeAdministrator)]
        [HttpPatch]
        public async Task ChangeDepartmentBaseInformationAsync([FromBody] ChangeDepartmentMainInfoCommand command)
            => await _sender.Send(command);

        #endregion

        #region Delete commands

        [Authorize(Policy = Polices.MustBeAdministrator)]
        [HttpDelete]
        public async Task DeleteDirectorFromDepartmentAsync([FromHeader] Guid departmentId)
            => await _sender.Send(new PurgeDirectorFromDepartmentCommand { DepartmentId = departmentId });

        [Authorize(Policy = Polices.MustBeDirectorOrAdministrator)]
        [Authorize]
        [HttpDelete]
        public async Task DeleteEmployeeFromDepartmentAsync([FromBody] PurgeEmployeeFromDepartmentCommand command)
            => await _sender.Send(command);

        [Authorize(Policy = Polices.MustBeDirectorOrAdministrator)]
        [HttpDelete]
        public async Task DeleteAllEmployeesFromDepartmentAsync([FromHeader] Guid departmentId)
            => await _sender.Send(new PurgeEmployeesFromCompanyCommand { DepartmentId = departmentId });

        [Authorize(Policy = Polices.MustBeDirectorOrAdministrator)]
        [HttpDelete]
        public async Task DeleteDepartmentAsync([FromHeader] Guid departmentId)
            => await _sender.Send(new DeleteDepartmentCommand { DepartmentId = departmentId });

        #endregion
    }
}
