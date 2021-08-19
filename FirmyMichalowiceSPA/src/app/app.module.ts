import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes  } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


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
   ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    BsDropdownModule.forRoot(),
    MatAutocompleteModule,
    MatFormFieldModule,
    MatInputModule,
    BrowserAnimationsModule,
    FileUploadModule,
    // tslint:disable-next-line:max-line-length
    LoggerModule.forRoot({serverLoggingUrl: environment.apiUrl + 'logs/post', level: NgxLoggerLevel.DEBUG, serverLogLevel: NgxLoggerLevel.ERROR})
    // FontAwesomeModule
  ],
  providers: [
    CompanyListResolver,
    CompanyDetailResolver,
    CompanyEditlResolver,
    PreventUnsavedChanges,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
