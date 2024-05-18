import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Shipment, BagWithLetters, BagWithParcels } from '../../models/shipment.model';
import { ShipmentService } from '../../services/shipment.service';

@Component({
  selector: 'app-shipment-detail',
  templateUrl: './shipment-detail.component.html',
  styleUrls: ['./shipment-detail.component.scss']
})
export class ShipmentDetailComponent implements OnInit {
  shipment: Shipment | null = null;

  constructor(
    private route: ActivatedRoute,
    private shipmentService: ShipmentService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.shipmentService.getShipment(Number(id)).subscribe((data: Shipment) => {
        this.shipment = data;
      });
    }
  }

  isBagWithLetters(bag: any): bag is BagWithLetters {
    return 'countOfLetters' in bag;
  }

  isBagWithParcels(bag: any): bag is BagWithParcels {
    return 'listOfParcels' in bag;
  }
}
