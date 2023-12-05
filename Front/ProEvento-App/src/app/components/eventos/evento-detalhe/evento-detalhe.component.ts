import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventoService } from '@app/service/evento.service';
import { Evento } from '@app/models/Evento';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Lote } from '@app/models/Lote';
import { LoteService } from '@app/service/lote.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { environment } from '@environments/environment';


@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
})
export class EventoDetalheComponent implements OnInit {
  
  modalRef: BsModalRef;
  eventoId: number;
  form: FormGroup;
  evento = {} as Evento;
  estadoSalvar = 'Create' as string;
  loteAtual = {id: 0, nome: '', indice: 0};
  imagemURL = 'assets/UploadImage.png';
  file: File;

  get modoEditar(): boolean {
    return this.estadoSalvar == 'Update'
  }

  get lotes(): FormArray{
    return this.form.get('lotes') as FormArray;
  }

  get f(){
    return this.form.controls;
  }

  get bsConfig(): any{
    return { 
      isAnimated: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass:'theme-default',
      showWeekNumbers: false
     }
  }

  get bsConfigLote(): any{
    return { 
      isAnimated: true,
      containerClass:'theme-default',
      showWeekNumbers: false
     }
  }

  constructor(private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activatedRouter: ActivatedRoute,
    private router: Router,
    private eventoService: EventoService,
    private loteService: LoteService,
    private modalService: BsModalService,
    private spinnerService: NgxSpinnerService,
    private toastrService: ToastrService){
      this.localeService.use('pt-br');
  }

  public carregaEvento(): void{
    this.eventoId = +this.activatedRouter.snapshot.paramMap.get('id')!;

    if(this.eventoId != null && this.eventoId != 0){
      this.spinnerService.show();
      
      this.estadoSalvar = 'Update';
      
      this.eventoService.getEventoById(this.eventoId).subscribe({
        next: (evento: Evento) => {
          this.evento = {...evento};
          this.form.patchValue(this.evento);
          if(this.evento.imagemURL != ''){
            this.imagemURL = environment.apiURL + 'Resources/Images/' + this.evento.imagemURL;     
          }
          this.carregarLotes();
        },
        error: (error: any) => {
          this.spinnerService.hide();
          this.toastrService.error("Erro ao tentar carregar o Evento.");
          console.error(error);
        }
      }).add(() => this.spinnerService.hide());
    };
  }
  
  public carregarLotes(): void {
    this.loteService.getLotesByEventoId(this.eventoId).subscribe(
      (lotesRetorno: Lote[]) => {
        lotesRetorno.forEach(lote => {
          this.lotes.push(this.criarLote(lote));
        });
      },
      (error: any) => {
        this.toastrService.error('Error ao tentar carregar lotes', 'Erro');
        console.error(error);
      },
      () => {}
    ).add(() => this.spinnerService.hide());
  }

  ngOnInit(): void {
    this.carregaEvento();
    this.validation();
  }



  public validation(): void{
    this.form = this.fb.group({
      local : ['', Validators.required],
      dataEvento : ['', Validators.required],
      tema : ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      qtdPessoas : ['', [Validators.required, Validators.maxLength(1200000)]],
      imagemURL : [''],
      telefone : ['', Validators.required],
      email : ['', [Validators.required, Validators.email]],
      lotes: this.fb.array([])
    });
  }

  adicionarLote(): void {
    this.lotes.push(this.criarLote({id: 0} as Lote));
  }

  criarLote(lote: Lote): FormGroup {
    return this.fb.group({
      id: [lote.id], 
      nome: [lote.nome, Validators.required], 
      preco: [lote.preco, Validators.required], 
      quantidade: [lote.quantidade, Validators.required], 
      dataInicio: [lote.dataInicio], 
      dataFim: [lote.dataFim]
    });
  }

  public ResetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: any): any{
    return {'is-invalid': campoForm.errors && campoForm.touched };
  }

  public salvarEvento() : void{
    if(this.form.valid){
      this.spinnerService.show();
      
      this.evento = (this.estadoSalvar == 'Create') ? {...this.form.value} : {id: this.evento.id, ...this.form.value}

      this.eventoService[this.estadoSalvar](this.evento).subscribe(
        (eventoRetorno: Evento) => {
          this.toastrService.success('Evento salvo com sucesso!', 'Sucesso');
          this.router.navigate([`eventos/detalhe/${eventoRetorno.id}`]);
        },
        (error: any) => {
          console.error(error),
          this.toastrService.error('Error ao tentar salvar Evento!')
        },
      ).add(() => this.spinnerService.hide());
    }
  }

  public salvarLotes(): void{
    this.spinnerService.show();
    
    if(this.form.controls.lotes.valid){
      this.loteService.salvaLotes(this.eventoId, this.form.value.lotes).subscribe(
        () => {
          this.toastrService.success('Lotes salvos com sucesso!', 'Sucesso!');
          this.lotes.reset();
        },
        (error: any) => {
          this.toastrService.error('Erro ao tentar salvar Lotes!', 'Erro');
          console.error(error);
        }
      ).add(() => this.spinnerService.hide())
    }
  }

  public removerLote(template: TemplateRef<any>,indice: number): void {

    this.loteAtual.id = this.lotes.get(indice + '.id')!.value;
    this.loteAtual.nome = this.lotes.get(indice + '.nome')!.value;
    this.loteAtual.indice = indice;

    this.modalRef = this.modalService.show(template, {class:'modal-sm'})
  }

  confirmDeleteLote(): void{
    this.modalRef.hide();
    this.spinnerService.show();

    this.loteService.DeleteLote(this.eventoId, this.loteAtual.id).subscribe(
      () => {
        this.toastrService.success('Lote deletado com sucesso!', 'Sucesso');
        this.lotes.removeAt(this.loteAtual.indice);
      },
      (error: any) => {
        this.toastrService.error(`error ao tentar deletar Lote: ${this.loteAtual.id}`);
      }
    ).add(() => this.spinnerService.hide());
  }

  declineDeleteLote(): void{
    this.modalRef.hide();
  }

  public mudarValorData(value: Date, indice:number, campo: string): void{
    this.lotes.value[indice][campo] = value;
  }

  public retornaNomeLote(nomeLote: string): string{
    return nomeLote == null || nomeLote == '' ? 'NOME DO LOTE' : nomeLote 
  }

  onFileChange(ev: any): void{
    const reader = new FileReader();

    reader.onload = (event: any) => this.imagemURL = event.target.result;

    this.file = ev.target.files;
    reader.readAsDataURL(this.file[0]);
    
    this.uploadImage();
  }

  uploadImage(): void{
    this.spinnerService.show();
    this.eventoService.postUpload(this.eventoId, this.file).subscribe(
      () => {
        this.carregaEvento();
        this.toastrService.success('Imagem atualizada com sucesso!!', 'Sucesso!');
      },
      (error: any) => {
        this.toastrService.error('Erro ao tentar atualizar imagem','Error');
        console.error(error);
      }
    ).add(() => this.spinnerService.hide());
  }
}
