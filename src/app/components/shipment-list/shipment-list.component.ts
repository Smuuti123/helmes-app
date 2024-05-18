import { Component, OnInit } from '@angular/core';
import { ShipmentService } from '../../services/shipment.service';
import { Shipment } from '../../models/shipment.model';

@Component({
  selector: 'app-shipment-list',
  templateUrl: './shipment-list.component.html',
  styleUrls: ['./shipment-list.component.scss']
})
export class ShipmentListComponent implements OnInit {
  shipments: Shipment[] = [];

  constructor(private shipmentService: ShipmentService) {}

  ngOnInit(): void {
    this.shipmentService.getShipments().subscribe((data: Shipment[]) => {
      console.log('Shipments fetched:', data);
      this.shipments = data;
    }, (error) => {
      console.error('Error fetching shipments:', error);
    });
  }
}
