import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { error } from 'console';
import { SettingsTemplate } from '../_models/settingsTemplate';
import { AlertifyService } from '../_services/alertify.service';
import { SettingsService } from '../_services/settings.service';
import { MatProgressBarModule } from '@angular/material/progress-bar';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css'],
})
export class ShopComponent implements OnInit {
  displayedColumns: string[] = [
    'position',
    'description',
    'lengthService',
    'cost',
    'add',
  ];
  dataSource: any;
  checked = false;
  @Input() user: any; // decorate the property with @Input() user: any;

  shopData: SettingsTemplate[] = [];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private settingservice: SettingsService,
    private alertify: AlertifyService
  ) {
    this.settingservice.getAllSettings().subscribe(
      (data) => {
        this.dataSource = data;
        this.dataSource.paginator = this.paginator;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  ngOnInit(): void {}

  removeItem(element: SettingsTemplate) {
    debugger;
    this.shopData.push(element);
    this.alertify.success('Dodano element do koszyka.');
    (error) => {
      this.alertify.error(
        'Nie usunięto element. Spróbuj jeszcze raz.' + error.error
      );
    };
  }
}
