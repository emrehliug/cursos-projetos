/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { JogoDaVelhaService } from './jogo-da-velha.service';

describe('Service: JogoDaVelha', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [JogoDaVelhaService]
    });
  });

  it('should ...', inject([JogoDaVelhaService], (service: JogoDaVelhaService) => {
    expect(service).toBeTruthy();
  }));
});
