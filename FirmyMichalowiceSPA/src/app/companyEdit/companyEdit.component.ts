import { UpperCasePipe } from '@angular/common';
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
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { map, startWith } from 'rxjs/operators';
import { CompanyTypeService } from '../_services/companyType.service';


@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-companyEdit',
  templateUrl: './companyEdit.component.html',
  styleUrls: ['./companyEdit.component.css']
})
export class CompanyEditComponent implements OnInit {
  theFile: any = null;
  progress: number;
  message: string;
  myProgress: number;
  imageToShow: SafeUrl | null = null;
  noImageFound: boolean;
  content: any;
  myControl = new FormControl();
  options: string[] = [];
  filteredOptions: Observable<string[]>;
  // tslint:disable-next-line:no-output-on-prefix
  @Output() public onUploadFinished = new EventEmitter();
  company: Company;
  baseUrl = environment.apiUrl;
  fileToUpload: File | null = null;
  @ViewChild('editForm') editForm: NgForm;
  constructor(private route: ActivatedRoute,
              private alertify: AlertifyService,
              private authService: AuthService,
              private companyService: CompanyService,
              private uploadPhotoService: UploadPhotoService,
              private logger: NGXLogger,
              private sanitizer: DomSanitizer,
              private companyTypeService: CompanyTypeService) { }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.getCompanyTypes();
    this.route.data.subscribe(data => {
      this.company = data.company;
    });
    this.progress = 0;
    this.getImage(this.authService.decotedToken.nameid);

    this.filteredOptions = this.myControl.valueChanges
    .pipe(
      startWith(''),
      map(value => this._filter(value))
    );
  }
  // tslint:disable-next-line:typedef
  getImage(userId: number) {
    const mediaType = 'application/image';
    this.uploadPhotoService.getImage(userId)
      .subscribe(data => {
        const blob = new Blob([data], { type: mediaType });
        const unsafeImg = URL.createObjectURL(blob);
        this.imageToShow = this.sanitizer.bypassSecurityTrustUrl(unsafeImg);
    }, error => {
        console.log(error);
        this.noImageFound = true;
    });

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
       .subscribe(event => {
         if (event.type === HttpEventType.UploadProgress) {
           this.progress = Math.round(100 * event.loaded / event.total);
         }
         else if (event.type === HttpEventType.Response) {
           this.message = 'Dodano logo.';
           this.getImage(this.authService.decotedToken.nameid);
         }
       }, err => {
          this.progress = 99;
          this.message = 'Błąd. Nie udało się wysłać pliku';
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
    console.log(this.options);
    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }

  // tslint:disable-next-line:typedef
  getCompanyTypes() {
   this.companyTypeService.getCompanyTypes().subscribe(data => {
    data.forEach((item) => {
      this.options.push(item.toLocaleUpperCase());
    });
}, error => {
    console.log(error);
});

  }
}
