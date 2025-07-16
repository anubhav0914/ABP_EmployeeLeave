// src/shared/service-proxies/employee-client-provider.ts
import { inject } from '@angular/core';
import { Client } from './employee-service-proxy';
import { HttpClient } from '@angular/common/http';

export function employeeClientFactory(): Client {
  const angularHttp = inject(HttpClient);

  const fetchCompatible = {
    fetch: (url: RequestInfo, init?: RequestInit): Promise<Response> => {
      return fetch(url.toString(), init); // still using fetch here
    },
  };

  return new Client('http://localhost:5000', fetchCompatible);
}
