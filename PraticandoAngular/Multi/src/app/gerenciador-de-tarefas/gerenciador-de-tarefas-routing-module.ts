import { Routes } from "@angular/router";
import { GerenciadorDeTarefasComponent } from "./components/gerenciador-de-tarefas.component";
import { TarefasRoutes } from "./tarefas/tarefas-routing.module";

export const GerenciadorRoutes: Routes = [
  { path: 'gerenciador', component: GerenciadorDeTarefasComponent},
  ...TarefasRoutes
];
