import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BagWithLetters, BagWithParcels } from '../models/shipment.model';
import { config } from '../app.config.server';
import { getApiUrl } from '../utils/config-util';

@Injectable({
  providedIn: 'root'
})
export class BagService {
  private apiUrl = getApiUrl(config);

  constructor(private http: HttpClient) {}

  getBagWithLetters(id: number): Observable<BagWithLetters> {
    return this.http.get<BagWithLetters>(`${this.apiUrl}/BagWithLetters/${id}`);
  }

  getBagWithParcels(id: number): Observable<BagWithParcels> {
    return this.http.get<BagWithParcels>(`${this.apiUrl}/BagWithParcels/${id}`);
  }
}
