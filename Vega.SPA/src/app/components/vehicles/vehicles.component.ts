import { KeyValuePairModel } from './../../models/KeyValuePairModel';
import { MakeModel } from './../../models/MakeModel';
import { VehicleModel } from './../../models/VehicleModel';
import { VehiclesService } from 'src/app/services/vehicles.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.css'],
})
export class VehiclesComponent implements OnInit {
  query: any = {};
  totalItem: Number = 0;
  vehicles: VehicleModel[] = [];
  makes: MakeModel[] = [];
  models: KeyValuePairModel[] = [];
  columns: any[] = [
    { title: 'Id', key: 'id', isSortable: false },
    { title: 'Model', key: 'model', isSortable: true },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    { title: '', key: '', isSortable: false },
  ];
  constructor(private vehiclesService: VehiclesService) {}

  ngOnInit() {
    this.vehiclesService.getMakes().subscribe((res) => (this.makes = res));
    this.populateVehicles();
  }
  private populateVehicles() {
    this.vehiclesService.getAllVehicles(this.query).subscribe((res) => {
      this.vehicles = res.pagedList;
      this.totalItem = res.totalCount;
    });
  }

  onMakeChange() {
    let selectedMake = this.makes.find((m) => m.id == this.query.makeId);
    this.models = selectedMake?.models || [];
    this.query.modelId = undefined;
  }

  applyFilter() {
    this.populateVehicles();
  }
  resetFilter() {
    this.query = {};
    this.populateVehicles();
  }
  applySorting(column: any) {
    if (!column.isSortable) return;

    if (this.query.sortBy === column.key)
      this.query.isSortAscending = !this.query.isSortAscending;
    else {
      this.query.sortBy = column.key;
      this.query.isSortAscending = false;
    }
    //console.log(column, this.query);
    this.populateVehicles();
  }
  onPageChange($event: any) {
    this.query.page = $event.page;
    this.query.pageSize = $event.pageSize;
    console.log($event, this.query);
    this.populateVehicles();
  }
}
