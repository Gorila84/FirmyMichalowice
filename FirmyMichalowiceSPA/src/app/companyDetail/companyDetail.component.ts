import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Company } from '../_models/company';


@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-companyDetail',
  templateUrl: './companyDetail.component.html',
  styleUrls: ['./companyDetail.component.css']
})
export class CompanyDetailComponent implements OnInit {
  company: Company;
  isCompanyActive: boolean;
  constructor(private route: ActivatedRoute,
             // private sanitizer: DomSanitizer
             ) { }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.company = data.company;
    });
    this.isCompanyActive = this.company.statusFromCeidg == 'AKTYWNY';
  }

  isEmptyObject(obj) {
  return (obj && (Object.keys(obj).length === 0));
}

}

