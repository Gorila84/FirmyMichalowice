import { Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { AdminComponent } from './admin/admin.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { CompanyDetailComponent } from './companyDetail/companyDetail.component';
import { CompanyEditComponent } from './companyEdit/companyEdit.component';
import { CompanyListComponent } from './companyList/companyList.component';
import { ContactComponent } from './contact/contact.component';
import { LoginComponent } from './login/login.component';
import { Polityka_prywatnosciComponent } from './polityka_prywatnosci/polityka_prywatnosci.component';
import { RegisterComponent } from './register/register.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { StatuteComponent } from './statute/statute.component';
import { PreventUnsavedChanges } from './_guard/prevent-unsaved-changes.guard';
import { CompanyDetailResolver } from './_resolvers/company_details_resolver';
import { CompanyEditlResolver } from './_resolvers/company_edit_resolver';
import { CompanyListResolver } from './_resolvers/company_list_resolver';



export const appRoutes: Routes = [
        {path: '', component: CompanyListComponent, resolve: {companies: CompanyListResolver}},
        {path: 'firma/:id', component: CompanyDetailComponent, resolve: {company: CompanyDetailResolver}},
        {path: 'logowanie', component: LoginComponent},
        {path: 'rodo', component: Polityka_prywatnosciComponent},
        {path: 'rejestracja', component: RegisterComponent},
        {path: 'edycja/:id', component: CompanyEditComponent,
                        resolve: {company: CompanyEditlResolver},
                        canDeactivate: [PreventUnsavedChanges]},
        {path: 'kontakt', component: ContactComponent},
        {path: 'rejestracja/zp', component: Polityka_prywatnosciComponent},
        {path: 'rejestracja/regulamin', component: StatuteComponent},
        {path: 'zmienHaslo', component: ChangePasswordComponent},
        {path: 'resetHasla', component: ResetPasswordComponent},
        {path: 'zp', component: Polityka_prywatnosciComponent},
        {path: 'regulamin', component: StatuteComponent},
        {path: 'onas', component: AboutComponent},
        {path: 'admin', component: AdminComponent}
    ];
