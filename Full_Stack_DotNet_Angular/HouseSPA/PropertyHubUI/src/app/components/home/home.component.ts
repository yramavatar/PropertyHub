import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { User, UserType } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { PropertyService } from '../../services/property.service';
import { Property } from '../../models/property.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  userType: UserType = 0;
  user!: User;
  email: string = '';
  ownerId: number = 0;

  constructor(private userService: UserService, private authService: AuthenticationService, private router: Router, private propertyService: PropertyService,private toastr:ToastrService) {
    this.userType = (this.userService.getUserType())
    this.email = (this.userService.getUserEmail())
    this.getUserbyEmail();

  }

  ngOnInit(): void {
    this.getPropertyList();
  }


  getUserbyEmail(): void {
    this.userService.getUserByEmail(this.email).subscribe((res: User) => {
      // console.log(res)
      this.user = res;
      this.ownerId = this.user.userId;
      // console.log(this.ownerId);
    })
  }
  // for property-owner 
  newProperty: Property = {
    ownerId: 0,
    propertyType: 0,
    flatType: 0,
    sizeSqFt: 0,
    description: '',
    price: 0,
    location: '',
    city: '',
    propertyStatus: 0,
    imageUrl: ''
  };
  properties: any[] = [];
  getPropertyList(): void {
    this.propertyService.getAllProperties().subscribe(properties => {
      this.properties = properties;
      // console.log(this.properties)
    });
  }

  editProperty(property: Property): void {
    // Implement edit functionality as per your requirement
    console.log('Edit property:', property);
    localStorage.setItem('Property',JSON.stringify(property));
    this.router.navigate(['/my-properties'])
  }

  deleteProperty(propertyId: number): void {
    if (confirm('Are you sure you want to delete this property?')) {
      this.propertyService.deleteProperty(propertyId).subscribe(
        () => {
          // Reload properties after successful deletion
          this.getPropertyList();
        //  alert('Property deleted successfully.');
        this.toastr.success('Property deleted successfully');
        },
        error => {
          console.error('Error deleting property:', error);
        }
      );
    }
  }
}
