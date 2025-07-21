import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { FounderServicesService } from '../shared/service-proxies/employee';
import { ApproveLeaveDto } from '../shared/service-proxies/employee/model/approveLeaveDto'


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
export class FounderService {
  constructor(private client: FounderServicesService) {}
  
changestatus(body: ApproveLeaveDto): Observable<any> {
  console.log("going to the proxy", body);
  return from(this.client.apiServicesAppFounderServicesApproveLeavePost(body));
}

approveManager(id: number): Observable<any> {
    console.log('Approving manager:', id);
    return from(this.client.apiServicesAppFounderServicesApproveManagerPost(id));
  }

  approveEmployee(id: number): Observable<any> {
    console.log('Approving manager:', id);
    return from(this.client.apiServicesAppFounderServicesApproveEmployeePost(id));
  }

}
  
