using System;
using System.Threading.Tasks;
using Abp.Application.Services;

namespace EmployeeLeave.Services.Interfaces;

public interface IMyService : IApplicationService
{
    Task Execute();
}
