import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { JogoDaVelhaComponent } from './components/jogo-da-velha.component';
import { JogoDaVelhaService } from './service/jogo-da-velha.service';

@NgModule({
  imports: [
    CommonModule,
    RouterModule
  ],
  declarations: [
    JogoDaVelhaComponent
  ],
  exports: [
    JogoDaVelhaComponent
  ],
  providers: [
    JogoDaVelhaService
  ]
})
export class JogoDaVelhaModule { }
