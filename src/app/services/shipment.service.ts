import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Shipment } from '../models/shipment.model';
import { config } from '../app.config.server';
import { getApiUrl } from '../utils/config-util';

@Injectable({
  providedIn: 'root'
})
export class ShipmentService {
  private apiUrl = `${getApiUrl(config)}/Shipments`;

  constructor(private http: HttpClient) {}

  getShipments(): Observable<Shipment[]> {
    return this.http.get<Shipment[]>(this.apiUrl);
  }

  getShipment(id: number): Observable<Shipment> {
    return this.http.get<Shipment>(`${this.apiUrl}/${id}`);
  }
}
