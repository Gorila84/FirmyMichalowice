import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  baseUrl = environment.apiUrl + 'message/';


  constructor(private http: HttpClient) { }

  // tslint:disable-next-line:typedef
  sendMessage(model: any) {
    return this.http.post(this.baseUrl + 'AddMessage', model)
      .pipe(map((response: boolean) => {
       return response
      }));
  }
}
