export interface User {
    userId: number;
    username: string;
    email: string;
    password: string;
    firstName: string;
    lastName: string;
    contactNumber: string;
    userType: UserType;
  }
   
  export enum UserType {
    Buyer = 0,
    PropertyOwner = 1,
    extra =2
  }