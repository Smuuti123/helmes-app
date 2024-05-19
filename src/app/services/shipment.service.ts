import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin } from 'rxjs';
import { map, mergeMap } from 'rxjs/operators';
import { Shipment, BagWithLetters, BagWithParcels } from '../models/shipment.model';
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
    return this.http.get<Shipment>(`${this.apiUrl}/${id}`).pipe(
      mergeMap(shipment => {
        const bagRequests = shipment.bags.map(bag =>
          bag.type === 'letters' 
            ? this.http.get<BagWithLetters>(`${getApiUrl(config)}/BagWithLetters/${bag.id}`)
            : this.http.get<BagWithParcels>(`${getApiUrl(config)}/BagWithParcels/${bag.id}`)
        );
        return forkJoin(bagRequests).pipe(
          map(bags => ({
            ...shipment,
            bags
          }))
        );
      })
    );
  }
}
