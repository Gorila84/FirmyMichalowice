import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Component({
  selector: 'app-google-map',
  templateUrl: './google-map.component.html',
  styleUrls: ['./google-map.component.css'],
})
export class GoogleMapComponent implements OnInit {
  options: google.maps.MapOptions;
  markers: any;
  @Input() item?: any;
  @Input() companyName?: any;

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {
    this.options = {
      center: { lat: this.item.location.lat, lng: this.item.location.lng },
      zoom: 15,
    };
    this.markers = [
      {
        position: {
          lat: this.item.location.lat,
          lng: this.item.location.lng,
        },
        visible: false,
        opacity: 0.1,

        title: this.companyName,
        options: {
          animation: google.maps.Animation.BOUNCE,
          //icon: 'https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png',
        },
      },
    ];
  }
}
