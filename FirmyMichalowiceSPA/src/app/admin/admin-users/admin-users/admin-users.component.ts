import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { Company } from 'src/app/_models/company';
import { Pagination } from 'src/app/_models/pagination';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { environment } from 'src/environments/environment';
import { AdminEditUserComponent } from '../../admin-edit-user/admin-edit-user/admin-edit-user.component';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.css']
})
export class AdminUsersComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private adminService: AdminService,
              private alertify: AlertifyService,
              private route: ActivatedRoute,
              private http: HttpClient,
              public dialog: MatDialog) { }

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

openDialog(id: number, name: string, isActive: boolean, isAdmin: boolean): void {
  const dialogRef = this.dialog.open(AdminEditUserComponent, {
    width: '300px',
    height: '400px',
    data: { id: id, name: name, isActive: isActive, isAdmin: isAdmin  },
   
  });
  debugger
  const sub = dialogRef.componentInstance.getDatas.subscribe(() => {
    this.getDatas();
  });
}
refreshAdminTable() {
  debugger
  this.adminService.getCompaniesForAdmin()
    .subscribe((data) => {
      this.companies = data;
    });
}

}
