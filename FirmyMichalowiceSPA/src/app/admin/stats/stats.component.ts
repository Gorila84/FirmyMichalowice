import { Component, OnInit } from '@angular/core';
import { Company } from 'src/app/_models/company';
import { AdminService } from 'src/app/_services/admin.service';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.css']
})
export class StatsComponent implements OnInit {

  constructor(private adminService: AdminService) { }

  companies: Company[] =[];
  firstFiveData: MatTableDataSource<any>;
  lastFiveData: MatTableDataSource<any>;
  displayedColumns: string[] = ['id','companyName', 'entryCount'];
  
  ngOnInit(): void {
    this.getFirst5();
    this.getLast5();
  }


  getFirst5(){
    this.adminService.getTopFive().subscribe(data => {
      this.firstFiveData = new MatTableDataSource(data);
    debugger
    });
  }
  getLast5(){
    this.adminService.getLastFive().subscribe(data => {
      this.lastFiveData = new MatTableDataSource(data);
    
    });
  }
}
