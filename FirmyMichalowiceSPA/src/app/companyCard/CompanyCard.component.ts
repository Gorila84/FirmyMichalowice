import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Company } from '../_models/company';
import { UploadPhotoService } from '../_services/uploadPhoto.service';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-CompanyCard',
  templateUrl: './CompanyCard.component.html',
  styleUrls: ['./CompanyCard.component.css']
})
export class CompanyCardComponent implements OnInit {
  @Input() company: Company;
  imageToShow: any;
  noImageFound: boolean;

  constructor( private uploadPhotoService: UploadPhotoService,
               private sanitizer: DomSanitizer){
   }

// tslint:disable-next-line:typedef
ngOnInit() {
    // this.getImage();
}

  // tslint:disable-next-line:typedef
  getImage() {
    const mediaType = 'application/image';
    const blob = new Blob([this.company.photo.fileData], { type: mediaType });
    const unsafeImg = URL.createObjectURL(blob);
    this.imageToShow = this.sanitizer.bypassSecurityTrustUrl(unsafeImg);

  }
}


