// import { Component } from '@angular/core';
// import { Router } from '@angular/router';
// import { UserService } from '../../services/user.service';
// import { User, UserType } from '../../models/user.model';
 
// @Component({
//   selector: 'app-registration',
//   templateUrl: './registration.component.html',
//   styleUrls: ['./registration.component.css']
// })
// export class RegistrationComponent {
//   username: string = '';
//   firstName: string = '';
//   lastName: string = '';
//   contactNumber: string = '';
//   email: string = '';
//   password: string = '';
  
  
//   userType: UserType =UserType.Buyer;
 
//   constructor(private userService: UserService, private router: Router) {}
 
//   onRegisterSubmit(): void {
//     const newUser: User = {
//       userId: 0, // Temporary value, will be assigned by the backend
//       username: this.username,
//       firstName: this.firstName,
//       lastName: this.lastName,
//       contactNumber: this.contactNumber,
//       email: this.email,
//       password: this.password,
//       userType: this.userType
//     };
 
//     this.userService.register(newUser).subscribe({
//       next:(registeredUser: User) => {
//         console.log('Registration successful:', registeredUser);
//         // Redirect to login page after successful registration
//          this.router.navigate(['/login']);

//       },
//       error: (error: any) => {
//         console.error('Registration failed:', error); // Handle error appropriately
//       }
//     }
      
      
     
//     );
//   }
// }
  


import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { User, UserType } from '../../models/user.model';
import { ToastrService } from 'ngx-toastr';
 
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  username: string = '';
  firstName: string = '';
  lastName: string = '';
  contactNumber: string = '';
  email: string = '';
  password: string = '';
  userType: UserType = UserType.Buyer;
 
  constructor(private userService: UserService, private router: Router,private toastr: ToastrService) {}
 
  onRegisterSubmit(): void {
    // Check if userType is PropertyOwner, set it accordingly
    if (this.userType === UserType.PropertyOwner) {
      // Additional logic for PropertyOwner registration, if needed
    }
 
    const newUser: User = {
      userId: 0, // Temporary value, will be assigned by the backend
      username: this.username,
      firstName: this.firstName,
      lastName: this.lastName,
      contactNumber: this.contactNumber,
      email: this.email,
      password: this.password,
      userType: this.userType
    };
 
    this.userService.register(newUser).subscribe({
      next: (res ) => {
        // alert(res.message)
        this.toastr.success('Registration Successfull');
     //  console.log('Registration successful:', registeredUser);
        // Redirect to login page after successful registration
          
        this.router.navigate(['/login']);
      },
      error: (error: any) => {
         alert('Registration failed.Please try again.');
        console.error('Registration failed:', error); // Handle error appropriately
         
      }
    });
  }
}

 