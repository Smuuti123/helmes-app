import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ShipmentListComponent } from './components/shipment-list/shipment-list.component';
import { ShipmentDetailComponent } from './components/shipment-detail/shipment-detail.component';
import { AppRoutingModule } from './app.routes';

@NgModule({
  declarations: [
    ShipmentListComponent,
    ShipmentDetailComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AppComponent,  // Import standalone component here
    RouterModule.forRoot([])  // Ensure this is correct
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
