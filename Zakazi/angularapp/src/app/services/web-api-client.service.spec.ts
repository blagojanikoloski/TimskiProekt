import { TestBed } from '@angular/core/testing';

import { WebApiClientService } from './web-api-client.service';

describe('WebApiClientService', () => {
  let service: WebApiClientService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WebApiClientService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
