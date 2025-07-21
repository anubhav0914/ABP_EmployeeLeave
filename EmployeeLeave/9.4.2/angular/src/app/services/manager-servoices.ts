import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import {  ManagerDto, ManagerServicesService } from '../shared/service-proxies/employee';


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
export class ManagerService {
  getMangerByUserId() {
    console.log("going to the proxy");
  return from(this.client.apiServicesAppManagerServicesGetMangerRoleRequestsGet());
  }
  constructor(private client: ManagerServicesService) {}
  
gellAll(): Observable<any> {
  console.log("going to the proxy");
  return from(this.client.apiServicesAppManagerServicesGetAllManagerGet());
}

getApproved(): Observable<any> {
  console.log("going to the proxy");
  return from(this.client.apiServicesAppManagerServicesGetMangerRoleApprovedGet());
}
getREquested(): Observable<any> {
  console.log("going to the proxy");
  return from(this.client.apiServicesAppManagerServicesGetMangerRoleRequestsGet());
}
register(body: ManagerDto): Observable<any> {
  console.log("going to the proxy");
  return from(this.client.apiServicesAppManagerServicesRegisterMangerPost(body));
}
}
  
