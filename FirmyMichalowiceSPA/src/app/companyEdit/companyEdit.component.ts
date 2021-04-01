import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Company } from '../_models/company';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { CompanyService } from '../_services/company.service';

@Component({
  selector: 'app-companyEdit',
  templateUrl: './companyEdit.component.html',
  styleUrls: ['./companyEdit.component.css']
})
export class CompanyEditComponent implements OnInit {

  company: Company
  @ViewChild('editForm') editForm: NgForm;

  constructor(private route: ActivatedRoute,
              private alertify: AlertifyService,
              private authService: AuthService,
              private companyService: CompanyService) { }

  ngOnInit() {
    this.route.data.subscribe(data=>{
      this.company = data.company
    })
  }

  updateCompany(){
    
    this.companyService.updateCompany(this.authService.decotedToken.nameid, this.company)
      .subscribe(next=> {
        this.alertify.success("Twoje dane zostały pomyślnie zaktualizowane.");
        this.editForm.reset(this.company);
      }, error => {
        this.alertify.error(error);
  });
  
  }

}
