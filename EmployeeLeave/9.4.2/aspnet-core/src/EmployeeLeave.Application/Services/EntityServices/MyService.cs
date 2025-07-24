using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using EmployeeLeave.Services.Interfaces;

namespace EmployeeLeave.Services.EntityServices;

public class MyService : ApplicationService , IMyService
{
    private readonly IMyTestService _myTestService;
    public MyService(IMyTestService myTestService)
    {
        _myTestService = myTestService;
    }

    public async Task Execute()
    {
        try
        {
            _myTestService.DoWork();
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.ToString());
        }

      }
   
}
