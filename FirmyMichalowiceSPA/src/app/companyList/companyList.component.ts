import { Component, OnInit } from '@angular/core';
import { FormGroup, NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { from } from 'rxjs';
import { Company } from '../_models/company';
import { Pagination, PaginationResult } from '../_models/pagination';
import { AlertifyService } from '../_services/alertify.service';
import { CompanyService } from '../_services/company.service';


@Component({
  selector: 'app-companyList',
  templateUrl: './companyList.component.html',
  styleUrls: ['./companyList.component.css']
})

export class CompanyListComponent implements OnInit {

  filterForms: FormGroup;
  companies: Company[];
  companyName:string;
  pagination: Pagination;
  
  constructor(private route: ActivatedRoute,
              private companyService: CompanyService,
              private alertify: AlertifyService
              ) { }

  companyParams: any = {};
  
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.companies = data.companies.result;
      this.pagination = data.companies.pagination;
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadCompanies();
  }

  loadCompanies() {
    this.companyService.getUsers(this.pagination.currentPage, this.pagination.itemsPerPage, this.companyParams)
     .subscribe((res: PaginationResult<Company[]>) => {
      this.companies = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }



  clearFilterFields(){
  
  (<HTMLInputElement> document.getElementById('companyName')).value = '';
  (<HTMLInputElement> document.getElementById('companyType')).value = '';
  (<HTMLInputElement> document.getElementById('city')).value = '';

  this.companyParams.CompanyName = '';
  this.companyParams.CompanyType ='';
  this.companyParams.City = '';
  this.loadCompanies();
  
}
 
}
