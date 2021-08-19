import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Company } from '../_models/company';
import { UploadPhotoService } from '../_services/uploadPhoto.service';

@Component({
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
  this.getImage(this.company.id);
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


