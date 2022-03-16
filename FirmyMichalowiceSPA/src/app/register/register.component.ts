import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { ReCaptchaV3Service } from 'ng-recaptcha';
import { Category } from '../_models/category';
import { CompanyType } from '../_models/companyTypes';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { CompanyService } from '../_services/company.service';
import { RecaptchaService } from '../_services/recaptcha-service.service';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();
  @ViewChild('registerForm') registerForm: NgForm;
  model: any ={};
  singInForm: FormGroup;
  categories: Category[];
  constructor(private authService: AuthService, 
              private alertify: AlertifyService,
              private companyService: CompanyService,
              private router: Router,
              private recaptchaV3Service: ReCaptchaV3Service,
              private recaptchaValidationService : RecaptchaService
              ) { }

  ngOnInit() {
    this.getCompanyType();
  }
  
  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
     ]);

  nipFormControl = new FormControl('', [
    Validators.required,
    Validators.pattern("^[0-9]*$"),
    Validators.maxLength(10),
    Validators.minLength(10),
  ]);

  passwordFormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(6),
  ]);

  shortDescriptionFormControl = new FormControl('', [
    Validators.required,
    Validators.maxLength(255)
  ]);
  categoryFormControl = new FormControl('', [
    Validators.required
     ]);
  matcher = new MyErrorStateMatcher();



  get f() { return this.singInForm.controls; }
  

  register(form: NgForm)
  {
    if (form.invalid) {
      for (const control of Object.keys(form.controls)) {
        form.controls[control].markAsTouched();
      }
    }
    let element = <HTMLInputElement> document.getElementById("rodoCheckBox");

    if (this.emailFormControl.invalid || this.passwordFormControl.invalid || this.nipFormControl.invalid || !element.checked || this.shortDescriptionFormControl.invalid || this.categoryFormControl.invalid) {
      return;
  }
  this.recaptchaV3Service.execute('importantAction')
  .subscribe((token: string) => {

    this.recaptchaValidationService.check(token).subscribe(data => {
    const result = JSON.parse(JSON.stringify(data));
    if(result['success'])
    {
      this.authService.register(this.model).subscribe( data =>{
      this.alertify.success('Rejestracja powiodła się.');},
      error => {
        this.alertify.error(error.error);
      }, 

      ()=>
      {
        this.authService.login(this.model).subscribe(()=>
        this.router.navigate(['edycja/' + this.authService.decotedToken.nameid]
        ));
      },
     )
    }

  })
})
  }


  cancel(){
    this.cancelRegister.emit(false);
    this.registerForm.reset(this.model);
    let element = <HTMLInputElement> document.getElementById("rodoCheckBox");
    element.checked=false;
    }
  
    getCompanyType(){

      this.companyService.getCategories().subscribe((data) =>{
        this.categories = data;
      } );
    }
}
