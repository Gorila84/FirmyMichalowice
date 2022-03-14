import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
//import * as ILITEAPI from '../imapLiteApi-core';
import { Company } from '../_models/company';
import { CompanyService } from '../_services/company.service';
import { HostListener } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { DomSanitizer, SafeStyle } from '@angular/platform-browser';
import { Offer } from '../_models/offer';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

declare var $: any;

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-companyDetail',
  templateUrl: './companyDetail.component.html',
  styleUrls: ['./companyDetail.component.css'],
})
export class CompanyDetailComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;

  company: Company;
  isCompanyActive: boolean;
  isEnabledGeolocation2Url: boolean;
  id: number;
  showArms: boolean;
  useGoggleMaps: boolean = false;
  isConsentToUseMA: boolean;
  useGeoportal = environment.useGeoportal;
  scrHeight: any;
  scrWidth: any;
  dataSource: MatTableDataSource<any>;
  dataFromApi: any;
  displayedColumns: string[] = ['name', 'price'];
  offer: Offer[] = [];
  p: number = 1;
  collection: any[];
  apiLoaded: Observable<boolean>;
  showMaps: boolean;

  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.scrHeight = window.innerHeight;
    this.scrWidth = window.innerWidth;
  }

  constructor(
    private route: ActivatedRoute,
    private companyService: CompanyService,
    private authService: AuthService,
    private httpClient: HttpClient
  ) {
    this.getScreenSize();
  }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.company = data.company;
    });
    this.isCompanyActive = this.company.statusFromCeidg == 'AKTYWNY';
    let isNull = false;
    if (this.company.geolocation2Url == null) {
      isNull = true;
    }
    isNull
      ? (this.isEnabledGeolocation2Url = false)
      : (this.isEnabledGeolocation2Url = true);
    this.showArms = environment.showArms && this.company.armsUrl ? true : false;
    this.dataSource = new MatTableDataSource(this.company.offers);
    this.collection = this.company.pkds;
    this.useGoggleMaps =
      this.company.geometries != undefined && this.company.geometries != [];
    this.showMaps = this.company.city && this.company.street ? true : false;
  }

  ngAfterViewInit(): void {
    // if(this.useGeoportal)
    // this.initMap(0, this.company)
    this.dataSource.paginator = this.paginator;
    $('.ngx-pagination').css('padding-left', 0);
    $('.pagination-previous').html('Poprzednie');
    $('.pagination-next').html('Następne');
  }

  showMapFn($event) {
    // if(this.useGeoportal)
    // this.initMap($event.index, this.company)
  }

  //  initMap(index:number, company: Company) {
  //   const body = document.getElementsByTagName("BODY")[0];
  //   const mapDiv = document.getElementById("iapi");

  //   let mapWidth = 500;
  //   if(this.scrWidth < 600) mapWidth = this.scrWidth - 60;

  //   ILITEAPI.init(
  //     {
  //       divId: "iapi",
  //       width: mapWidth,
  //       height: 300,
  //       activeGpMapId: "gp0",
  //       activeGpMaps: ["gp0"],
  //       activeGpActions: ["pan", "fullExtent"],
  //       useMenu: false,
  //       scale: 15,
  //       /*'marker' : {
  //     'x' : 591920,
  //     'y' : 259048,
  //     'scale':1000,
  //     'opts' : { 'title' : '', 'content' : '', show: false }
  //   }*/
  //     },
  //     this.onInitMap(index, company)
  //   );
  // }
  //  onInitMap(idx: Number, company: Company ) {
  //   if (searchAdr){
  //   const addr = searchAdr(idx, company)
  //   const opt = adrOpt(idx, company)
  //   ILITEAPI.searchAddress(addr, opt)
  // }
  // }

  getIfAdditionalAddressIsTrue() {
    return this.company.additionalAddress;
  }

  getOffers() {
    const rowoferItems = this.companyService.getOffers(this.company.id);
    return rowoferItems;
  }
}
// function searchAdr(idx: Number, company: Company) {
//   if( idx == 0) return company.city + ', ' + company.street;
//  else if (idx == 1) return company.officeCity + ', ' +  company.officeStreet
//  else return company.city + ', ' + company.street;
// }

// function adrOpt(idx: Number, company: Company) {
// const json = {
//   title: company.companyName,
//   content: company.city + ', ' + company.street,
//   show: true,
// };
// const json2 = {
//   title: company.companyName,
//   content: company.officeCity + ', ' +  company.officeStreet,
//   show: true,
// };
// if( idx == 0 ) return json;
// else if (idx == 1 ) return json2;
// else return json;
// }
