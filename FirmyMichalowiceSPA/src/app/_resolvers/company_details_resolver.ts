import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  Router,
  RouterStateSnapshot,
} from '@angular/router';

import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Company } from '../_models/company';
import { CompanyService } from '../_services/company.service';

@Injectable()
export class CompanyDetailResolver implements Resolve<Company> {
  constructor(
    private campanyService: CompanyService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Company> {
    return this.campanyService.getUser(route.params.id).pipe(
      catchError((error) => {
        this.alertify.error('Problem z pobraniem danych');
        this.router.navigate(['/uzytkownicy']);
        return of(null);
      })
    );
  }
}
