import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Company } from '../_models/company';
import { AlertifyService } from '../_services/alertify.service';
import { CompanyService } from '../_services/company.service';
import { UploadPhotoService } from '../_services/uploadPhoto.service';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-companyDetail',
  templateUrl: './companyDetail.component.html',
  styleUrls: ['./companyDetail.component.css']
})
export class CompanyDetailComponent implements OnInit {
  imageToShow: SafeUrl | null = null;
  noImageFound: boolean;
  company: Company;
  constructor(private companyService: CompanyService,
              private alertify: AlertifyService,
              private route: ActivatedRoute,
              private uploadPhotoService: UploadPhotoService,
              private sanitizer: DomSanitizer) { }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.company = data.company;
      // this.getImage(this.company.id);
    });
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
}
