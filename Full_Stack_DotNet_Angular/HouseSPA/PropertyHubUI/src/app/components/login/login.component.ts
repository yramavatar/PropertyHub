 
import { Component,inject } from '@angular/core';

import { Router } from '@angular/router';

import { AuthenticationService } from '../../services/authentication.service';

import { UserType } from '../../models/user.model';
import { NgToastService } from 'ng-angular-popup';
import { ToastrService } from 'ngx-toastr';
 
@Component({

  selector: 'app-login',

  templateUrl: './login.component.html',

  styleUrls: ['./login.component.css']

})

export class LoginComponent {

  email: string = '';

  password: string = '';

  userType: UserType;
  // toast = inject(NgToastService);
 
  constructor(private authService: AuthenticationService, private router: Router, private toastr: ToastrService) {

    this.userType = UserType.Buyer; // Initialize userType in the constructor

  }

  
 
  onLoginSubmit(): void {

    this.authService.login(this.email, this.password, this.userType).subscribe({

      next: (response: any) => {
        console.log(response)
      
        // Handle successful login response here
        console.log(this.userType)

        console.log('Login successful:', response);
        

        // this.authService.setIsLoggedIn(false);
           

        // Redirect based on user type
        if(response !==null) {
         // alert("Login Successfull")
          this.toastr.success('Login Successfull');
         // this can be used:  this.router.navigate(['/home']);
        if (this.userType === UserType.PropertyOwner) {

          this.router.navigate(['/home']);

        } else {

          this.router.navigate(['/home']);
          
        }
        }
          else {
            this.toastr.error('Invalid email or password');
           // alert("Login failed please check your credentials and try again");
         this.router.navigate(['/login']);
          }
      },

      error: (error) => {

        // Handle login error here

        error('Login failed:',error);
         alert("Login failed please check your credentials and try again");
         this.router.navigate(['/login']);
           
          

      }

    });

  }

}

