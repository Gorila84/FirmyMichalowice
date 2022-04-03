import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ApiClient } from '../helpers/apiClient';
import { Company } from '../_models/company';
import { CompanyForEditByAdmin } from '../_models/companyForEditByAdmin';
import { PaginationResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  
  baseUrl = environment.apiUrl;

  
  constructor(private http: HttpClient, private client: ApiClient) { }

  getCompaniesForAdmin() {
    const headers = this.client.addBearer();
    return this.http.get<Company[]>(this.baseUrl + 'admin/getUsersForAdmin', {headers});
  }

  editUserForAdmin(company: CompanyForEditByAdmin) {
    // const headers = this.client.addBearer();
    debugger
    return this.http.put(this.baseUrl + 'admin/uzytkownicy/', company);
  }

  getTopFive(){
    const headers = this.client.addBearer();
    return this.http.get<Company[]>(this.baseUrl + 'admin/topfive', {headers});
  }

  getLastFive(){
    const headers = this.client.addBearer();
    return this.http.get<Company[]>(this.baseUrl + 'admin/lastfive', {headers});
  }
}
