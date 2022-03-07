import { Component, OnInit } from '@angular/core';
import { error } from 'protractor';
import { AlertifyService } from '../_services/alertify.service';
import { CompanyService } from '../_services/company.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css'],
})
export class AboutComponent implements OnInit {
  municipalitieList: any;
  constructor(
    private companyService: CompanyService,
    private alertify: AlertifyService
  ) {
    this.companyService
      .getMunicipalitie()
      .subscribe((data) => (this.municipalitieList = data)),
      (error) => {
        this.alertify.error(error);
      };
  }

  ngOnInit(): void {}
}
