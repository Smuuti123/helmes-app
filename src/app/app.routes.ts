import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShipmentsListComponent } from './shipments-list/shipments-list.component';
import { ShipmentDetailsComponent } from './shipment-details/shipment-details.component';
import { BagDetailsComponent } from './bag-details/bag-details.component';



const routes: Routes = [{path: '', component: ShipmentsListComponent}, {path: 'shipment/:id', component: ShipmentDetailsComponent}, {path: 'bag/:id', component: BagDetailsComponent}];


@NgModule({imports: [RouterModule.forRoot(routes)], exports: [RouterModule]})

export class AppRoutingModule {}