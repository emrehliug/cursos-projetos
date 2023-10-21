import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavComponent } from './components/nav.component';
import { NavService } from './services/nav.service';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    IonicModule.forRoot(),
    RouterModule
  ],
  declarations: [
    NavComponent
  ],
  providers:[
    NavService
  ],
  exports:[
    NavComponent
  ]
})
export class NavModule { }
