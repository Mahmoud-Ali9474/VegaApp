import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class VehiclePhotosService {
  constructor(private http: HttpClient) {}
  getPhotos(vehicleId: any): any {
    return this.http.get(
      environment.baseUrl + `api/vehicles/${vehicleId}/photos`
    );
  }
  uploadPhoto(vehicleId: any, photo: any) {
    var form = new FormData();
    form.append('file', photo);
    return this.http.post(
      environment.baseUrl + `api/vehicles/${vehicleId}/upload`,
      form
    );
  }
}
