import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { ApiClient } from '../helpers/apiClient';
import { Company } from '../_models/company';

@Injectable({
    providedIn: 'root'
  })
export class CompanyService {

    baseUrl = environment.apiUrl;

constructor(private http: HttpClient, private client: ApiClient) { }

getUsers(): Observable<Company[]>{
    return this.http.get<Company[]>(this.baseUrl + 'company');
  }

getUser(id: number): Observable<Company>{
  return this.http.get<Company>(this.baseUrl + 'company/' + id);
}

// tslint:disable-next-line:typedef
updateCompany(id: number, company: Company){
  const headers = this.client.addBearer();
  return this.http.put(this.baseUrl + 'company/' + id, company, { headers});
}

}

