import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PropertyService } from '../../services/property.service';
import { UserService } from '../../services/user.service';
import { BookingService } from '../../services/booking.service';
import { Property } from '../../models/property.model';

@Component({
  selector: 'app-property-listings',
  templateUrl: './property-listings.component.html',
  styleUrl: './property-listings.component.css'
})
export class PropertyListingsComponent {
  
  filteredProperties: Property[] = [];
  cities: string[] = [];
  selectedCity: string = '';

  bookingData: any = {
    propertyId: 0,
    buyerId: 0,
    status: 0,
    bookingDate: new Date()
  };

  constructor(private userService:UserService, private router: Router, private propertyService: PropertyService,private bookingService:BookingService ) {
    this.getPropertyList();
  }
  properties: any[] = [];
  getPropertyList(): void {
    this.propertyService.getAllProperties().subscribe(properties => {
      this.filteredProperties = properties;
      this.properties = properties;

      this.getCities();
      // console.log(this.properties)
    });
  }
  getCities(): void {
    this.cities = Array.from(new Set(this.properties.map(property => property.city)));
  }

  searchByCity(): void {
    // console.log(this.selectedCity,"djdjwe")
    if (this.selectedCity) {
      this.filteredProperties = this.properties.filter(property => property.city === this.selectedCity);
     // console.log(this.filteredProperties)
      
    } if(this.selectedCity==='') {
      console.log(this.filteredProperties)
      this.filteredProperties = this.properties; // Show all properties if no city selected
    }
  }

  bookProperty(property: Property): void {
    // Navigate to the book-property route and pass the propertyId
    this.router.navigate(['/book-property'], { state: { propertyId: property.propertyId } });
  }
  submitBooking() {
    this.bookingService.bookProperty(this.bookingData).subscribe(
      result => {
        console.log('Booking successful', result);
        // Add any additional handling here (e.g., show success message)
      },
      error => {
        console.error('Error occurred while booking property', error);
        // Add error handling here (e.g., show error message)
      }
    );
  }

  // Helper functions to format property data
  getPropertyType(type: number): string {
    return type === 0 ? 'Apartment' : type === 1 ? 'Commercial' : 'Villa';
  }
 
  getFlatType(type: number): string {
    return type === 0 ? '1 BHK' : type === 1 ? '2 BHK' : type === 2 ? '3 BHK' : '4 BHK';
  }
 
  getPropertyStatus(status: number): string {
    return status === 0 ? 'Available' : status === 1 ? 'Rented' : status === 2 ? 'Sold' : 'Booked';
  }
}
