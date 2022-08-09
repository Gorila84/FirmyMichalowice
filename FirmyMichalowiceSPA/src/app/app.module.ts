import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  MatCheckboxDefaultOptions,
  MatCheckboxModule,
  MAT_CHECKBOX_DEFAULT_OPTIONS,
} from '@angular/material/checkbox';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSidenavModule } from '@angular/material/sidenav';
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
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { FooterComponent } from './footer/footer.component';
import { ContactComponent } from './contact/contact.component';

import { AccordionModule } from 'ngx-bootstrap/accordion';
import { MatMenuModule } from '@angular/material/menu';
import { Polityka_prywatnosciComponent } from './polityka_prywatnosci/polityka_prywatnosci.component';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RECAPTCHA_V3_SITE_KEY, RecaptchaV3Module } from 'ng-recaptcha';
import {
  NgcCookieConsentConfig,
  NgcCookieConsentModule,
} from 'ngx-cookieconsent';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { EditOfferDialogComponent } from './edit-offer-dialog/edit-offer-dialog.component';
import { StatuteComponent } from './statute/statute.component';
import { AboutComponent } from './about/about.component';
import { NgxPaginationModule } from 'ngx-pagination'; // <-- import the module
import { MunicipalitieResolver } from './_resolvers/municipality_list_resolver';
import { AdminComponent } from './admin/admin.component';
import { AdminMenuComponent } from './admin/admin-menu/admin-menu.component';
import { AdminUsersComponent } from './admin/admin-users/admin-users/admin-users.component';
import { AdminEditUserComponent } from './admin/admin-edit-user/admin-edit-user/admin-edit-user.component'; // <-- import the module

const cookieConfig: NgcCookieConsentConfig = {
  cookie: {
    //domain: 'localhost', // it is recommended to set your domain, for cookies to work properly
    domain: 'fp-krk.pl', // it is recommended to set your domain, for cookies to work properly
  },
  palette: {
    popup: {
      background: '#000',
    },
    button: {
      background: '#f1d600',
    },
  },
  theme: 'edgeless',
  type: 'opt-out',
  layout: 'my-custom-layout',
  layouts: {
    'my-custom-layout': '{{messagelink}}{{myCompliance}}',
  },
  elements: {
    messagelink: `
    <span id="cookieconsent:desc" class="cc-message">{{message}} 
    <a _ngcontent-xmk-c153="" ng-reflect-router-link="zp/#cookiesInfo" href="/#/zp/#cookiesInfo">{{cookiePolicyLink}}</a>,
    <a _ngcontent-xmk-c153="" ng-reflect-router-link="zp" href="/#/zp">Zasadami prywatności</a> oraz 
    <a _ngcontent-utu-c153="" ng-reflect-router-link="regulamin" href="/#/regulamin">{{statuteLink}} </a>
    </span>
    `,
    myCompliance: `<div class="btn-group btn-group-sm" role="group" aria-label="...">
    <a aria-label="allow cookies" role="button" tabindex="0" class="cc-btn cc-allow">Przejdź do FPK</a>
    </div>`,
  },
  content: {
    message: 'Używając tej aplikacji zgadzasz się z ',

    cookiePolicyLink: 'Polityką Cookie',
    cookiePolicyHref: '',

    privacyPolicyLink: 'Polityką Prywtaności',
    privacyPolicyHref: '',

    statuteLink: 'Regulaminem',
    statuteHref: '',
  },
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
    Polityka_prywatnosciComponent,
    ChangePasswordComponent,
    ResetPasswordComponent,
    EditOfferDialogComponent,
    StatuteComponent,
    AboutComponent,
    AdminComponent,
    AdminMenuComponent,
    AdminUsersComponent,
    AdminEditUserComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes, {
      relativeLinkResolution: 'legacy',
      useHash: true,
    }),
    HttpClientModule,
    BsDropdownModule.forRoot(),
    MatAutocompleteModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTableModule,
    MatPaginatorModule,
    PaginationModule.forRoot(),
    BrowserAnimationsModule,
    FileUploadModule,
    MatButtonModule,
    MatMenuModule,
    MatCardModule,
    MatCheckboxModule,
    MatDialogModule,
    MatSidenavModule,
    AccordionModule.forRoot(),
    CommonModule,
    // tslint:disable-next-line:max-line-length
    LoggerModule.forRoot({
      serverLoggingUrl: environment.apiUrl + 'logs/post',
      level: NgxLoggerLevel.DEBUG,
      serverLogLevel: NgxLoggerLevel.ERROR,
    }),
    // FontAwesomeModule
    MatTabsModule,
    MatTooltipModule,
    RecaptchaV3Module,
    NgcCookieConsentModule.forRoot(cookieConfig),
    NgxPaginationModule,
  ],
  providers: [
    CompanyListResolver,
    CompanyDetailResolver,
    CompanyEditlResolver,
    MunicipalitieResolver,
    PreventUnsavedChanges,
    {
      provide: MAT_CHECKBOX_DEFAULT_OPTIONS,
      useValue: { clickAction: 'noop' } as MatCheckboxDefaultOptions,
    },
    { provide: RECAPTCHA_V3_SITE_KEY, useValue: environment.recaptcha.siteKey },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
