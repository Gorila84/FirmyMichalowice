import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
import { Company } from '../_models/company';
import { CompanyService } from '../_services/company.service';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-companyDetail',
  templateUrl: './companyDetail.component.html',
  styleUrls: ['./companyDetail.component.css']
})
export class CompanyDetailComponent implements OnInit {
  company: Company;
  isCompanyActive: boolean;
  isEnabledGeolocation2Url: boolean;
  id: number;
  showArms = environment.showArms;
  constructor(private route: ActivatedRoute,
              private companyService: CompanyService
             ) { }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.company = data.company;
    });
    this.isCompanyActive = this.company.statusFromCeidg == 'AKTYWNY';
    this.isEnabledGeolocation2Url = this.company.geolocation2Url.length == 0;
  }

  isEmptyObject(obj) {
  return (obj && (Object.keys(obj).length === 0));
}

  getIfAdditionalAddressIsTrue(){
    
    return this.company.additionalAddress;
  }

}

