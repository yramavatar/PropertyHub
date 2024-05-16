import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PropertyListingsComponent } from './components/property-listings/property-listings.component';
import { MyPropertiesComponent } from './components/my-properties/my-properties.component';
import { MyBookingsComponent } from './components/my-bookings/my-bookings.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { BookPropertyComponent } from './components/book-property/book-property.component';
import { AuthGuard } from './services/auth.guard';
 
// routing configuration , with Routes interface
const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationComponent },
  {
    path: '', component: DashboardComponent,
    canActivate:[AuthGuard],    // added for authgurad
    children: [
      {path: '',redirectTo: 'home',pathMatch:'full'}, // added newly
      { path: '', component: HomeComponent },
      { path: 'home', component: HomeComponent },
      { path: 'property-listings', component: PropertyListingsComponent },
      { path: 'book-property', component: BookPropertyComponent},
      { path: 'my-properties', component: MyPropertiesComponent },
      { path: 'my-bookings', component: MyBookingsComponent },]
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]   // available for other module after exporting 
})
export class AppRoutingModule { }
