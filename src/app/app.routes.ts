import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShipmentListComponent } from './components/shipment-list/shipment-list.component';
import { ShipmentDetailComponent } from './components/shipment-detail/shipment-detail.component';

const routes: Routes = [
  { path: 'shipments', component: ShipmentListComponent },
  { path: 'shipment/:id', component: ShipmentDetailComponent },
  { path: '', redirectTo: '/shipments', pathMatch: 'full' }  // Default route to shipments
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
