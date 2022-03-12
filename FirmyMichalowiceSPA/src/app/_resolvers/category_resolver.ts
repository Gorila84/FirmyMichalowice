import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { AlertifyService } from '../_services/alertify.service';
import { CompanyService } from '../_services/company.service';
import { catchError } from 'rxjs/operators';
import { Municipalitie } from '../_models/municipalitie';
import { Category } from '../_models/category';

@Injectable()
export class CategoryResolver implements Resolve<Category[]> {
  constructor(
    private companyService: CompanyService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Category[]> {
    return this.companyService.getCategories().pipe(
      catchError((error) => {
        this.alertify.error('Problem z pobraniem danych');
        this.router.navigate(['']);
        return of(null);
      })
    );
  }
}
