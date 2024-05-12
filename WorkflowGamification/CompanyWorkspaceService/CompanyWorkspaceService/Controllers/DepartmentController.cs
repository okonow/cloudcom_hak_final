using Application.Common.Models.DepartmentModels;
using Application.Departments.Commands;
using Application.Departments.Commands.PatchCommands;
using Application.Departments.Commands.PurgeCommands;
using Application.Departments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWorkspaceService.Controllers
{
    [ApiController]
    public class DepartmentController(ISender sender) : BaseController(sender)
    {
        [HttpPost]
        public async Task<Guid> CreateDepartmentAsync([FromBody] CreateDepartmentCommand command)
            => await _sender.Send(command);

        #region Get queries

        [HttpGet]
        public async Task<DepartmentMainInfoVM> GetDepartmentInformationAsync([FromHeader] Guid departmentId)
            => await _sender.Send(new GetDepartmentMainInfoQuery { DepartmentId = departmentId });

        [HttpGet]
        public async Task<DepartmentVM> GetDepartmentFullInformationAsync([FromHeader] Guid departmentId)
            => await _sender.Send(new GetDepartmentQuery { DepartmentId = departmentId });

        [HttpGet]
        public async Task<IList<DepartmentVM>> GetDepartmentsFullInformationAsync()
            => await _sender.Send(new GetDepartmentsQuery());

        [HttpGet]
        public async Task<DepartmentStaffVM> GetDepartmentStaffAsync([FromHeader] Guid departmentId)
            => await _sender.Send(new GetDepartmentStaffQuery { DepartmentId = departmentId });

        [HttpGet]
        public async Task<DepartmentVM> GetDepartmentOfUserAsync([FromHeader] Guid userId)
            => await _sender.Send(new GetUserDepartmentQuery { UserId = userId });

        #endregion

        #region Update commands

        [HttpPut]
        public async Task UpdateDepartmentAsync([FromBody] UpdateDepartmentCommand command)
            => await _sender.Send(command);

        [HttpPatch]
        public async Task AddEmployeeInDepartmentAsync([FromBody] AddEmployeeInDepartmentCommand command)
            => await _sender.Send(command);

        [HttpPatch]
        public async Task ChangeDirectorInDepartmentAsync([FromBody] ChangeDirectorInDepartmentCommand command)
            => await _sender.Send(command);

        [HttpPatch]
        public async Task ChangeDepartmentBaseInformationAsync([FromBody] ChangeDepartmentMainInfoCommand command)
            => await _sender.Send(command);

        #endregion

        #region Delete commands

        [HttpDelete]
        public async Task DeleteDirectorFromDepartmentAsync([FromHeader] Guid departmentId)
            => await _sender.Send(new PurgeDirectorFromDepartmentCommand { DepartmentId = departmentId });

        [HttpDelete]
        public async Task DeleteEmployeeFromDepartmentAsync([FromBody] PurgeEmployeeFromDepartmentCommand command)
            => await _sender.Send(command);

        [HttpDelete]
        public async Task DeleteAllEmployeesFromDepartmentAsync([FromHeader] Guid departmentId)
            => await _sender.Send(new PurgeEmployeesFromCompanyCommand { DepartmentId = departmentId });

        [HttpDelete]
        public async Task DeleteDepartmentAsync([FromHeader] Guid departmentId)
            => await _sender.Send(new DeleteDepartmentCommand { DepartmentId = departmentId });

        #endregion
    }
}
