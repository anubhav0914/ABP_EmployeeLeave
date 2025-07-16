// src/app/factories/client-factory.ts
import { Client } from '../../shared/service-proxies/employee-service-proxy';

export function clientFactory(): Client {
  const baseUrl = 'http://localhost:5000'; // Change this to your API URL if different
  console.log("Creating Client instance...");

  const fetcher = {
    fetch: (url: RequestInfo, init?: RequestInit): Promise<Response> => fetch(url, init)
  };

  return new Client(baseUrl, fetcher);
}
