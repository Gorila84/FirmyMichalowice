import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Company } from '../_models/company';
import {DomSanitizer} from '@angular/platform-browser';
import { CompanyService } from '../_services/company.service';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-companyDetail',
  templateUrl: './companyDetail.component.html',
  styleUrls: ['./companyDetail.component.css']
})
export class CompanyDetailComponent implements OnInit {
  company: Company;
  id: number;
  constructor(private route: ActivatedRoute,
              private companyService: CompanyService
             ) { }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.company = data.company;
    });
  }

  isEmptyObject(obj) {
  return (obj && (Object.keys(obj).length === 0));
}

  getIfAdditionalAddressIsTrue(){
    
    return this.company.additionalAddress;
  }

}

