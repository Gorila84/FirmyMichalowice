import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot} from '@angular/router';

import {AlertifyService} from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Company } from '../_models/company';
import { CompanyService } from '../_services/company.service';
import { AuthService } from '../_services/auth.service';


@Injectable()
export class CompanyEditlResolver implements Resolve<Company>{

    constructor(private campanyService: CompanyService, 
                private router: Router, 
                private alertify: AlertifyService,
                private authService: AuthService){}

    resolve(route: ActivatedRouteSnapshot): Observable<Company> {
        return this.campanyService.getUser(this.authService.decotedToken.nameid).pipe(
            catchError(error => {
                this.alertify.error('Problem z pobraniem danych');
                this.router.navigate(['']);
                return of(null)
            }
                
        )
        )
    }
}

