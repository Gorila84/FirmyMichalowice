import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CEIDGService {

  token = "eyJraWQiOiJjZWlkZyIsImFsZyI6IkhTNTEyIn0.eyJnaXZlbl9uYW1lIjoiS2Fyb2wiLCJwZXNlbCI6Ijg4MDQxMTEwODM0IiwiaWF0IjoxNj" +
                "MwMDkzMjg2LCJmYW1pbHlfbmFtZSI6IkZyYW5jemFrIiwiY2xpZW50X2lkIjoiVVNFUi04ODA0MTExMDgzNC1LQVJPTC1GUkFOQ1pBSyJ9.8H0" +
                "gw-BOPSAFSdWR_NAohscrpJU-R2Hbb87xY9IDZIR3toCrjSR2Ig5xttOBSRDlWKs3Z_Xyr6vNji4_Vn1v0g"

  baseUrl = environment.hdUrl;  
  constructor(private http: HttpClient) { 
   
  }

  getCompanyData(nip: string): Observable<any>{
    const headers = this.addBearer();
    nip = '6821706218';
    return this.http.get<any>(this.baseUrl + nip, {headers});
  }


 addBearer(): HttpHeaders {
    const token =   this.token;
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
}
}
