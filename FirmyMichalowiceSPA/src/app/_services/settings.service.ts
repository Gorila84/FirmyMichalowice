import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ApiClient } from '../helpers/apiClient';
import { SettingsTemplate } from '../_models/settingsTemplate';

@Injectable({
  providedIn: 'root',
})
export class SettingsService {
  baseUrl = environment.apiUrl + 'AdditionalSettings/';

  constructor(private httpClient: HttpClient, private client: ApiClient) {}

  getAllSettings() {
    const headers = this.client.addBearer();
    return this.httpClient.get<SettingsTemplate[]>(
      this.baseUrl + 'GetAllSettings',
      { headers }
    );
  }
}
