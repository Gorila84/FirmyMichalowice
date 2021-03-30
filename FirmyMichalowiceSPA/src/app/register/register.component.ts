import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();
  @ViewChild('registerForm') registerForm: NgForm;
  model: any ={};
  
  constructor(private authService: AuthService, 
              private alertify: AlertifyService,
              ) { }

  ngOnInit() {
    
  }

  register(){
    let element = <HTMLInputElement> document.getElementById("rodoCheckBox");
    if (element.checked) {this.authService.register(this.model).subscribe(()=>{
      this.alertify.success('Rejestracja powiodła się.');},
      error => {this.alertify.error(error);}
  )}
    
  
  }

  cancel(){
    this.cancelRegister.emit(false);
    this.registerForm.reset(this.model);
    let element = <HTMLInputElement> document.getElementById("rodoCheckBox");
    element.checked=false;
    }


}
