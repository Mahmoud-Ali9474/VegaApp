import { Component, OnInit } from '@angular/core';
import { VehiclesService } from 'src/app/services/vehicles.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css'],
})
export class VehicleFormComponent implements OnInit {
  makes: any[] = [];
  models: any[] = [];
  features: any[] = [];
  makeId: any;
  constructor(private vehicleService: VehiclesService) {}

  ngOnInit(): void {
    this.vehicleService.getMakes().subscribe((res) => (this.makes = res));
    this.vehicleService.getFeatures().subscribe((res) => (this.features = res));
  }
  onMakeChange() {
    console.log(this.makeId);
    let selectedMake = this.makes.find((m) => m.id == this.makeId);
    this.models = selectedMake?.models || [];
  }
}
