import { Component, OnInit } from '@angular/core';
import {Location} from '@angular/common';

@Component({
  selector: 'app-polityka_prywatnosci',
  templateUrl: './polityka_prywatnosci.component.html',
  styleUrls: ['./polityka_prywatnosci.component.css']
})
export class Polityka_prywatnosciComponent implements OnInit {

  constructor(private _location: Location) 
  {}
  ngOnInit() {
 
  }

  backClicked() {
    this._location.back();
  }

}
