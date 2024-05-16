
import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable, of } from 'rxjs';

import { catchError } from 'rxjs/operators';

import { User, UserType } from '../models/user.model';

@Injectable({

  providedIn: 'root'

})

export class UserService {

  private baseUrl = 'https://localhost:7131/api/user'; // Replace with your backend API URL

  constructor(private http: HttpClient) { }

  register(userData: User): Observable<any> {
    console.log("user data in service", userData)

    const registerUrl = `${this.baseUrl}/register`;

    return this.http.post<User>(registerUrl, userData)

      .pipe(

        catchError(error => {

          console.error('Error registering user:', error);

          return of(null);

        })

      );

  }
  user:any={
    userType:2,
    email:''
  }

  getUserType():any{
     this.user= (localStorage.getItem('currentUser'))
    return JSON.parse(this.user).userType
  }
  getUserEmail():any{
    this.user = (localStorage.getItem('currentUser'))
    return JSON.parse(this.user).email
  }
  getUserByEmail(email: string): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/email/${email}`);
  }
   
    // newly added can be deleted
     getUserById(userId:number):Observable<any>{
      return this.http.get<any>(`${this.baseUrl}/${userId}`);
      
     }

     //till this

}
