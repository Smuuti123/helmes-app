import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ShipmentsListComponent } from './shipments-list/shipments-list.component';
import { ShipmentDetailsComponent } from './shipment-details/shipment-details.component';
import { BagDetailsComponent } from './bag-details/bag-details.component';

@NgModule({
  declarations: [AppComponent, ShipmentsListComponent, ShipmentDetailsComponent, BagDetailsComponent],
  imports: [ BrowserModule, HttpClientModule, RouterModule],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule {}
