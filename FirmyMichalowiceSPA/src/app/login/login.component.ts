import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Company } from '../_models/company';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = {};
  company: Company;

  constructor(public authService: AuthService, 
              private alertifyService: AlertifyService, 
              private router: Router) { }

  ngOnInit() {}
  
  loginFormControl = new FormControl('', [
      Validators.required,
      Validators.email,
     ]);

  passwordFormControl = new FormControl('', [
      Validators.required,
      
     ]);



  login(){
    this.authService.login(this.model).subscribe(next => {
      this.alertifyService.success('Zalogowałeś się do aplikacji');
    }, error => {
      this.alertifyService.error('Wprowadziłeś niepoprawny email lub hasło');
    }, ()=>(this.router.navigate(['edycja/', this.authService.decotedToken.nameid])
    ));
  }
  loggedIn(){
    return this.authService.loggedIn();
  }

}
