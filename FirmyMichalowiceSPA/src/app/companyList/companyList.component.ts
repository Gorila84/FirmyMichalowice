import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Company } from '../_models/company';
import { AlertifyService } from '../_services/alertify.service';
import { CompanyService } from '../_services/company.service';

@Component({
  selector: 'app-companyList',
  templateUrl: './companyList.component.html',
  styleUrls: ['./companyList.component.css']
})
export class CompanyListComponent implements OnInit {

  companies: Company[];
  constructor(private route: ActivatedRoute,
              private companyService: CompanyService,
              private alertify: AlertifyService
              ) { }

  
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.companies = data.companies;
    });
  }
 
}
