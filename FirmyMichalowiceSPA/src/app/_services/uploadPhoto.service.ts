import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CompanyEditComponent } from '../companyEdit/companyEdit.component';
import { CompanyEditlResolver } from '../_resolvers/company_edit_resolver';

@Injectable({
  providedIn: 'root'
})
export class UploadPhotoService {

  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

// uploadFile(theFile: CompanyEditComponent):Observable<any>{
//   return this.http.post<CompanyEditComponent>(this.baseUrl + 'photos', theFile);
// }


}
