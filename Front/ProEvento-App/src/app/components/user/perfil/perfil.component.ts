import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/ValidatorField';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  form: FormGroup;

  get f() {
    return this.form.controls;
  }

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.validation();
  }

  public validation(){

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('senha','confirmeSenha')
    };

    this.form = this.fb.group({
      titulo: ['', Validators.required],
      primeiroNome: ['', Validators.required],
      ultimoNome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      telefone: ['', [Validators.required, Validators.maxLength(11)]],
      funcao: ['', Validators.required],
      descricao: ['', Validators.required],
      senha: ['', [Validators.required, Validators.minLength(6)]],
      confirmeSenha: ['', Validators.required]
    }, formOptions)
  }

  public ResetForm(): void{
    this.form.reset();
  }
}
