import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";


import { CalculadoraComponent } from "./calculadora/components/calculadora.component";
import { GerenciadorRoutes } from "./gerenciador-de-tarefas/gerenciador-de-tarefas-routing-module";
import { JogoDaVelhaRoutes } from "./jogo-da-velha";

export const routes: Routes = [
  { path: 'calculadora', component: CalculadoraComponent },
  ...GerenciadorRoutes,
  ...JogoDaVelhaRoutes,
  { path: '', redirectTo: 'calculadora', pathMatch:'full' },
  { path: '**', redirectTo: 'calculadora', pathMatch:'full' }
];

@NgModule
({
  imports: [
     RouterModule.forRoot(routes)
    ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule {}
