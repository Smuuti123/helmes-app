import { Component, OnInit } from '@angular/core';
import { ShipmentService } from '../../services/shipment.service';
import { Shipment, BagWithLetters, BagWithParcels, Bag } from '../../models/shipment.model';

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
      this.shipments = data;
    }, (error) => {
    });
  }

  isBagWithLetters(bag: Bag): bag is BagWithLetters {
    return 'countOfLetters' in bag;
  }

  isBagWithParcels(bag: Bag): bag is BagWithParcels {
    return 'listOfParcels' in bag;
  }

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return date.toISOString().split('T')[0];
  }
}
