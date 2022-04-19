import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class VehiclesService {
  constructor(private http: HttpClient) {}

  getMakes() {
    return this.http.get<any[]>(environment.baseUrl + 'api/makes');
  }

  getFeatures() {
    return this.http.get<any[]>(environment.baseUrl + 'api/features');
  }
}
