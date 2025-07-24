using System;
using Abp.Application.Services;
using EmployeeLeave.Services.Interfaces;
using Nito.Disposables;

namespace EmployeeLeave.Services.EntityServices;

public class MyTestService : ApplicationService, IMyTestService 
{
    public void DoWork()
    {
        Console.WriteLine("doing work ...........................................................");
    }

    // public void Dispose()
    // {
    //     Console.WriteLine("Disposing ..........................................................");
    // }
}
