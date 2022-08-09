import { Offer } from './offer';
import { Photo } from './photo';

export interface Company {
  id: number;
  username: string;
  companyName: string;
  shortDescription: string;
  longDescription: string;
  phoneNumber: string;
  webSite: string;
  emailAddress: string;
  city: string;
  street: string;
  postalCode: string;
  photoUrl: string;
  companyType: string;
  photo: Photo;
  nip: string;
  mainPKD: PKD;
  pkds: PKD[];
  geolocationUrl: string;
  geolocation2Url: string;
  municipalitie: string;
  armsUrl: string;
  officeCity: string;
  officeStreet: string;
  officePostalCode: string;
  officeMunicipalitie: string;
  statusFromCeidg: string;
  isAdmin: boolean;
  isActive: boolean;
  additionalAddress: boolean;
  created?: Date;
  modify?: Date;
  offers: Offer[];
  geometries: GoggleMapsGeometry[];
  userSettings: CompanySettings;
}

export interface PKD {
  symbol: string;
  nazwa: string;
}

export interface GoggleMapsGeometry {
  type: number;
  lat: any;
  lng: any;
}

export interface CompanySettings {
  settingsTemplateId: number;
  subscriptionEndDate: Date;
}
