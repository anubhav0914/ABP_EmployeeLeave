import { Injectable } from '@angular/core';
import { Client,EmployeeDto,EmployeeResponseDtoListApiResponse,EmployeeResponseDtoApiResponse } from '../../shared/service-proxies/employee-service-proxy';

import { from, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  constructor(private client: Client) {}

  getAll(): Observable<EmployeeResponseDtoListApiResponse> {
    return from(this.client.getAllEmployee());
  }

  getById(id: number): Observable<EmployeeResponseDtoApiResponse> {
    return from(this.client.getEmployeeByUserId(id));
  }

  create(body: EmployeeDto): Observable<EmployeeResponseDtoApiResponse> {
    return from(this.client.registerEmployee(body));
  }

  update(body: EmployeeDto): Observable<EmployeeResponseDtoApiResponse> {
    return from(this.client.update(body));
  }

  delete(id: number): Observable<EmployeeResponseDtoApiResponse> {
    return from(this.client.deleteEmployee(id));
  }
}
