import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
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
  company: Company;
  armsUrls :any;
  apiUrl = environment.apiUrl + 'Arms/GetArms';
  showArms = environment.showArms;

  constructor(public authService: AuthService, 
              private alertifyService: AlertifyService,
              private router: Router,
              private http: HttpClient) {
   
  }

  ngOnInit() {
    if(this.showArms){
      this.http.get(this.apiUrl).subscribe(data =>{
        this.armsUrls = data
      });
    }  
  }

  loginCheck(){
    return this.authService.loggedIn();
  }
  getUserId(){
    return this.authService.decotedToken.nameid;
  }

  getUserName(){
    return this.authService.decotedToken.unique_name;
  }
  
    logOut(){
    localStorage.removeItem('token');
    this.alertifyService.message('Zostałeś wylogowany');
    this.router.navigate([''])
  }
}
