import { Component } from '@angular/core';
import { Property } from '../../models/property.model';
import { User, UserType } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';
import { PropertyService } from '../../services/property.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-my-properties',
  templateUrl: './my-properties.component.html',
  styleUrl: './my-properties.component.css'
})
export class MyPropertiesComponent {
  userType: UserType = 0;
  user!: User;
  email: string = '';
  ownerId: number = 0;
  property:any | null | undefined   // to hold property data
  parsedProperty:any                // to hold property data

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
    // object to store of property being edited
  editProperty: Property = {
    propertyId:0,
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
 
  
  constructor(private userService: UserService, private authService: AuthenticationService, private router: Router, private propertyService: PropertyService,private toastr: ToastrService) {
    this.userType = (this.userService.getUserType())
    this.email = (this.userService.getUserEmail())
    this.getUserbyEmail();
    this.getProperty();
  }
  // to check an existing proeprty if  its being edited 
  getProperty(){
    this.property = (localStorage.getItem('Property'))
    this.parsedProperty = JSON.parse(this.property);
    console.log(this.property);
    console.log(this.parsedProperty);
    if(this.property!==null){
      this.editProperty.propertyId = this.parsedProperty.propertyId;
      this.newProperty.ownerId= this.parsedProperty.ownerId 
      this.newProperty.propertyType= this.parsedProperty.propertyType
      this.newProperty.flatType= this.parsedProperty.flatType
      this.newProperty.sizeSqFt= this.parsedProperty.sizeSqFt
      this.newProperty.description= this.parsedProperty.description
      this.newProperty.price= this.parsedProperty.price
      this.newProperty.location= this.parsedProperty.location
      this.newProperty.city= this.parsedProperty.city
      this.newProperty.propertyStatus= this.parsedProperty.propertyStatus
      this.newProperty.imageUrl= this.parsedProperty.imageUrl
    }
    localStorage.removeItem('Property');
  }

  

  ngOnInit(): void {
    
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
  
  properties: any[] = [];
  getPropertyList(): void {
    this.propertyService.getAllProperties().subscribe((properties: any[]) => {
      this.properties = properties;
      // console.log(this.properties)
    });
  }

  onAddPropertySubmit(): void {
    if(this.property===null){
    this.newProperty.ownerId = this.ownerId;
    this.newProperty.propertyType = +this.newProperty.propertyType;
    this.newProperty.flatType = +this.newProperty.flatType; 
    this.newProperty.propertyStatus = +this.newProperty.propertyStatus;
    console.log(this.newProperty)
    this.propertyService.addProperty(this.newProperty).subscribe(() => {
      this.getPropertyList();
      // Clear the form fields after successful addition
      this.newProperty = {
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
    });
    //alert("Property Added Successfully");
    this.toastr.success('Property Added successfully');
    if(confirm("You want to add more Property??")){
      this.router.navigate(['/my-properties'])
      this.newProperty = {
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
    }else{
      this.router.navigate(['/home']);
    }
    
  }
  else{
    this.newProperty.propertyType = +this.newProperty.propertyType;
    this.newProperty.flatType = +this.newProperty.flatType; 
    this.newProperty.propertyStatus = +this.newProperty.propertyStatus;
    this.editProperty= this.newProperty;
    this.editProperty.propertyId = JSON.parse(this.property).propertyId;
    console.log(this.editProperty)
    this.updateProperty(this.editProperty);
  }
  }

  updateProperty(editProperty: any): void {
    // Implement edit functionality
    console.log(editProperty)
    this.propertyService.updateProperty(editProperty).subscribe(() => {
      this.getPropertyList()
      // Clear the form fields after successful addition
      this.newProperty = {
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
      //alert("Property Updated successfully");
      this.toastr.success('Property Updated successfully');
      this.router.navigate(['/home']);
    })
  }
}
