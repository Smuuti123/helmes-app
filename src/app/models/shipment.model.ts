export interface Shipment {
  id: number;
  shipmentNumber: string;
  flightNumber: string;
  flightDate: string;
  status: number;
  bags: (BagWithLetters | BagWithParcels)[];
}

export interface Bag {
  listOfParcels: any;
  id: number;
  bagNumber: string;
  type: 'letters' | 'parcels';
}

export interface BagWithLetters extends Bag {
  countOfLetters: number;
  price: number;
}

export interface BagWithParcels extends Bag {
  listOfParcels: Parcel[];
  price: number;
}

export interface Parcel {
  parcelNumber: string;
  recipientName: string;
  destinationCountry: string;
  weight: number;
  price: number;
}
