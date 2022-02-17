import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-statute',
  templateUrl: './statute.component.html',
  styleUrls: ['./statute.component.css'],
})
export class StatuteComponent implements OnInit {
  constructor(private _location: Location) {}

  ngOnInit(): void {}
  backClicked() {
    this._location.back();
  }
}
