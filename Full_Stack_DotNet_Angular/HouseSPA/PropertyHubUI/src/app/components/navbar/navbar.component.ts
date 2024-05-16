import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';
import { User, UserType } from '../../models/user.model';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  userType:UserType=0;
  userFirstName:string='';
  email: string = '';
  firstName!:User;
  constructor(private userService:UserService, private authService:AuthenticationService,private router:Router){
    let user = (this.userService.getUserType())
    this.email = (this.userService.getUserEmail())
    this.getUserbyEmail();
    // console.log(user)
    this.userType=user;
  }
  
  // user:any = (this.userService.getUserType())
   
  logout(){
    this.authService.logout();
    this.userType=2
    this.router.navigate(['/login']);

  }

  getUserbyEmail(): void {
    this.userService.getUserByEmail(this.email).subscribe((res:User) => {
      //console.log(res)
     this.firstName=res; 
     this.userFirstName = this.firstName.firstName; // Extracting code 
    //  res = (this.firstName);
     //console.log(this.userFirstName)
       console.log(this.userFirstName);
      
       
    })
  }
}
