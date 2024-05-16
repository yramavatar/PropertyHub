import { Component } from '@angular/core';
import { PropertyService } from '../../services/property.service';
import { BookingService } from '../../services/booking.service';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { Booking } from '../../models/booking.model';
import { Property } from '../../models/property.model';
import { ToastrService } from 'ngx-toastr';
 
@Component({

  selector: 'app-my-bookings',

  templateUrl: './my-bookings.component.html',

  styleUrls: ['./my-bookings.component.css']

})

export class MyBookingsComponent {

  user!: User;
  email: string = '';
  ownerId: number = 0;
  contactNumber :string ='';
  properties:Property[]=[];
 
  constructor(private userService:UserService, private propertyService: PropertyService,private bookingService:BookingService,private toastr:ToastrService ) {
    
    this.email = (this.userService.getUserEmail())
    this.getUserbyEmail()
  }

  getUserbyEmail(): void {
    this.userService.getUserByEmail(this.email).subscribe((res: User) => {
      // console.log(res)
      this.user = res;
      this.ownerId = this.user.userId;
      console.log(this.ownerId);
      this.getBookingList();
    })
  }

  bookings: any[] = [];
  getBookingList(): void {
    this.bookingService.getBookingsByBuyerId(this.ownerId).subscribe(bookings => {
      this.bookings = bookings;
      this.getPropertiesForBookings();
    });
  }
 
  

  // getPropertiesForBookings(): void {
  //   this.bookings.forEach(booking => {
  //     this.propertyService.getPropertyById(booking.propertyId).subscribe(property => {
        
  //       console.log(property);
  //   this.properties.push(property);
       
  //       // Assign property details to each booking
  //     });
  //   });
  // }

  getPropertiesForBookings(): void {
    this.bookings.forEach(booking => {
      
      this.propertyService.getPropertyById(booking.propertyId).subscribe(property => {
        if(property.ownerId !== undefined) {
        this.userService.getUserById(property.ownerId).subscribe((owner: User) => {
          property.owner = owner;
          property.showOwnerInfo = false; // Initialize flag for owner info visibility
          property.bookingDate=booking.bookingDate; // this can be deleted if it is not working
          this.properties.push(property);
        });
      }
      });
    });
  }

  // deleteProperty(propertyId: number): void {
  //   if(confirm("You want to delete this property??")){
  //   this.propertyService.deleteProperty(propertyId).subscribe(() => {
  //     // Remove the deleted property from the properties array
  //     alert("Item has been delted successfully")
  //     this.properties = this.properties.filter(property => property.propertyId !== propertyId);
      
  //   });
  // }

  // }

   
  
   
   
  deleteProperty(propertyId: number): void {
  
    if (confirm("Are you sure you want to delete this property?")) {
  
      this.propertyService.deleteProperty(propertyId).subscribe(() => { // it subscribe to the obsrbl rtrnd by the service call to handle asynchrnousg
  
        // Remove the deleted property from the properties array
  
        this.properties = this.properties.filter(property => property.propertyId !== propertyId);
  
        this.toastr.success('Property deleted successfully', 'Success', {
  
          positionClass: 'toast-top-center',
  
          timeOut: 2000,
  
          closeButton: true,
  
        });
  
      }, error => {
  
        // Handle error if deletion fails
  
        this.toastr.error('Failed to delete property', 'Error', {
  
          positionClass: 'toast-top-center',
  
          timeOut: 2000,
  
          closeButton: true,
  
        });
  
      });
  
    } else {
  
      this.toastr.info('Property deletion canceled', 'Info', {
  
        positionClass: 'toast-top-center',
  
        timeOut: 2000,
  
        closeButton: true,
  
      });
  
    }
  
  }
  
  // Method to toggle visibility of owner's contact number
  showOwnerInfo(property: any): void {
    property.showOwnerInfo = !property.showOwnerInfo;  // for specific property owner
  }

}
