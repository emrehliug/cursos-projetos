import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { TooltipModule } from 'ngx-bootstrap/tooltip';

import { AppComponent } from './app.component';
import { NavModule } from './shared/nav/nav.module';
import { CalculadoraModule } from './calculadora';
import { GerenciadorDeTarefasModule } from './gerenciador-de-tarefas/gerenciador-de-tarefas.module';

@NgModule({
  declarations: [
    AppComponent
     ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    TooltipModule,
    CalculadoraModule,
    NavModule,
    GerenciadorDeTarefasModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
