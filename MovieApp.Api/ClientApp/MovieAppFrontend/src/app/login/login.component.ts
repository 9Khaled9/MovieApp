import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  constructor(private _authService: AuthService) {}

  loginWithGoogle(): void {
    this._authService.loginWithGoogle();
  }

  loginWithAzureB2C(): void {
    this._authService.loginWithAzureB2C();
  }

}
