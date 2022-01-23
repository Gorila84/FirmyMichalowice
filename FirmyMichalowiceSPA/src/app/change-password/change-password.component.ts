import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MyErrorStateMatcher } from '../register/register.component';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  model: any = {};
  newPassword: string;
  confirmPassword: string;

  constructor(private authService: AuthService,
              private alertifyService: AlertifyService) { }

  ngOnInit() {
  }
  passwordFormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(6),
  ]);
  secondPasswordFormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(6),
  ]);
  matcher = new MyErrorStateMatcher();

  changePassword(){
    
    this.newPassword = (<HTMLInputElement>document.getElementById("newPassword")).value;
    this.confirmPassword = (<HTMLInputElement>document.getElementById("confirmPassword")).value;

    if(this.newPassword === this.confirmPassword){
      this.authService.changePassword(this.authService.decotedToken.nameid, this.model).subscribe(
        next => 
        {this.alertifyService.success("Twoje hasło zostało zmienione")
      }, 
      error=>{this.alertifyService.error('Nie udało się zmienić hasła');}
      
      );
    }else{
      this.alertifyService.error('Nie udało się zmienić hasła. Wprowadzone hasła są różne!!!');
    }

    
  }  

  getUserId(){

    return this.authService.decotedToken.nameid;
  }
}
