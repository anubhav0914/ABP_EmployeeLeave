using System;

namespace EmployeeLeave.Utils;

public class ApiResponse<T>
{
    public bool status { get; set; }
    public int statusCode { get; set; }

    public string message { get; set; }
    
    public T data { get; set; }
}
