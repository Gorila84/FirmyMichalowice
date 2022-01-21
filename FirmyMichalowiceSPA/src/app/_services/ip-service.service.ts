import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CookieConsent } from '../_models/cookieConsent';

@Injectable({
  providedIn: 'root'
})
export class IpServiceService {
  baseUrl = environment.apiUrl;
  constructor(private http:HttpClient) { }  
  public getIPAddress()  
  {  
    return this.http.get("http://api.ipify.org/?format=json");  
  }  

  addConsent(consent: CookieConsent){
     return this.http.post<CookieConsent>(this.baseUrl + 'CookieConsent/addconsent/', consent);
  }
}
