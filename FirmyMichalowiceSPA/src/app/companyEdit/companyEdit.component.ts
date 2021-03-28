import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
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
  @ViewChild('editForm') editForm: NgForm;

  constructor(private route: ActivatedRoute,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data=>{
      this.company = data.company
    })
  }

  updateCompany(){
    console.log(this.company);
    this.alertify.success("Twoje dane zostały pomyślnie zaktualizowane.");
    this.editForm.reset(this.company);
  }

}
