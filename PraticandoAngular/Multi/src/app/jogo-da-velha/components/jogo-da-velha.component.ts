import { Component, OnInit } from '@angular/core';
import { JogoDaVelhaService } from '../service/jogo-da-velha.service';

@Component({
  selector: 'app-jogo-da-velha',
  templateUrl: './jogo-da-velha.component.html',
  styleUrls: ['./jogo-da-velha.component.css']
})
export class JogoDaVelhaComponent implements OnInit {

  constructor(private jogoDaVelhaService: JogoDaVelhaService) { }

  ngOnInit() {
    this.jogoDaVelhaService.inicializar();
  }

  get showInicio(): boolean {
    return this.jogoDaVelhaService.showInicio;
  }
  get showFinal(): boolean {
    return this.jogoDaVelhaService.showFinal;
  }
  get showTabuleiro(): boolean {
    return this.jogoDaVelhaService.showTabuleiro;
  }

  //exibi tabuleiro em branco
  iniciarJogo(): void {
    this.jogoDaVelhaService.iniciarJogo();
  }
  //inicia o jogo pegando a posição do click
  jogar(posX: number, posY: number): void {
    this.jogoDaVelhaService.jogar(posX, posY);
  }
  //exibi a img X se retornado true
  exibirX(posX: number, posY: number): boolean {
    return this.jogoDaVelhaService.exibirX(posX, posY);
  }
  //exibi a img O se retornado true
  exibirO(posX: number, posY: number): boolean {
    return this.jogoDaVelhaService.exibirO(posX, posY);
  }

  exibirVitoria(posX: number, posY: number): boolean {
    return this.jogoDaVelhaService.exibirVitoria(posX, posY);
  }

  get jogador(): number {
    return this.jogoDaVelhaService.jogador;
  }

  novoJogo(): void {
    this.jogoDaVelhaService.novoJogo();
  }
}
