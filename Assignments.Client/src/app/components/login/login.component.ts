import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { UserLogin } from '../../interfaces/auth';
import { LoginResponse } from '../../interfaces/LoginRes';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  formLoginClass: string = 'myformClass';

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]],
  });

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private messageService: MessageService
  ) {}

  get email() {
    return this.loginForm.controls['email'];
  }
  get password() {
    return this.loginForm.controls['password'];
  }

  loginUser() {
    const userLogin: UserLogin = {
      email: this.email.value!,
      password: this.password.value!,
    };

    this.authService.loginUser(userLogin).subscribe(
      (response) => {
        const res: LoginResponse = response.body;
        localStorage.setItem('token', res.token);

        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Login successfully',
        });
        this.router.navigateByUrl('/home');
      },
      (error) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: error.error.errors,
        });
      }
    );
  }
}
