using Application.Common.Models.JobModels;
using Application.Jobs.Commands;
using Application.Jobs.Commands.PatchCommands;
using Application.Jobs.Commands.UpdateCommands;
using Application.Jobs.Queries.JobAnswersQueries;
using Application.Jobs.Queries.JobMetadataQueries;
using Application.Jobs.Queries.JobQueries;
using Application.Jobs.Queries.JobQueries.DepartmentJobsQueries;
using Application.Jobs.Queries.JobQueries.UsersJobsQueries;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWorkspaceService.Controllers
{
    [ApiController]
    public class JobController(ISender sender) : BaseController(sender)
    {
        [Authorize(Policy = Polices.MustBeDirectorOrAdministrator)]
        [HttpPost]
        public async Task<Guid> CreateJobAsync([FromBody] CreateJobCommand command)
            => await _sender.Send(command);

        #region Get queries

        [HttpGet]
        public async Task<IList<JobMainInfoVM>> GetAllJobsAsync()
            => await _sender.Send(new GetAllJobsInfoQuery());

        [HttpGet]
        public async Task<IList<JobMainInfoVM>> GetFreeJobsInDepartmentAsync([FromHeader] Guid departmentId)
            => await _sender.Send(new GetUnfinishedJobsInfoInDepartmentQuery { DepartmentId = departmentId });

        [HttpGet]
        public async Task<IList<JobMainInfoVM>> GetJobsInDepartmentAsync([FromHeader] Guid departmentId)
            => await _sender.Send(new GetAllJobsInDepartmentQuery { DepartmentId = departmentId });

        [HttpGet]
        public async Task<JobMainInfoVM> GetJobAsync([FromHeader] Guid jobId)
            => await _sender.Send(new JetJobInfoQuery { JobId = jobId });

        [HttpGet]
        public async Task<IList<JobAnswerVM>> GetJobAnswersAsync([FromHeader] Guid jobId)
            => await _sender.Send(new GetJobAnswersQuery { JobId = jobId });

        [HttpGet]
        public async Task<JobMetadataVM> GetJobMetadataAsync([FromHeader] Guid jobId)
            => await _sender.Send(new GetJobMetadataQuery { JobId = jobId });

        [HttpGet]
        public async Task<IList<JobMainInfoVM>> GetEmployeeUnfinishedJobsAsync([FromHeader] Guid employeeId)
            => await _sender.Send(new GetEmployeeUnfinishedJobsInfoQuery { EmployeeId = employeeId });

        [HttpGet]
        public async Task<IList<JobMainInfoVM>> GetUserJobsAsync([FromHeader] Guid userId)
            => await _sender.Send(new GetUserJobsInfoQuery { UserId = userId });

        #endregion

        #region Update commands

        [HttpPatch]
        public async Task AddAnswerToJobAsync([FromBody] AddAnswerToJobCommand command)
            => await _sender.Send(command);

        [HttpPatch]
        public async Task AddCommentToJobAsync([FromBody] AddCommentToJobCommand command)
            => await _sender.Send(command);

        [HttpPatch]
        public async Task FinishJobAsync([FromHeader] Guid jobId)
            => await _sender.Send(new FinishJobCommand { JobId = jobId });

        [HttpPatch]
        public async Task AddEmployeeToJob([FromBody] AddEmployeeToJobCommand command)
            => await _sender.Send(command);

        #endregion

        [HttpDelete]
        public async Task DeleteJobAsync([FromHeader] Guid jobId)
            => await _sender.Send(new DeleteJobCommand { JobId = jobId });
    }
}
