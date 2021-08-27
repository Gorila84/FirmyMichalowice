import { Component, Input, OnInit } from '@angular/core';
import { Company } from '../_models/company';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-CompanyCard',
  templateUrl: './CompanyCard.component.html',
  styleUrls: ['./CompanyCard.component.css']
})
export class CompanyCardComponent implements OnInit {
  @Input() company: Company;
  imageToShow: any;
  noImageFound: boolean;

  constructor(){
   }


// tslint:disable-next-line:typedef
ngOnInit() {

}

}


