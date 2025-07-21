import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { EmployeeServicesService } from '../shared/service-proxies/employee/api/employeeServices.service';
import { LeaveRequestServicesService } from '../shared/service-proxies/employee';
import { LeaveRequestCreateUpdateDto } from '../shared/service-proxies/employee/model/leaveRequestCreateUpdateDto';
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
export class LeaveRequestServices {
  constructor(private client: LeaveRequestServicesService) {}

  getAll(): Observable<any> {
    console.log("going to the proxy");
    return from(this.client.apiServicesAppLeaveRequestServicesGetAllGet());
  }

  getById(id: number): Observable<any> {
    return from(this.client.apiServicesAppLeaveRequestServicesGetByIdGet(id));
  }

  create(body: LeaveRequestCreateUpdateDto): Observable<any> {
    return from(this.client.apiServicesAppLeaveRequestServicesAddLeaveRequestPost(body));
  }

  update(body: LeaveRequestCreateUpdateDto): Observable<any> {
    return from(this.client.apiServicesAppLeaveRequestServicesUpdatePut(body));
  }

  delete(id: number): Observable<any> {
    return from(this.client.apiServicesAppLeaveRequestServicesDeleteDelete(id));
  }
}
