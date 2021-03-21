import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
// import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';


import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { CompanyCardComponent } from './companyCard/CompanyCard.component';
import { CompanyListComponent } from './companyList/companyList.component';
import { RouterModule, Routes  } from '@angular/router';
import { appRoutes } from './routs';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CompanyDetailComponent } from './companyDetail/companyDetail.component';
import { CompanyListResolver } from './_resolvers/company_list_resolver';
import { CompanyDetailResolver } from './_resolvers/company_details_resolver';
import { CompanyEditComponent } from './companyEdit/companyEdit.component';


@NgModule({
  declarations: [							
    AppComponent,
      NavComponent,
      LoginComponent,
      RegisterComponent,
      CompanyCardComponent,
      CompanyListComponent,
      CompanyDetailComponent,
      CompanyEditComponent
   ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    
    // FontAwesomeModule
  ],
  providers: [
    CompanyListResolver,
    CompanyDetailResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
