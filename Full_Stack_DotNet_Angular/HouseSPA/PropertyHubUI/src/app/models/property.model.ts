export interface Property {
  showOwnerInfo ?: boolean;  // added
  bookingDate?: Date; // newly added to show bookingDate
    propertyId?:number;
    ownerId?: number;  // marked nullable
     owner?: any; // You can replace 'any' with the appropriate type for 'Owner' if available and added
    propertyType: PropertyType;
    flatType: FlatType;
    sizeSqFt: number;
    description: string;
    price: number;
    location: string;
    city: string;
    propertyStatus: PropertyStatus;
    imageUrl: string;
    // bookings: any[]; // You can replace 'any' with the appropriate type for 'Booking' if available
    // feedbacks: any[]; // You can replace 'any' with the appropriate type for 'Feedback' if available
  }
   
  export enum PropertyType {
    Apartment = 0,
    Commercial = 1,
    Villa = 2
  }
   
  export enum FlatType {
    _1BHK = 0,
    _2BHK = 1,
    _3BHK = 2,
    _4BHK = 3
  }
   
  export enum PropertyStatus {
    Available = 0,
    Rented = 1,
    Sold = 2,
    Booked = 3
  }
