/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CalculadoraService } from './calculadora.service';

describe('Service: Calculadora', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CalculadoraService]
    });
  });

  it('should ...', inject([CalculadoraService],
    (service: CalculadoraService) => {
    expect(service).toBeTruthy();
  }));

  it('deve garantir que 2 + 4 = 6' ,
    inject([CalculadoraService],(service: CalculadoraService)=>{
      let soma = service.calcular(2,4,CalculadoraService.SOMA);
      expect(soma).toEqual(6);
    }));

  it('deve garantir que 10 / 2 = 5' ,
    inject([CalculadoraService],(service: CalculadoraService)=>{
      let soma = service.calcular(10,2,CalculadoraService.DIVISAO);
      expect(soma).toEqual(5);
    }));

  it('deve garantir que 10 * 2 = 20' ,
    inject([CalculadoraService],(service: CalculadoraService)=>{
      let soma = service.calcular(10,2,CalculadoraService.MULTIPLICACAO);
      expect(soma).toEqual(20);
    }));

  it('deve garantir que 10 - 2 = 8' ,
    inject([CalculadoraService],(service: CalculadoraService)=>{
      let soma = service.calcular(10,2,CalculadoraService.SUBTRACAO);
      expect(soma).toEqual(8);
    }));

  it('deve garantir que operacao invalida retorne = 0' ,
    inject([CalculadoraService],(service: CalculadoraService)=>{
      let soma = service.calcular(10,2,'%');
      expect(soma).toEqual(0);
    }));
});
