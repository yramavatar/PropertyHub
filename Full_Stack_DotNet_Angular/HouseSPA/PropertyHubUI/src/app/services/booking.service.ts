// import { Injectable } from '@angular/core';

// @Injectable({
//   providedIn: 'root'
// })
// export class BookingService {

//   constructor() { }
// }

// This is created by me 




import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Booking } from '../models/booking.model';
 
@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private baseUrl = 'https://localhost:7131/api/bookings';
 
  constructor(private http: HttpClient) { }
 
  bookProperty(booking: Booking): Observable<Booking> {
    return this.http.post<Booking>(`${this.baseUrl}/book`, booking);
  }
 
  cancelBooking(bookingId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/cancel/${bookingId}`);
  }
 
  getBookingById(bookingId: number): Observable<Booking> {
    return this.http.get<Booking>(`${this.baseUrl}/${bookingId}`);
  }
 
  getBookingsByBuyerId(buyerId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/byBuyer/${buyerId}`);
  }
 
  getBookingsByPropertyId(propertyId: number): Observable<Booking[]> {
    return this.http.get<Booking[]>(`${this.baseUrl}/byProperty/${propertyId}`);
  }
 
  getAllBookings(): Observable<Booking[]> {
    return this.http.get<Booking[]>(`${this.baseUrl}/allBooking`);
  }
}