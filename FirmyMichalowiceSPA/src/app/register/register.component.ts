import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

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
  
  constructor(private authService: AuthService, 
              private alertify: AlertifyService,
              private router: Router
              ) { }

  ngOnInit() {
    
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
  matcher = new MyErrorStateMatcher();



  get f() { return this.singInForm.controls; }
  

  register(){
    let element = <HTMLInputElement> document.getElementById("rodoCheckBox");

    if (this.emailFormControl.invalid || this.passwordFormControl.invalid || this.nipFormControl.invalid || !element.checked) {
      return;
  }
  
      this.authService.register(this.model).subscribe( data=>{
      this.alertify.success('Rejestracja powiodła się.');},
      error => {
        alert(error);
        this.alertify.error(error.error);
      }, 
      ()=>
      {
        this.authService.login(this.model).subscribe(()=>
        this.router.navigate(['edycja/']
        ));
      }

  )}
    

  cancel(){
    this.cancelRegister.emit(false);
    this.registerForm.reset(this.model);
    let element = <HTMLInputElement> document.getElementById("rodoCheckBox");
    element.checked=false;
    }


}
