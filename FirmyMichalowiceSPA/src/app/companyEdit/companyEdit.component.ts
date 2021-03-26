import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Company } from '../_models/company';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-companyEdit',
  templateUrl: './companyEdit.component.html',
  styleUrls: ['./companyEdit.component.css']
})
export class CompanyEditComponent implements OnInit {

  company: Company

  constructor(private route: ActivatedRoute,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data=>{
      this.company = this.company
    })
  }

}
