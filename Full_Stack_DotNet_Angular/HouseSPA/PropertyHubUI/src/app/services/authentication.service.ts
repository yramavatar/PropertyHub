
import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable, of, throwError } from 'rxjs';

import { catchError } from 'rxjs/operators';

import { UserType } from '../models/user.model';
import { Router } from '@angular/router';
 
@Injectable({

  providedIn: 'root'

})

export class AuthenticationService {

  // loginStatus:boolean = false;
  // isLoggedIn = new BehaviorSubject<boolean>(false)

  private baseUrl = 'https://localhost:7131/api/user'; // Replace with your backend API URL
 
  constructor(private http: HttpClient,private router:Router) { }   // http fetching data from api external
 
  login(email: string, password: string, userType: UserType): Observable<any> {

    console.log({email,password,userType})
    const loginUrl = `${this.baseUrl}/authenticate`;
        localStorage.setItem('currentUser', JSON.stringify({email,userType}));
    return this.http.post<any>(loginUrl, { email, password, userType }).pipe(

       

      catchError(error => {

        console.error('Login failed:', error);

        return of(null);

      })

    );

  }


  logout(){
    localStorage.removeItem('currentUser');
  }

  isLoggedIn():boolean{
    return !!localStorage.getItem('currentUser');
  }
}
