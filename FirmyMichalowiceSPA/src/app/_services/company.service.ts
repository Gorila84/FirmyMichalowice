import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { Company } from '../_models/company';

@Injectable({
    providedIn: 'root'
  })
export class CompanyService {

    baseUrl = environment.apiUrl;
    
constructor(private http: HttpClient) { }

getUsers() : Observable<Company[]>{
    return this.http.get<Company[]>(this.baseUrl + 'company');
  }

getUser(id: number):Observable<Company>{
  return this.http.get<Company>(this.baseUrl+'company/'+ id);
} 

updateCompany(id: number, company: Company){
  return this.http.put(this.baseUrl + 'company/' + id, company);
}

}
