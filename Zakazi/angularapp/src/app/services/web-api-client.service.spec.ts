import { TestBed } from '@angular/core/testing';

import { WebApiClient } from './web-api-client.service';

describe('WebApiClientService', () => {
  let service: WebApiClient;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WebApiClient);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
