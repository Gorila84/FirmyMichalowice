import { Component, OnInit, ViewChild } from '@angular/core';
import { Company } from '../_models/company';
import { CompanyService } from '../_services/company.service';
import { Pagination, PaginationResult } from '../_models/pagination';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from '../_services/admin.service';
import { MatTableDataSource } from '@angular/material/table';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private adminService: AdminService,
              private alertify: AlertifyService,
              private route: ActivatedRoute,
              private http: HttpClient) { }

  companies: Company[] =[];
  pagination: Pagination;
  companyParams: any = {};
  dataSource: MatTableDataSource<any>;
  displayedColumns: string[] = ['companyName', 'phoneNumber', 'emailAddress', 'city', 'isAdmin', 'isActive', 'modify', 'created', 'buttons' ];
  baseUrl = environment.apiUrl;

  ngOnInit(): void {

    this.getDatas();
  }




getDatas(){
  this.adminService.getCompaniesForAdmin().subscribe(data => {
    this.dataSource = new MatTableDataSource(data);
    this.dataSource.paginator = this.paginator;
  });
}

}
   



