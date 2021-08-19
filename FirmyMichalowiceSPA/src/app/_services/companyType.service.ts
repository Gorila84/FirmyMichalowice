import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { debounce, debounceTime, map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ApiClient } from '../helpers/apiClient';
import { Company } from '../_models/company';
import { CompanyType } from '../_models/companyTypes';

@Injectable({
  providedIn: 'root'
})
export class CompanyTypeService {

  baseUrl = environment.apiUrl;

constructor(private httpClient: HttpClient, private client: ApiClient) { }


// tslint:disable-next-line:typedef
 getCompanyTypes(): Observable<any> {
   const headers = this.client.addBearer();
   return this.httpClient.get<any>(this.baseUrl + `company/getcompanytypes`, {headers});
 }


}
