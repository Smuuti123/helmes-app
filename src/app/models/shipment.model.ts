export interface Shipment {
    id: number;
    shipmentNumber: string;
    flightNumber: string;
    flightDate: string;
    status: string;
    bags: (BagWithLetters | BagWithParcels)[];
  }
  
  export interface Bag {
    id: number;
    bagNumber: string;
    type: string;
    price: number;
  }
  
  export interface BagWithLetters extends Bag {
    countOfLetters: number;
  }
  
  export interface BagWithParcels extends Bag {
    listOfParcels: Parcel[];
  }
  
  export interface Parcel {
    parcelNumber: string;
    recipientName: string;
    destinationCountry: string;
    weight: number;
    price: number;
  }
  