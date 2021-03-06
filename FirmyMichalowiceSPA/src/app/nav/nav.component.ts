import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { Company } from '../_models/company';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  @Input() loginInformation: any;
  company: Company
  constructor(public authService: AuthService, 
              private alertifyService: AlertifyService,
              private router: Router) {
   
  }

  ngOnInit() {
  }

  loginCheck(){
    return this.authService.loggedIn();
  }
  getUserId(){
    return this.authService.decotedToken.nameid;
  }
  
    logOut(){
    localStorage.removeItem('token');
    this.alertifyService.message('Zostałeś wylogowany');
    this.router.navigate([''])
  }
}
