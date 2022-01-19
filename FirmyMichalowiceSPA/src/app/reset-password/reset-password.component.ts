import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Company } from '../_models/company';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  model: any = {};
  company: Company;

  
  constructor(public authService: AuthService,
              private alertifyService: AlertifyService,) { }

  ngOnInit(): void {
  }

  loginFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
   ]);

  resetPassword(){
    this.authService.resetPassword(this.model).subscribe(next => 
      {this.alertifyService.success("Twoje hasło zostało zresetowane i wysłane na Twojego maila.")
    }, 
    error=>{this.alertifyService.error('Wprowadziłeś niepoprawny email. Twoje hasło nie zostało zresetowane.');}
    );
  }

}
