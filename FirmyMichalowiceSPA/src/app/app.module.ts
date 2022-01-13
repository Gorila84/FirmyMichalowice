import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule  } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatCheckboxDefaultOptions, MatCheckboxModule, MAT_CHECKBOX_DEFAULT_OPTIONS} from '@angular/material/checkbox';
import { CommonModule } from "@angular/common";

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { CompanyCardComponent } from './companyCard/CompanyCard.component';
import { CompanyListComponent } from './companyList/companyList.component';
import { appRoutes } from './routs';
import { CompanyDetailComponent } from './companyDetail/companyDetail.component';
import { CompanyListResolver } from './_resolvers/company_list_resolver';
import { CompanyDetailResolver } from './_resolvers/company_details_resolver';
import { CompanyEditComponent } from './companyEdit/companyEdit.component';
import { CompanyEditlResolver } from './_resolvers/company_edit_resolver';
import { PreventUnsavedChanges } from './_guard/prevent-unsaved-changes.guard';
import { FileUploadModule } from 'ng2-file-upload';
import { LoggerModule, NgxLoggerLevel } from 'ngx-logger';
import { environment } from 'src/environments/environment';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import {MatSelectModule} from '@angular/material/select';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { FooterComponent } from './footer/footer.component';
import { ContactComponent } from './contact/contact.component';

import { AccordionModule } from 'ngx-bootstrap/accordion';
import { MatMenuModule} from '@angular/material/menu';
import { Polityka_prywatnosciComponent } from './polityka_prywatnosci/polityka_prywatnosci.component';
import {MatTabsModule} from '@angular/material/tabs';
import {MatTooltipModule} from '@angular/material/tooltip';
import { RECAPTCHA_V3_SITE_KEY, RecaptchaV3Module } from 'ng-recaptcha';
import {NgcCookieConsentConfig, NgcCookieConsentModule} from 'ngx-cookieconsent';


const cookieConfig:NgcCookieConsentConfig = {
  cookie: {
    domain: 'localhost'// it is recommended to set your domain, for cookies to work properly
  },
  palette: {
    popup: {
      background: '#000'
    },
    button: {
      background: '#f1d600'
    }
  },
  theme: 'edgeless',
  type: 'opt-out',
  layout: 'my-custom-layout',
  layouts: {
    "my-custom-layout": '{{messagelink}}{{myCompliance}}'
  },
  elements:{
    messagelink: `
    <span id="cookieconsent:desc" class="cc-message">{{message}} 
      <a aria-label="learn more about cookies" tabindex="0" class="cc-link" href="{{cookiePolicyHref}}" target="_blank" rel="noopener">{{cookiePolicyLink}}</a>, 
      <a aria-label="learn more about our privacy policy" tabindex="1" class="cc-link"  href="{{privacyPolicyHref}}" target="_blank" rel="noopener">{{privacyPolicyLink}}</a> oraz 
      <a aria-label="learn more about our terms of service" tabindex="2" class="cc-link" [routerLink]="['zp']" target="_blank" rel="noopener">{{tosLink}}</a>
    </span>
    `,
    myCompliance : `<div class="btn-group btn-group-sm" role="group" aria-label="...">
    <a aria-label="deny cookies" role="button" tabindex="0" class="cc-btn cc-deny" style="background-color: black; color: white">Odrzuć</a>
    <a aria-label="allow cookies" role="button" tabindex="0" class="cc-btn cc-allow">Zaakceptuj</a>
    </div>`
  },
  content:{
    message: 'Używając tej aplikacji zgadzasz się z ',
    
    cookiePolicyLink: 'Polityka Cookie',
    cookiePolicyHref: '#',

    privacyPolicyLink: 'Polityka Prywtaności',
    privacyPolicyHref: '/rejestracja/zp',

    tosLink: 'Regulaminem',
    tosHref: '#',
  }
};

@NgModule({
  declarations: [		
    AppComponent,
      NavComponent,
      LoginComponent,
      RegisterComponent,
      CompanyCardComponent,
      CompanyListComponent,
      CompanyDetailComponent,
      CompanyEditComponent,
      FooterComponent,
      ContactComponent,
      Polityka_prywatnosciComponent
   ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes, { relativeLinkResolution: 'legacy' }),
    HttpClientModule,
    BsDropdownModule.forRoot(),
    MatAutocompleteModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    PaginationModule.forRoot(),
    BrowserAnimationsModule,
    FileUploadModule,
    MatButtonModule,
    MatMenuModule,
    MatCardModule,
    MatCheckboxModule,
    AccordionModule.forRoot(),
    CommonModule,
    // tslint:disable-next-line:max-line-length
    LoggerModule.forRoot({serverLoggingUrl: environment.apiUrl + 'logs/post', level: NgxLoggerLevel.DEBUG, serverLogLevel: NgxLoggerLevel.ERROR}),
    // FontAwesomeModule
    MatTabsModule,
    MatTooltipModule,
    RecaptchaV3Module,
    NgcCookieConsentModule.forRoot(cookieConfig),
  ],
  providers: [
    CompanyListResolver,
    CompanyDetailResolver,
    CompanyEditlResolver,
    PreventUnsavedChanges,
    {provide: MAT_CHECKBOX_DEFAULT_OPTIONS, useValue: { clickAction: 'noop' } as MatCheckboxDefaultOptions},
    { provide: RECAPTCHA_V3_SITE_KEY, 
      useValue: environment.recaptcha.siteKey
     }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
