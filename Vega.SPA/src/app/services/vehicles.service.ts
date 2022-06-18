import { MakeModel } from 'src/app/models/MakeModel';
import { KeyValuePairModel } from './../models/KeyValuePairModel';
import { SaveVehicleModel } from './../models/SaveVehicleModel';
import { VehicleModel } from './../models/VehicleModel';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class VehiclesService {
  constructor(private http: HttpClient) {}
  getAllVehicles(query: any) {
    return this.http.get<any>(
      environment.baseUrl +
        'api/vehicles' +
        this.convertObjectToQueryString(query)
    );
  }
  getVehicleById(id: number) {
    return this.http.get<VehicleModel>(
      environment.baseUrl + 'api/vehicles/' + id
    );
  }
  create(vehicle: SaveVehicleModel) {
    return this.http.post(environment.baseUrl + 'api/vehicles', vehicle);
  }
  update(id: number, vehicle: SaveVehicleModel) {
    return this.http.put(environment.baseUrl + 'api/vehicles/' + id, vehicle);
  }
  delete(id: number) {
    return this.http.delete(environment.baseUrl + 'api/vehicles/' + id);
  }
  getMakes() {
    return this.http.get<MakeModel[]>(environment.baseUrl + 'api/makes');
  }

  getFeatures() {
    return this.http.get<KeyValuePairModel[]>(
      environment.baseUrl + 'api/features'
    );
  }

  private convertObjectToQueryString(query: any) {
    let parts = [];
    for (const key in query) {
      let value = query[key];
      if (value != undefined || value != null)
        parts.push(encodeURIComponent(key) + '=' + encodeURIComponent(value));
    }
    return parts.length ? '?' + parts.join('&') : '';
  }
}
