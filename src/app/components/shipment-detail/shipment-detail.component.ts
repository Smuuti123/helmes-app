import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Shipment, BagWithLetters, BagWithParcels, Bag } from '../../models/shipment.model';
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
        console.log('Loaded shipment:', this.shipment);
      });
    }
  }

  isBagWithLetters(bag: Bag): bag is BagWithLetters {
    return bag.type === 'letters';
  }

  isBagWithParcels(bag: Bag): bag is BagWithParcels {
    return bag.type === 'parcels';
  }

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return date.toISOString().split('T')[0];
  }

  getBagType(bag: Bag): string {
    if (this.isBagWithLetters(bag)) {
      return 'Bag with letters';
    } 
    return 'Bag with parcels';
  }

  getTotalAmount(bag: Bag): number {
    if (this.isBagWithLetters(bag)) {
      return bag.countOfLetters;
    } else this.isBagWithParcels(bag) 
      return bag.listOfParcels.length;
  }

  getTotalPrice(bag: Bag): number {
    if (this.isBagWithLetters(bag)) {
      return bag.price;
    } else(this.isBagWithParcels(bag))
      return bag.listOfParcels.reduce((total: any, parcel: { price: any; }) => total + parcel.price, 0);
  }
}
