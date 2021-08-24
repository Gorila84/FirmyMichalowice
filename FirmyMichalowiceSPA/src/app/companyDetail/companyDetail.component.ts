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
  constructor(private route: ActivatedRoute,
             ) { }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.company = data.company;
    });
  }
  // tslint:disable-next-line:typedef

}
