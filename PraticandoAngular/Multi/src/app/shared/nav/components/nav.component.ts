import { Component, OnInit } from '@angular/core';
import { Renderer2 } from '@angular/core';
import { NavService } from '../services/nav.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  readonly script_path = 'assets/scripts/nav.js';
  constructor(private navService: NavService,private renderer: Renderer2){ }

  ngOnInit() {
    const scriptElement = this.navService.carregaJsScript(this.renderer,this.script_path);
    scriptElement.onload = () => {
    }
  }

}

