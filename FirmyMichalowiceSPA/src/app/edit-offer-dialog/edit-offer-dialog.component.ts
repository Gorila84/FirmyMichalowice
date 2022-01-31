import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CompanyEditComponent } from '../companyEdit/companyEdit.component';
import { Offer } from '../_models/offer';
import { AlertifyService } from '../_services/alertify.service';
import { CompanyService } from '../_services/company.service';

@Component({
  selector: 'app-edit-offer-dialog',
  templateUrl: './edit-offer-dialog.component.html',
  styleUrls: ['./edit-offer-dialog.component.css']
})
export class EditOfferDialogComponent implements OnInit {



  constructor(public dialogRef: MatDialogRef<EditOfferDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Offer, 
            private companyService: CompanyService,
            private alertifyService: AlertifyService) {}


  ngOnInit(): void {
      
  }

  editOffer(){
    this.companyService.editOffer(this.data.id, this.data).subscribe(next => {
      this.alertifyService.success("Twoja oferta " + this.data.name + " została zmieniona.")
    }, error => {this.alertifyService.error(error)});
    this.closeEditDialog();
  }
  closeEditDialog(): void {
    this.dialogRef.close();
  }

}
