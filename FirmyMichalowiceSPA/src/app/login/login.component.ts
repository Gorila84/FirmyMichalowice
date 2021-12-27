import { Component, OnInit } from '@angular/core';
import { FormControl, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ReCaptchaV3Service } from 'ng-recaptcha';
import { Company } from '../_models/company';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { RecaptchaService } from '../_services/recaptcha-service.service';

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
              private router: Router,
              private recaptchaV3Service: ReCaptchaV3Service,
              private recaptchaValidationService : RecaptchaService,) { }

  ngOnInit() {}
  
  loginFormControl = new FormControl('', [
      Validators.required,
      Validators.email,
     ]);

  passwordFormControl = new FormControl('', [
      Validators.required,
      
     ]);



  login(form: NgForm){
    alert()
debugger
    if (form.invalid) {
      for (const control of Object.keys(form.controls)) {
        form.controls[control].markAsTouched();
      }
      return;
    }

    if (this.loginFormControl.invalid || this.passwordFormControl.invalid) {
      return;
  }

  
  this.recaptchaV3Service.execute('importantAction')
  .subscribe((token: string) => {

    this.recaptchaValidationService.check(token).subscribe(data => {
    const result = JSON.parse(JSON.stringify(data));
    debugger
    if(result['success'])
    {
    this.authService.login(this.model).subscribe(next => {
      this.alertifyService.success('Zalogowałeś się do aplikacji');
    }, error => {
      this.alertifyService.error('Wprowadziłeś niepoprawny email lub hasło');
    }, ()=>(this.router.navigate(['edycja/', this.authService.decotedToken.nameid])
     ));
   }})}
  )}


  loggedIn(){
    return this.authService.loggedIn();
  }

}
