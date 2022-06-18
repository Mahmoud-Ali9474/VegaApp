import { KeyValuePairModel } from './../../models/KeyValuePairModel';
import { VehicleModel } from './../../models/VehicleModel';
import { SaveVehicleModel } from './../../models/SaveVehicleModel';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { VehiclesService } from 'src/app/services/vehicles.service';
import { forkJoin, from } from 'rxjs';
import { MakeModel } from 'src/app/models/MakeModel';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css'],
})
export class VehicleFormComponent implements OnInit {
  makes: MakeModel[] = [];
  models: KeyValuePairModel[] = [];
  features: KeyValuePairModel[] = [];
  vehicle: SaveVehicleModel = {
    id: 0,
    modelId: undefined,
    makeId: undefined,
    isRegistered: false,
    features: [],
    contact: { name: '', email: '', phone: '' },
  };
  vehicleId: number = 0;
  constructor(
    private toastrService: ToastrService,
    private vehicleService: VehiclesService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    let source: any = {
      makes: this.vehicleService.getMakes(),
      features: this.vehicleService.getFeatures(),
    };

    this.vehicleId = +(this.route.snapshot.paramMap.get('id') || 0);
    if (this.vehicleId)
      source.vehicle = this.vehicleService.getVehicleById(this.vehicleId);

    forkJoin(source).subscribe((data: any) => {
      this.makes = data.makes;
      this.features = data.features;
      if (this.vehicleId) this.setVehicle(data.vehicle);
      if (this.vehicle.makeId) this.getModelsOfMake();
    });
  }
  onMakeChange() {
    this.getModelsOfMake();
    this.vehicle.modelId = undefined;
  }
  private getModelsOfMake() {
    let selectedMake = this.makes.find((m) => m.id == this.vehicle.makeId);
    this.models = selectedMake?.models || [];
  }

  onFeatureToggle(feature: any, $event: any) {
    let isSelected = $event.target.checked;
    if (isSelected) this.vehicle.features.push(feature.id);
    else {
      let index = this.vehicle.features.indexOf(feature.id);
      this.vehicle.features.splice(index, 1);
    }
  }
  save(form: NgForm) {
    let obs = this.vehicleService.update(this.vehicleId, this.vehicle);
    if (!this.vehicleId) obs = this.vehicleService.create(this.vehicle);

    obs.subscribe((res) =>
      this.toastrService.success('Vehicle saved successfully', 'Done')
    );
  }
  delete() {
    let confirmed = confirm('Are you sure you want to delete this item?');
    if (!confirmed) return;

    this.vehicleService
      .delete(this.vehicleId)
      .subscribe((res) =>
        this.toastrService.success('Vehicle removed successfully', 'Done')
      );
  }
  private setVehicle(vehicle: VehicleModel) {
    this.vehicle.id = vehicle.id;
    this.vehicle.modelId = vehicle.model.id;
    this.vehicle.makeId = vehicle.make.id;
    this.vehicle.isRegistered = vehicle.isRegistered;
    this.vehicle.contact = vehicle.contact;
    this.vehicle.features = vehicle.features.map((f) => f.id);
  }
}
