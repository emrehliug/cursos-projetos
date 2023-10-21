import { Injectable } from '@angular/core';
import { Renderer2 } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavService {

constructor() { }

carregaJsScript(renderer: Renderer2, src: string): HTMLScriptElement{

  const script = renderer.createElement('script');
  script.type = 'text/javascript';
  script.src = src;
  renderer.appendChild(document.body, script);

  return script;
}
}
