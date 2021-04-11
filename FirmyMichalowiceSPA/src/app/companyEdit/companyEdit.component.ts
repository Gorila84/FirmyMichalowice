import { UpperCasePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Company } from '../_models/company';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { CompanyService } from '../_services/company.service';
import { UploadPhotoService } from '../_services/uploadPhoto.service';
import { FileUploader } from 'ng2-file-upload';
import { CoreEnvironment } from '@angular/compiler/src/compiler_facade_interface';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-companyEdit',
  templateUrl: './companyEdit.component.html',
  styleUrls: ['./companyEdit.component.css']
})
export class CompanyEditComponent implements OnInit {
  theFile: any = null;
  messages: string[] = [];
  uploader:FileUploader;
  hasBaseDropZoneOver:boolean;
  hasAnotherDropZoneOver:boolean;
  response:string;
  company: Company
  baseUrl = environment.apiUrl;
  @ViewChild('editForm') editForm: NgForm;
      
     
  constructor(private route: ActivatedRoute,
              private alertify: AlertifyService,
              private authService: AuthService,
              private companyService: CompanyService,
              private uploadPhotoService: UploadPhotoService) { }

  ngOnInit() {
    this.route.data.subscribe(data=>{
      this.company = data.company
    })
    this.initializeUploader();
  }
  public fileOverBase(e:any):void {
    this.hasBaseDropZoneOver = e;
  }

  updateCompany(){
    
    this.companyService.updateCompany(this.authService.decotedToken.nameid, this.company)
      .subscribe(next=> {
        this.alertify.success("Twoje dane zostały pomyślnie zaktualizowane.");
        this.editForm.reset(this.company);
      }, error => {
        this.alertify.error(error);
  });
  }
  initializeUploader() {
    this.uploader = new FileUploader({
        url: this.baseUrl + 'edycja/' + this.authService.decotedToken.nameid,
        authToken: 'Bearer ' + localStorage.getItem('token'),
        isHTML5: true,
        allowedFileType: ['image'],
        removeAfterUpload: true,
        autoUpload: false,
        maxFileSize: 10 * 1024 * 1024
    });
  }
  
  
  }


