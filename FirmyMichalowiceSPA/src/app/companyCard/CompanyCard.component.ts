import { Component, Input, OnInit } from '@angular/core';
import { Company } from '../_models/company';

@Component({
  selector: 'app-CompanyCard',
  templateUrl: './CompanyCard.component.html',
  styleUrls: ['./CompanyCard.component.css']
})
export class CompanyCardComponent implements OnInit {
  @Input() company: Company;

  constructor(){ }

  ngOnInit() : void{
   
  }


}
