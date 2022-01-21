import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { Company } from '../_models/company';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = environment.apiUrl + 'auth/';
  resetPassUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();
  decotedToken: any;
  client: any;

  constructor(private http: HttpClient) { }

  // tslint:disable-next-line:typedef
  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model)
      .pipe(map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decotedToken = this.jwtHelper.decodeToken(user.token);
          console.log(this.decotedToken);
        }
      }));
  }
  // tslint:disable-next-line:typedef
  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model);
  }
  // tslint:disable-next-line:typedef
  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  // tslint:disable-next-line:typedef
  logOut() {
    localStorage.removeItem('token');
  }
  resetPassword(model:any){
    return this.http.post(this.resetPassUrl + 'resetPassword', model);
  }

  changePassword(id:number,model:any){
    //const headers = this.client.addBearer();
    return this.http.post(this.resetPassUrl + 'changePassword/' + id, model);
  }
}
