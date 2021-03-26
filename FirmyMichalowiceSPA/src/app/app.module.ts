import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes  } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';

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
    FormsModule
    // FontAwesomeModule
  ],
  providers: [
    CompanyListResolver,
    CompanyDetailResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
