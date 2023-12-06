import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { passwordMatchValidators } from '../../shared/password-match.directive';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { UserRegister } from '../../interfaces/auth';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  registerForm = this.fb.group(
    {
      username: ['', [Validators.required, Validators.pattern(/^[a-zA-Z]+$/)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]],
    },
    { validators: passwordMatchValidators }
  );

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    public messageService: MessageService,
    private router: Router
  ) {}

  get username() {
    return this.registerForm.controls['username'];
  }

  get email() {
    return this.registerForm.controls['email'];
  }

  get password() {
    return this.registerForm.controls['password'];
  }

  get confirmPassword() {
    return this.registerForm.controls['confirmPassword'];
  }

  splitString(inputString: string): string[] {
    return inputString.split(' ');
  }

  submitDetails() {
    const postData = { ...this.registerForm.value };
    delete postData.confirmPassword;
    this.authService.registerUser(postData as UserRegister).subscribe(
      (response) => {
        if (response.status === 200) {
          const resBody = this.splitString(response.body.response);

          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: resBody[1] + ' registered successfull',
          });
          this.router.navigate(['login']);
        }
      },
      (error) => {
        const errArray = error.error.errors;
        for (let i = 0; i < errArray.length; i++) {
          const element = errArray[i];
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: element,
          });
        }
      }
    );
  }
}
