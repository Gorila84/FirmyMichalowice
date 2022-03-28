import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MatCheckboxChange, MatCheckboxModule } from '@angular/material/checkbox';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { Company } from 'src/app/_models/company';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-admin-edit-user',
  templateUrl: './admin-edit-user.component.html',
  styleUrls: ['./admin-edit-user.component.css']
})
export class AdminEditUserComponent implements OnInit {
  @Output() refreshTable: EventEmitter<boolean> = new EventEmitter();
  checked:boolean;

  constructor(public dialogRef: MatDialogRef<AdminEditUserComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Company,
    private adminService: AdminService,
    private alertifyService: AlertifyService) { }

  ngOnInit(): void {
  }

  editUserForAdmin() {
    this.adminService.editUserForAdmin(this.data.id, this.data).subscribe(
      (next) => {
        this.alertifyService.success(
          'Twoja oferta ' + this.data.companyName + ' została zmieniona.'
        );
        this.refreshTable.emit(true);
      },
      (error) => {
        this.alertifyService.error(error);
      }
    );
    debugger
    this.closeEditDialog();
  }
  closeEditDialog(): void {
    this.dialogRef.close();
  }
  change(){
    
    this.checked = (this.checked)?false:true;

    }

}
