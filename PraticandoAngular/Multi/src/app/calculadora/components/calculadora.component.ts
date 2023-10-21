import { Component, OnInit } from '@angular/core';
import { CalculadoraService } from '../services/calculadora.service';

@Component({
  selector: 'app-calculadora',
  templateUrl: './calculadora.component.html',
  styleUrls: ['./calculadora.component.css']
})
export class CalculadoraComponent implements OnInit {

  private numero1: string | null = '';
  private numero2: string | null = '';
  private resultado: number | null = 0;
  private operacao: string | null = '';

  constructor(private calculadoraService: CalculadoraService) {

  }

  ngOnInit() {
    this.limpar();
  }

  limpar(): void{
    this.numero1 = '0';
    this.numero2 = null;
    this.resultado = null;
    this.operacao = null;
  }

  adicionarNumero(numero: string): void{
    if(this.operacao === null){
      this.numero1 = this.concatenarNumero(this.numero1,numero)
    }else{
      this.numero2 = this.concatenarNumero(this.numero2, numero)
    }
  }

  /**Retorna o valor concatenado. Trata o serapador decimal
   * @param string numAtual
   * @param string numConcat
   * @return string
   */
  concatenarNumero(numAtual: string | null, numConcat: string): string{
    if(numAtual === '0' || numAtual === null){
      numAtual = '';
    }
    if(numConcat === '.' && numAtual === ''){
      return '0.';
    }
    if(numConcat === '.' && numAtual.indexOf('.') > -1){
      return numAtual;
    }
    return numAtual + numConcat
  }

  definirOperacao(operacao: string | null):void {
    //Apenas define a operação caso não exista uma
    if(this.operacao === null){
      this.operacao = operacao;
      return;
    }
    //Caso operação definida e número 2 selecionado, efetua o calculo da operação
    if(this.numero2 !== null){
      this.resultado = this.calculadoraService.calcular(
        parseFloat(this.numero1 as string),
        parseFloat(this.numero2),
        this.operacao
      );
      this.operacao = operacao;
      this.numero1 = this.resultado.toString();
      this.numero2 = null;
      this.resultado = null;
    }
  }

  //Calcula se houver operacao
  calcular(): void{
    if(this.numero2 === null){
      return;
    }
    this.resultado = this.calculadoraService.calcular(
      parseFloat(this.numero1 as string),
      parseFloat(this.numero2),
      this.operacao as string);
  }

  //Retorna o valor a ser exibido na tela da calculadora
  get display(): string{
    if(this.resultado !== null){
      return this.resultado.toString();
    }
    if(this.numero2 !== null){
      return this.numero2;
    }
    return this.numero1 as string;
  }
}
