import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { LeaveTypeServicesService } from '../shared/service-proxies/employee';
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
export class LeaveTypeServices {
  constructor(private client: LeaveTypeServicesService) {}

  getAll(): Observable<any> {
    console.log("going to the proxy");
    return from(this.client.apiServicesAppLeaveTypeServicesGetAllGet());
  }

  addleave(body: any): Observable<any> {
    return from(this.client.apiServicesAppLeaveTypeServicesAddLeaveTypePost(body));
  }

}
