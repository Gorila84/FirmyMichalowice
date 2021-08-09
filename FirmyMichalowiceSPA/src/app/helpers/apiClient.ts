import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthService } from '../_services/auth.service';


@Injectable({
    providedIn: 'root'
  })
export class ApiClient {

    baseUrl = environment.apiUrl;

constructor(private http: HttpClient, private authService: AuthService) {}

public addBearer(): HttpHeaders {
    const token =   localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      // tslint:disable-next-line:object-literal-key-quotes
      'Authorization': `Bearer ${token}`
    });
}
}
