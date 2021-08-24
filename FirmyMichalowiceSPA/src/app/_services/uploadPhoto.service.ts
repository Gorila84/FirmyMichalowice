import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CompanyEditComponent } from '../companyEdit/companyEdit.component';
import { CompanyEditlResolver } from '../_resolvers/company_edit_resolver';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ApiClient } from '../helpers/apiClient';

@Injectable({
  providedIn: 'root'
})
export class UploadPhotoService {

  baseUrl = environment.apiUrl;

constructor(private httpClient: HttpClient) { }

// tslint:disable-next-line:typedef
uploadImage(userId, image) {
  const formData: FormData = new FormData();
  formData.append('Image', image, image.name);
  const token =   localStorage.getItem('token');
  // tslint:disable-next-line:max-line-length
  return this.httpClient.post(this.baseUrl + `photos/upload/${userId}`, formData, { headers: {
      // tslint:disable-next-line:object-literal-key-quotes
      'Authorization': `Bearer ${token}`
}, observe: 'events', reportProgress: true});
}

}
