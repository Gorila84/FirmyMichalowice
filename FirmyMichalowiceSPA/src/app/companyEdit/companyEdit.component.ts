
import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl, NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Company } from '../_models/company';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { CompanyService } from '../_services/company.service';
import { UploadPhotoService } from '../_services/uploadPhoto.service';
import { environment } from 'src/environments/environment';
import { HttpEventType } from '@angular/common/http';
import { NGXLogger } from 'ngx-logger';
import { map, startWith } from 'rxjs/operators';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { buffer } from 'rxjs/operators';
import { CompanyTypeService } from '../_services/companyType.service';
import { toBase64String } from '@angular/compiler/src/output/source_map';





@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-companyEdit',
  templateUrl: './companyEdit.component.html',
  styleUrls: ['./companyEdit.component.css']
})
export class CompanyEditComponent implements OnInit {
  myControl = new FormControl();
  options: string[] = [];
  filteredOptions: Observable<string[]>;
  // tslint:disable-next-line:no-output-on-prefix
  @Output() public onUploadFinished = new EventEmitter();
  company: Company;
  baseUrl = environment.apiUrl;
  fileToUpload: File | null = null;
  shown: any;

  @ViewChild('editForm') editForm: NgForm;
  constructor(private route: ActivatedRoute,
              private alertify: AlertifyService,
              private authService: AuthService,
              private companyService: CompanyService,
              private uploadPhotoService: UploadPhotoService,
              private logger: NGXLogger,
              private companyTypeService: CompanyTypeService) { }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    
    this.getCompanyTypes();
    this.route.data.subscribe(data => {
      this.company = data.company;
    });
    // this.getImage(this.authService.decotedToken.nameid);
    // this.getImage();
    console.log(this.company);
    this.filteredOptions = this.myControl.valueChanges
    .pipe(
      startWith(''),
      map(value => this._filter(value))
    );
  }
  // tslint:disable-next-line:typedef
  getImage(result: string) {
    return result.slice(22, result.length);
  }

  // tslint:disable-next-line:typedef
  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
}
  // tslint:disable-next-line:typedef
  updateCompany(){
    this.companyService.updateCompany(this.authService.decotedToken.nameid, this.company)
      .subscribe(next => {
        this.alertify.success('Twoje dane zostały pomyślnie zaktualizowane.');
        this.editForm.reset(this.company);
      }, error => {
        this.alertify.error(error);
  });
  }
  // tslint:disable-next-line:typedef
  uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    const fileToUpload = files[0] as File;
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.uploadPhotoService.uploadImage(this.authService.decotedToken.nameid, fileToUpload)
       .subscribe(async event => {
         if (event.type === HttpEventType.UploadProgress) {
         }
         else if (event.type === HttpEventType.Response) {
           this.alertify.success('Dodano logo.');
           // this.getImage();
           // window.location.reload();
           const fileReader = new FileReader();
           fileReader.onload = (e) => {
           this.company.photo.fileData = this.getImage(fileReader.result.toString()) as any ;
           };
           fileReader.readAsDataURL(files[0]);

         }
       }, err => {
          const userId = this.authService.decotedToken.nameid;
          err.userId = userId;
          err.componentName = this.constructor.name;
          this.logger.error(err);
          this.alertify.error('Błąd. Nie udało się wysłać pliku');
         }
       );
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }

  // tslint:disable-next-line:typedef
  getCompanyTypes() {
   this.companyTypeService.getCompanyTypes().subscribe(data => {
    data.forEach((item) => {
      this.options.push(item.toLocaleUpperCase());
    });
}, error => {
    this.alertify.error(error);
    console.log(error);
});

  }
 getCompanyData(){
   this.companyService.getDataFromCEIDG(this.company.nip).subscribe(data => {
    this.alertify.success('Twoje dane zostały pomyślnie pobrane.');
    let myObj = JSON.parse(data);
    this.company.companyName = myObj.firma[0].nazwa;  
    this.company.postalCode = myObj.firma[0].adresDzialanosci.kod;
    this.company.street = myObj.firma[0].adresDzialanosci.ulica + ' ' + myObj.firma[0].adresDzialanosci.budynek;
    this.company.city = myObj.firma[0].adresDzialanosci.miasto;
    this.company.nip = myObj.firma[0].wlasciciel.nip;
  }, error => {
    this.alertify.error(error);
});
 }

  getCompanyType(){
    console.log(this.company.companyType)
    return this.company.companyType;
  }


}
