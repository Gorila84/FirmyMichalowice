import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Offer } from '../_models/offer';

@Component({
  selector: 'app-edit-offer-dialog',
  templateUrl: './edit-offer-dialog.component.html',
  styleUrls: ['./edit-offer-dialog.component.css']
})
export class EditOfferDialogComponent implements OnInit {



  constructor(public dialogRef: MatDialogRef<EditOfferDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Offer) {}


  ngOnInit(): void {
  }

}
