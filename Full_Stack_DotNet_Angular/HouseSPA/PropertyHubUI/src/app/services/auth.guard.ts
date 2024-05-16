// import { CanActivateFn } from '@angular/router';

// export const authGuard: CanActivateFn = (route, state) => {
//   return true;
// };

 
import { Injectable } from '@angular/core';

import { CanActivate, Router } from '@angular/router';

import { AuthenticationService } from './authentication.service';
 
@Injectable({

  providedIn: 'root'

})

export class AuthGuard implements CanActivate {
 
  constructor(private authService: AuthenticationService, private router: Router) {}
 
  canActivate(): boolean {

    if (this.authService.isLoggedIn()) {

      return true;

    } else {

      this.router.navigate(['/login']);

      return false;

    }

  }

}
