import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { Company } from "../_models/company";
import { AlertifyService } from "../_services/alertify.service";
import { CompanyService } from "../_services/company.service";
import { catchError } from 'rxjs/operators';

@Injectable()
export class CompanyListResolver implements Resolve<Company[]>{

    constructor(private companyService: CompanyService, 
                private router: Router, 
                private alertify: AlertifyService){}

    resolve(route: ActivatedRouteSnapshot): Observable<Company[]> {
        return this.companyService.getUsers().pipe(
            catchError(error => {
                this.alertify.error('Problem z pobraniem danych');
                this.router.navigate(['']);
                return of(null)
            }
                
        )
        )
    }
}