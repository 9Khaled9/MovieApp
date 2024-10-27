import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authUrl = `${environment.apiBaseUrl}/account`;  // Adjust to actual endpoint

  constructor(private http: HttpClient, private router: Router) {}

  loginWithGoogle(): void {
    window.location.href = `${this.authUrl}/login/Google`;
  }

  loginWithAzureB2C(): void {
    window.location.href = `${this.authUrl}/login/AzureAdB2C`;
  }

  logout() {
    localStorage.removeItem("access_token");
    this.router.navigate(['/login']);
  }

  // Save JWT to local storage.
  handleJwtToken(token: string): void {
    localStorage.setItem('access_token', token);
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem("access_token");
    return !!token;
  }
}
