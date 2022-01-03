import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RecaptchaService{

  ulr = environment.apiUrl + "recaptcha/check";
  //ulr = environment.recaptcha.apiUrl;
  constructor( private http: HttpClient) { }

  check(token: string) {
    let headers = new HttpHeaders({'Content-Type' : 'application/json'})
    let model = {"secret" : "", "response" : token}
     return this.http.post(this.ulr ,model, {headers: headers})
  }
}
