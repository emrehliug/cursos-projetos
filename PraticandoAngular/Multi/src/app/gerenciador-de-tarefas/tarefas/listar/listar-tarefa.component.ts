import { Component, OnInit } from '@angular/core';
import { TarefaService , Tarefa } from '../shared';
import { Router } from '@angular/router';

@Component({
  selector: 'app-listar-tarefa',
  templateUrl: './listar-tarefa.component.html',
  styleUrls: ['./listar-tarefa.component.css']
})
export class ListarTarefaComponent implements OnInit {

  tarefas!: Tarefa[];

  constructor(private tarefaService: TarefaService,
    private router: Router
    ) { }

  ngOnInit() {
    this.tarefas = this.listarTodos();
  }

  listarTodos(): Tarefa[]{
    return this.tarefaService.listarTodos();
  }

  remover($event: any, tarefa: Tarefa): void{
    $event.preventDefault();
    if(confirm('Deseja remover a tarefa "'+ tarefa.nome +'"?')){
      this.tarefaService.remover(tarefa.id as number);
      this.tarefas = this.listarTodos();
    }
  }
  
  alterarStatus(tarefa: Tarefa): void{
      this.tarefaService.alterarStatus(tarefa.id as number);
      this.tarefas = this.listarTodos();
  }
}
