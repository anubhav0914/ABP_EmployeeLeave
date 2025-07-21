import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { EmployeeServicesService } from '../shared/service-proxies/employee/api/employeeServices.service';
import { EmployeeResponseDto } from '../shared/service-proxies/employee/model/employeeResponseDto';
import { EmployeeDto } from '../shared/service-proxies/employee/model/employeeDto';

interface WrappedResponse<T> {
  result: T;
  success: boolean;
  error: any;
  targetUrl?: string;
  unAuthorizedRequest?: boolean;
  __abp?: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  constructor(private client: EmployeeServicesService) {}

  getAll(): Observable<any> {
    console.log("going to the proxy");
    return from(this.client.apiServicesAppEmployeeServicesGetAllEmployeeGet());
  }

  getById(id: number): Observable<any> {
    return from(this.client.apiServicesAppEmployeeServicesGetByIdGet(id));
  }
  getEmployeeByUserId(id: number): Observable<any> {
    return from(this.client.apiServicesAppEmployeeServicesGetEmployeeByUserIdGet(id));
  }

  create(body: EmployeeDto): Observable<any> {
    return from(this.client.apiServicesAppEmployeeServicesRegisterEmployeePost(body));
  }

  update(body: EmployeeDto): Observable<any> {
    return from(this.client.apiServicesAppEmployeeServicesUpdatePut(body));
  }

  delete(id: number): Observable<any> {
    return from(this.client.apiServicesAppEmployeeServicesDeleteEmployeeDelete(id));
  }
  getApprovedEmployees(): Observable<any> {
    return from(this.client.apiServicesAppEmployeeServicesGetAllEmployeeApprovedGet());
  }
   getRequestedEmployees(): Observable<any> {
    return from(this.client.apiServicesAppEmployeeServicesGetAllEmployeeRequestedGet());
  }
  
}
