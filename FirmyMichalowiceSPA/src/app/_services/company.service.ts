import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { ApiClient } from '../helpers/apiClient';
import { Company } from '../_models/company';
import { PaginationResult } from '../_models/pagination';
import { map, catchError } from 'rxjs/operators';
import { CompanyType } from '../_models/companyTypes';

@Injectable({
    providedIn: 'root'
  })
export class CompanyService {

    baseUrl = environment.apiUrl;

constructor(private http: HttpClient, private client: ApiClient) { }

getUsers(page?, itemsPerPage?, userParams?, likesParam?): Observable<PaginationResult<Company[]>>{

    const paginationResult: PaginationResult<Company[]> = new PaginationResult<Company[]>();
    let params = new HttpParams();

    if (page != null && itemsPerPage != null){
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    return this.http.get<Company[]>(this.baseUrl + 'company', {observe: 'response', params})
      .pipe(
        map(response => {
          paginationResult.result = response.body;
          if (response.headers.get('Pagination') != null){
            paginationResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginationResult;
        })
      )
    ;
  }

getUser(id: number): Observable<Company>{
  return this.http.get<Company>(this.baseUrl + 'company/' + id);
}

// tslint:disable-next-line:typedef
updateCompany(id: number, company: Company){
  const headers = this.client.addBearer();
  return this.http.put(this.baseUrl + 'company/' + id, company, { headers});
}

getDataFromCEIDG(nip: string){
  return this.http.get<string>(this.baseUrl + 'company/GetDataFromCeidg/' + nip);
}
}
