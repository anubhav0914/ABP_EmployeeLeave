// src/shared/service-proxies/employee-service-proxy.module.ts
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { Client } from './employee-service-proxy';
import { employeeClientFactory } from './employee-client-provider';

@NgModule({
  imports: [HttpClientModule],
  providers: [
    {
      provide: Client,
      useFactory: employeeClientFactory,
    }
  ]
})
export class EmployeeServiceProxyModule {}
