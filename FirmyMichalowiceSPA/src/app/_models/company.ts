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
        municipalitie: string;
        armsUrl: string;
}

export interface PKD {
        symbol: string;
        nazwa: string;
}

