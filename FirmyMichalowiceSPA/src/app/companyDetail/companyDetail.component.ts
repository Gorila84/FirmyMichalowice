import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Company } from '../_models/company';
import { AlertifyService } from '../_services/alertify.service';
import { CompanyService } from '../_services/company.service';

@Component({
  selector: 'app-companyDetail',
  templateUrl: './companyDetail.component.html',
  styleUrls: ['./companyDetail.component.css']
})
export class CompanyDetailComponent implements OnInit {
  company: Company;
  constructor(private companyService: CompanyService,
    private alertify: AlertifyService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.company = data.company;
    });
  }

}
