import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Evento } from '@app/models/Evento';
import { Lote } from '@app/models/Lote';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

@Injectable()
export class LoteService {

  baseURL = 'https://localhost:44357/api/lotes';
  constructor(private http: HttpClient) { }

  //Consulta
  public getLotesByEventoId(eventoId: number): Observable<Lote[]> {
    return this.http.get<Lote[]>(`${this.baseURL}/${eventoId}`)
    .pipe(take(1));
  }

  //SalvaLotes
  public salvaLotes(eventoId: number, lotes: Lote[]): Observable<Lote[]> {
    return this.http.put<Lote[]>(`${this.baseURL}/${eventoId}`, lotes)
    .pipe(take(1));
  }

  //Deleta
  public DeleteLote(eventoId: number, loteId: number): Observable<any> {
    return this.http.delete<any>(`${this.baseURL}/${eventoId}/${loteId}`)
    .pipe(take(1));
  }

}
