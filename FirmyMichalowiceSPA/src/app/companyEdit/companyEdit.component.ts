import {
  Component,
  EventEmitter,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { Form, FormControl, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Company } from '../_models/company';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { CompanyService } from '../_services/company.service';
import { UploadPhotoService } from '../_services/uploadPhoto.service';
import { environment } from 'src/environments/environment';
import { HttpEventType } from '@angular/common/http';
import { NGXLogger } from 'ngx-logger';
import { map, startWith } from 'rxjs/operators';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { buffer } from 'rxjs/operators';
import { CompanyTypeService } from '../_services/companyType.service';
import { toBase64String } from '@angular/compiler/src/output/source_map';
import { OffsetModifier } from '@popperjs/core/lib/modifiers/offset';
import { Offer } from '../_models/offer';
import { MatPaginator } from '@angular/material/paginator';

import { EditOfferDialogComponent } from '../edit-offer-dialog/edit-offer-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { AdditionalOptionsDialogComponent } from '../additional-options-dialog/additional-options-dialog.component';

interface Item {
  value: string;
  viewValue: string;
}

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-companyEdit',
  templateUrl: './companyEdit.component.html',
  styleUrls: ['./companyEdit.component.css'],
})
export class CompanyEditComponent implements OnInit {
  myControl = new FormControl();
  options: string[] = [];
  displayedColumns: string[] = ['name', 'price', 'buttons'];
  rowofferItems: Offer[];
  filteredOptions: Observable<string[]>;
  // tslint:disable-next-line:no-output-on-prefix
  @Output() public onUploadFinished = new EventEmitter();
  @ViewChild(MatPaginator) paginator: MatPaginator;
  company: Company;
  offer: Offer[];
  model: any = {};
  offers: any;
  baseUrl = environment.apiUrl;
  fileToUpload: File | null = null;
  shown: any;
  dataSource: any;
  trade: FormControl;

  @ViewChild('editForm') editForm: NgForm;

  constructor(
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private authService: AuthService,
    private companyService: CompanyService,
    private uploadPhotoService: UploadPhotoService,
    private logger: NGXLogger,
    private companyTypeService: CompanyTypeService,
    public dialog: MatDialog
  ) {}

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this. openAdditionalSettings();
    this.getCompanyTypes();
    this.route.data.subscribe((data) => {
      this.company = data.company;
      this.offers = data.company.offers;
      this.trade = new FormControl(this.company.companyType);
    });
    // this.getImage(this.authService.decotedToken.nameid);
    // this.getImage();

    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map((value) => this._filter(value))
    );
    this.dataSource = this.getOffers().subscribe((data) => {
      this.dataSource = data;
    });

    // this.dataSource = new MatTableDataSource (this.getOffers().subscribe(data =>
    //                                           this.dataSource = data));
    this.dataSource.paginator = this.paginator;
  }

  offerNameFormControl = new FormControl('', [Validators.required]);

  // tslint:disable-next-line:typedef
  getImage(result: string, extenssion: string) {
    return result.slice(22, result.length);
  }

  // tslint:disable-next-line:typedef
  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }
  // tslint:disable-next-line:typedef
  updateCompany() {
    this.companyService
      .updateCompany(this.authService.decotedToken.nameid, this.company)
      .subscribe(
        (next) => {
          this.alertify.success('Twoje dane zostały pomyślnie zaktualizowane.');
          this.editForm.reset(this.company);
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }
  // tslint:disable-next-line:typedef
  uploadFile = (files) => {
    const fileToUpload = files[0] as File;
    let allowedExtrensions = ['image/png', 'image/jpeg', 'image/jpg'];

    console.log(fileToUpload.size);
    if (files.length === 0) {
      return;
    } else if (fileToUpload.size > 2500000) {
      this.alertify.error(
        'Błąd. Rozmiar obrazka nie może być wiekszy niż 2,5 MB.'
      );
      return;
    } else if (!allowedExtrensions.includes(fileToUpload.type)) {
      alert(fileToUpload.type);
      this.alertify.error(
        'Błąd. Niedozwolone rozszerzenie pliku. Użyj PNG, JPG lub JPEG'
      );
      return;
    }

    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.uploadPhotoService
      .uploadImage(this.authService.decotedToken.nameid, fileToUpload)
      .subscribe(
        async (event) => {
          if (event.type === HttpEventType.UploadProgress) {
          } else if (event.type === HttpEventType.Response) {
            this.alertify.success('Dodano logo.');
            // this.getImage();
            // window.location.reload();
            const fileReader = new FileReader();
            fileReader.onload = (e) => {
              this.company.photo.fileData = this.getImage(
                fileReader.result.toString(),
                fileToUpload.type
              ) as any;
            };
            fileReader.readAsDataURL(files[0]);
          }
        },
        (err) => {
          const userId = this.authService.decotedToken.nameid;
          err.userId = userId;
          err.componentName = this.constructor.name;
          //delete err['headers'];
          console.log(err);
          this.logger.error(err);
          this.alertify.error('Błąd. Nie udało się wysłać pliku');
        }
      );
  };

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.options.filter((option) =>
      option.toLowerCase().includes(filterValue)
    );
  }

  // tslint:disable-next-line:typedef
  getCompanyTypes() {
    this.companyTypeService.getCompanyTypes().subscribe(
      (data) => {
        data.forEach((item) => {
          this.options.push(item.toLocaleUpperCase());
        });
      },
      (error) => {
        this.alertify.error(error);
        console.log(error);
      }
    );
  }
  getCompanyData() {
    this.companyService.getDataFromCEIDG(this.company.nip).subscribe(
      (data) => {
        this.alertify.success('Twoje dane zostały pomyślnie pobrane.');
        let myObj = JSON.parse(JSON.stringify(data));
        console.log(myObj);
        this.company.companyName = myObj.nazwa;
        this.company.postalCode = myObj.adresDzialanosci.kod;
        this.company.street =
          myObj.adresDzialanosci.ulica + ' ' + myObj.adresDzialanosci.budynek;
        this.company.city = myObj.adresDzialanosci.miasto;
        this.company.nip = myObj.wlasciciel.nip;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  getOffers() {
    const rowoferItems = this.companyService.getOffers(
      this.authService.decotedToken.nameid
    );
    return rowoferItems;
  }

  addOffer() {
    this.model.userId = this.authService.decotedToken.nameid;
    this.companyService.addOffer(this.model).subscribe((data) => {
      debugger;
      this.refreshTable();
    });
  }

  removeOffer(id: number) {
    this.companyService.removeOffer(id).subscribe((data) => {
      debugger;
      this.refreshTable();
    });
  }

  openDialog(id: number, name: string, price: number): void {
    const dialogRef = this.dialog.open(EditOfferDialogComponent, {
      width: '250px',
      data: { id: id, name: name, price: price },
    });
    const sub = dialogRef.componentInstance.refreshTable.subscribe(() => {
      this.refreshTable();
    });
  }

  openAdditionalSettings():void{
    const additionalSettingsDialog = this.dialog.open(AdditionalOptionsDialogComponent,{
      width: '900px',
      height: '350px'

    })
  }

  refreshTable() {
    this.companyService
      .getUser(this.authService.decotedToken.nameid, true)
      .subscribe((data) => {
        this.company = data;
      });
  }
}
