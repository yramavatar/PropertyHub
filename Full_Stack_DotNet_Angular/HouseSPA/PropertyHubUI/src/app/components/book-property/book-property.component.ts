// import { Component, OnInit } from '@angular/core';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { ActivatedRoute, Router } from '@angular/router';
// import { UserService } from '../../services/user.service';
// import { User } from '../../models/user.model';
// import { BookingService } from '../../services/booking.service';

// @Component({
//   selector: 'app-book-property',
//   templateUrl: './book-property.component.html',
//   styleUrl: './book-property.component.css'
// })
// export class BookPropertyComponent implements OnInit{
//   propertyId: number | undefined;
//   bookPropertyForm!: FormGroup;
//   user!: User;
//   email: string = '';
//   ownerId: number = 0;
 
//   constructor(private userService:UserService,private route: ActivatedRoute, private fb: FormBuilder,private bookingService:BookingService,private router:Router) {
//     this.initForm();
//    }
 
 
//   getUserbyEmail(): void {
//     this.userService.getUserByEmail(this.email).subscribe((res: User) => {
//       // console.log(res)
//       this.user = res;
//       this.ownerId = this.user.userId;
//       // console.log(this.ownerId);
//       this.initForm();
//     })
//   }
//   ngOnInit(): void {
//     this.email = (this.userService.getUserEmail())
//     this.getUserbyEmail()
//     this.propertyId = history.state.propertyId;
    
//     // console.log(this.ownerId)
//   }
 
//   initForm(): void {
//     // console.log(this.ownerId)
    
//     this.bookPropertyForm = this.fb.group({
//       propertyId: [this.propertyId],
//       buyerId: [this.ownerId],
//       status: [0, Validators.required],
//       bookingDate: [new Date(), Validators.required]
//     });
//   }
 
//   onSubmit(): void {
//     if (this.bookPropertyForm.valid) {
//       const formData = this.bookPropertyForm.value;
//       // Submit the form data to your backend service
//       formData.status = parseInt(formData.status)
//       console.log(formData)
//       this.bookingService.bookProperty(formData).subscribe(()=>
//       {
//         console.log(formData)
//         this.router.navigate(['/my-bookings']);
//         alert("Booking details saved successfully");
//       },
//       err=>alert("This Property is not available.."))
//       this.router.navigate(['/property-listings'])
//       // console.log('Form submitted:', formData);
//     } else {
//       // Handle form validation errors
//       console.error('Form is invalid. Please check the fields.');
//     }
//   }
// }



 
import { Component, OnInit } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ActivatedRoute, Router } from '@angular/router';

import { UserService } from '../../services/user.service';

import { User } from '../../models/user.model';

import { BookingService } from '../../services/booking.service';
import { ToastrService } from 'ngx-toastr';
 
@Component({

  selector: 'app-book-property',

  templateUrl: './book-property.component.html',

  styleUrls: ['./book-property.component.css']

})

export class BookPropertyComponent implements OnInit {

  propertyId: number | undefined;

  bookPropertyForm!: FormGroup;  // for the booking form

  user!: User;

  email: string = '';

  ownerId: number = 0;

  minBookingDate: string = ''; //initialize with current date & time
 
  constructor(private userService: UserService, private route: ActivatedRoute, private fb: FormBuilder, private bookingService: BookingService, private router: Router,private toastr: ToastrService) {

    this.initForm();

  }
 
  ngOnInit(): void {    // called after the angular is initilised

    this.email = (this.userService.getUserEmail());

    this.getUserbyEmail();   // rertiev and initialise the form

    this.propertyId = history.state.propertyId;  // set propertyid from the route state for specific property

  }
 
  getUserbyEmail(): void {

    this.userService.getUserByEmail(this.email).subscribe((res: User) => {

      this.user = res;

      this.ownerId = this.user.userId;

      this.initForm();   // initilise the booking form

    })

  }
 
  initForm(): void {

    this.bookPropertyForm = this.fb.group({   // formbuilder service fb

      propertyId: [this.propertyId],

      buyerId: [this.ownerId],

      status: [0, Validators.required],

      bookingDate: ['', Validators.required] // Initialize with current date and time

    });
 
    // Calculate the minimum booking date (today's date)

    const currentDate = new Date();

    const year = currentDate.getFullYear();

    const month = ('0' + (currentDate.getMonth() + 1)).slice(-2); // Add leading zero if needed

    const day = ('0' + currentDate.getDate()).slice(-2); // Add leading zero if needed

    const hours = ('0' + currentDate.getHours()).slice(-2); // Add leading zero if needed

    const minutes = ('0' + currentDate.getMinutes()).slice(-2); // Add leading zero if needed

    this.minBookingDate = `${year}-${month}-${day}T${hours}:${minutes}`;

  }
 
  getCurrentDateTime(): string {

    const currentDate = new Date();

    const year = currentDate.getFullYear();

    const month = ('0' + (currentDate.getMonth() + 1)).slice(-2); // Add leading zero if needed

    const day = ('0' + currentDate.getDate()).slice(-2); // Add leading zero if needed

    const hours = ('0' + currentDate.getHours()).slice(-2); // Add leading zero if needed

    const minutes = ('0' + currentDate.getMinutes()).slice(-2); // Add leading zero if needed

    return `${year}-${month}-${day}T${hours}:${minutes}`;

  }
 
  onSubmit(): void {

    if (this.bookPropertyForm.valid) {

      const formData = this.bookPropertyForm.value;

      formData.status = parseInt(formData.status)

      console.log(formData)

      this.bookingService.bookProperty(formData).subscribe(() => {

        console.log(formData)

        this.router.navigate(['/my-bookings']);
          this.toastr.success('Your booking details has been saved successfully.'); // use toaster for 
       // alert("Booking details Saved");

      },

        err => alert("This Property is not available.."))

      this.router.navigate(['/property-listings'])

    } else {

      console.error('Form is invalid. Please check the fields.');

    }

  }

}



 
  