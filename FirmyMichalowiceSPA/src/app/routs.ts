import { Routes } from '@angular/router';
import { CompanyCardComponent } from './companyCard/CompanyCard.component';
import { CompanyDetailComponent } from './companyDetail/companyDetail.component';
import { CompanyListComponent } from './companyList/companyList.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { CompanyDetailResolver } from './_resolvers/company_details_resolver';
import { CompanyListResolver } from './_resolvers/company_list_resolver';


export const appRoutes: Routes = [
    
  
        {path: '', component: CompanyListComponent, resolve:{companies: CompanyListResolver}},
        {path: 'firma/:id', component: CompanyDetailComponent, resolve:{company: CompanyDetailResolver}},
        {path: 'logowanie', component: LoginComponent},
        {path: 'rejstracja', component: RegisterComponent}
        // {path: 'wiadomosci', component: MessagesComponent},
    ];