import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserLogin,UserRegister } from '../interfaces/auth';
import { Observable } from 'rxjs';
import { AppConfig } from '../../../config';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl: string = AppConfig.baseUrl;

  constructor(private http: HttpClient) {}

  registerUser(userDetails: UserRegister):Observable<any> {
    return this.http.post(`${this.baseUrl}/Account/Register`, userDetails,{observe:'response'});
  }

  loginUser(userDetails: UserLogin):Observable<any> {
    return this.http.post(`${this.baseUrl}/Account/Login`, userDetails,{observe:'response'});
  }

  getMyUser(): string | undefined {
    return localStorage.getItem('email')?.toString();
  }
}
