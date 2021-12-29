import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Company } from '../_models/company';
import { CompanyType } from '../_models/companyTypes';
import { Pagination, PaginationResult } from '../_models/pagination';
import { AlertifyService } from '../_services/alertify.service';
import { CompanyService } from '../_services/company.service';


@Component({
  selector: 'app-companyList',
  templateUrl: './companyList.component.html',
  styleUrls: ['./companyList.component.css']
})
export class CompanyListComponent implements OnInit {

  companies: Company[];
  companyTypes: CompanyType[];
  pagination: Pagination;
  city: string;
  companyName: string;
  companyType:string;
  filterForm: FormControl;

  constructor(private route: ActivatedRoute,
              private companyService: CompanyService,
              private alertify: AlertifyService
              ) { }

  companyParams: any = {};
  
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.companies = data.companies.result;
      this.pagination = data.companies.pagination;
      this.companyParams.CompanyType = "";
      this.companyParams.CompanyName = "";
      this.companyParams.City = "";
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


clear(){
  this.filterForm.reset();
}

 
}
