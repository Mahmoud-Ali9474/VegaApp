import { environment } from './../../../environments/environment';
import { VehiclePhotosService } from './../../services/vehicle-Photos.service';
import { ActivatedRoute } from '@angular/router';
import { VehiclesService } from 'src/app/services/vehicles.service';
import { VehicleModel } from './../../models/VehicleModel';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.css'],
})
export class VehicleComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef<HTMLInputElement> | undefined;
  currentTab: string = 'vehicle';
  baseUrl: string = environment.baseUrl + 'uploads/';
  photos: any[] = [];
  vehicle: VehicleModel = {
    id: 0,
    model: { id: 0, name: '' },
    make: { id: 0, name: '' },
    isRegistered: false,
    features: [],
    contact: { name: '', email: '', phone: '' },
    lastUpdate: new Date(),
  };
  constructor(
    private vehicleService: VehiclesService,
    private route: ActivatedRoute,
    private photosService: VehiclePhotosService
  ) {}

  ngOnInit() {
    this.vehicle.id = +(this.route.snapshot.paramMap.get('id') || 0);
    this.vehicleService
      .getVehicleById(this.vehicle.id)
      .subscribe((res) => (this.vehicle = res));
    this.photosService
      .getPhotos(this.vehicle.id)
      .subscribe((res: any) => (this.photos = res));
  }
  uploadPhoto() {
    var photo = this.fileInput?.nativeElement.files?.item(0);
    console.log(photo);
    this.photosService.uploadPhoto(this.vehicle.id, photo).subscribe((res) => {
      this.photos.push(res);
      this.fileInput!.nativeElement.value = '';
    });
  }
}
