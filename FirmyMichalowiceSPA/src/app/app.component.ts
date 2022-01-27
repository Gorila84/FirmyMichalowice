import { Component, OnInit, OnDestroy  } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './_services/auth.service';
import { NgcCookieConsentService, NgcNoCookieLawEvent, NgcStatusChangeEvent } from 'ngx-cookieconsent';
import { Subscription }   from 'rxjs';
import { IpServiceService } from './_services/ip-service.service';
import { CookieConsent } from './_models/cookieConsent';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy{

   //keep refs to subscriptions to be able to unsubscribe later
   private popupOpenSubscription!: Subscription;
   private statusChangeSubscription!: Subscription;


  jwtHelper = new JwtHelperService();

  constructor(private authService: AuthService,
    private ccService: NgcCookieConsentService,
    private ip: IpServiceService){}

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if(token){
      this.authService.decotedToken = this.jwtHelper.decodeToken(token);
    }
    
    // subscribe to cookieconsent observables to react to main events
    this.popupOpenSubscription = this.ccService.popupOpen$.subscribe(
      () => {
       this.ip.getIPAddress().subscribe((res:any)=>{  
        let consent = new CookieConsent();
        consent.userIP =res.ip;
        consent.date = null;
        this.ip.addConsent(consent).subscribe((res:any)=>{ 
        });
   });
  })
    this.statusChangeSubscription = this.ccService.statusChange$.subscribe(
      (event: NgcStatusChangeEvent) => {

        this.ip.getIPAddress().subscribe((res:any)=>{  
          let consent = new CookieConsent();
          consent.userIP = res.ip;
          consent.date = null;
          this.ip.addConsent(consent).subscribe((res:any)=>{ 
          })
   });
  })
}

  ngOnDestroy() {
    // unsubscribe to cookieconsent observables to prevent memory leaks
    this.popupOpenSubscription.unsubscribe();
    this.statusChangeSubscription.unsubscribe();
  }
  title = 'FirmyMichalowiceSPA';
}
