using System;
using Abp.Application.Services;
using Nito.Disposables;

namespace EmployeeLeave.Services.Interfaces;

public interface IMyTestService  : IApplicationService
{
    public void DoWork();
    // public void Dispose();
}
