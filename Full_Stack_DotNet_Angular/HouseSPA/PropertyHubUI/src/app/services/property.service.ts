import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Property } from '../models/property.model';
 
@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  private baseUrl = 'https://localhost:7131/api/Property';
 
  constructor(private http: HttpClient) { }
 
  addProperty(propertyData:Property): Observable<any> {
    console.log(propertyData)
    return this.http.post<Property>(`${this.baseUrl}`, propertyData);
  }
 
  getPropertyById(propertyId: number): Observable<Property> {
    return this.http.get<Property>(`${this.baseUrl}/${propertyId}`);
  }
 
  getAllProperties(): Observable<Property[]> {
    return this.http.get<Property[]>(`${this.baseUrl}`);
  }
 
  updateProperty(propertyData: any): Observable<any> {
    console.log(propertyData)
    return this.http.put<any>(`${this.baseUrl}`, propertyData);
  }
 
  getPropertyStatus(propertyId: number): Observable<string> {
    return this.http.get<string>(`${this.baseUrl}/${propertyId}/status`);
  }
 
  deleteProperty(propertyId: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/${propertyId}`);
  }
 
  searchPropertiesByCity(city: string): Observable<Property[]> {
    return this.http.get<Property[]>(`${this.baseUrl}/search/city/${city}`);
  }
}