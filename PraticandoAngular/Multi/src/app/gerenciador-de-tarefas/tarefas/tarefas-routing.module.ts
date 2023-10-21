import { Routes } from "@angular/router";

import { ListarTarefaComponent } from "./listar/listar-tarefa.component";
import { CadastrarTarefaComponent } from "./cadastrar/cadastrar-tarefa.component";
import { EditarTarefasComponent } from "./editar/editar-tarefas.component";

export const TarefasRoutes: Routes = [

  { path: 'tarefas/listar', component: ListarTarefaComponent},
  { path: 'tarefas/cadastrar', component: CadastrarTarefaComponent },
  { path: 'tarefas/editar/:id' , component: EditarTarefasComponent}
];

