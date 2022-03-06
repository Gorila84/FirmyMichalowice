import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Company } from '../_models/company';
import { CompanyType } from '../_models/companyTypes';
import { Municipalitie } from '../_models/municipalitie';
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
  municipalities:Municipalitie[];

  constructor(private route: ActivatedRoute,
              private companyService: CompanyService,
              private alertify: AlertifyService
              ) { }

  companyParams: any = {};
  
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.companies = data.companies.result;
      this.municipalities = data.municipalities;
      this.pagination = data.companies.pagination;
      this.companyParams.CompanyType = "";
      this.companyParams.CompanyName = "";
      this.companyParams.City = "";
      this.companyParams.Municipalitie ="";
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



  clearFiltrFileds(){
    var name = <HTMLInputElement>document.getElementById('companyName');
    var category = <HTMLInputElement>document.getElementById('companyName');
    var city = <HTMLInputElement>document.getElementById('companyName');
    name.value = "";
    category.value = "";
    city.value = "";

    this.companyParams.CompanyType = "";
    this.companyParams.CompanyName = "";
    this.companyParams.City = "";

    this.loadCompanies();
  }
 
  unselect(): void {
    this.companyParams.Municipalitie = "";
 }
}
