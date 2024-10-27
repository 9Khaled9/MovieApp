import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    const token = localStorage.getItem('access_token'); // Assuming you're storing JWT here

    if (token) {
      // User is logged in; return true
      return true;
    }

    // User is not logged in; redirect to the login page
    this.router.navigate(['/login']);
    return false;
  }
}
