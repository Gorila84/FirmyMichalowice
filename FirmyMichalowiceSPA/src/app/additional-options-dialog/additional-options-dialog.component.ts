import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-additional-options-dialog',
  templateUrl: './additional-options-dialog.component.html',
  styleUrls: ['./additional-options-dialog.component.css']
})
export class AdditionalOptionsDialogComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<AdditionalOptionsDialogComponent>,
  ) { }

  ngOnInit(): void {
  }

  closeDialog(){
    this.dialogRef.close();
  }
}
