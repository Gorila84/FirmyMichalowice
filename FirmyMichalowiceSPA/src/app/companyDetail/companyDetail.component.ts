import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
//import * as ILITEAPI from '../imapLiteApi-core';
import { Company } from '../_models/company';
import { CompanyService } from '../_services/company.service';
import { HostListener } from "@angular/core";
import { AuthService } from '../_services/auth.service';

declare var $: any;

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-companyDetail',
  templateUrl: './companyDetail.component.html',
  styleUrls: ['./companyDetail.component.css']
})
export class CompanyDetailComponent implements OnInit, AfterViewInit   {
  company: Company;
  isCompanyActive: boolean;
  isEnabledGeolocation2Url: boolean;
  id: number;
  showArms: boolean; 
  isConsentToUseMA: boolean;
  useGeoportal = environment.useGeoportal;
  scrHeight:any;
  scrWidth:any;
  dataSource : any;
  displayedColumns: string[] = ['name', 'price'];
  @HostListener('window:resize', ['$event'])
    getScreenSize(event?) {
          this.scrHeight = window.innerHeight;
          this.scrWidth = window.innerWidth;
    }

  constructor(private route: ActivatedRoute,
              private companyService: CompanyService, 
              private authService: AuthService
             ) { 
              this.getScreenSize();
             }
  ngAfterViewInit(): void {
    // if(this.useGeoportal)
    // this.initMap(0, this.company)
  }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.company = data.company;
    });
    this.isCompanyActive = this.company.statusFromCeidg == 'AKTYWNY';
    this.isEnabledGeolocation2Url = this.company.geolocation2Url.length == 0;
    this.showArms =  environment.showArms && this.company.armsUrl ? true : false ;

    this.dataSource =  this.getOffers().subscribe(data =>{
      this.dataSource = data
    } );

  }
  showMapFn($event){
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

  getIfAdditionalAddressIsTrue(){
    return this.company.additionalAddress;
  }
  
  getOffers(){
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



