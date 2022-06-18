import { VehicleComponent } from './components/vehicle/vehicle.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { VehiclesComponent } from './components/vehicles/vehicles.component';
import { AppErrorHandler } from './services/AppErrorHandler.service';
import { NgModule, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import * as Sentry from '@sentry/angular';
import { BrowserTracing } from '@sentry/tracing';
import { AppComponent } from './app.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavComponent } from './components/nav/nav.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { VehiclesService } from './services/vehicles.service';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
Sentry.init({
  dsn: 'https://1e44fcafe73a40c898571979cd9c0e78@o1214281.ingest.sentry.io/6354768',
  integrations: [
    // Registers and configures the Tracing integration,
    // which automatically instruments your application to monitor its
    // performance, including custom Angular routing instrumentation
    new BrowserTracing({
      tracingOrigins: ['localhost'],
      routingInstrumentation: Sentry.routingInstrumentation,
    }),
  ],

  // Set tracesSampleRate to 1.0 to capture 100%
  // of transactions for performance monitoring.
  // We recommend adjusting this value in production
  tracesSampleRate: 1.0,
});
@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    FooterComponent,
    VehiclesComponent,
    VehicleFormComponent,
    VehicleComponent,
    PaginationComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: false,
    }),
    RouterModule.forRoot([
      { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
      {
        path: 'vehicles/edit/:id',
        component: VehicleFormComponent,
      },

      {
        path: 'vehicles/new',
        component: VehicleFormComponent,
      },
      {
        path: 'vehicles/:id',
        component: VehicleComponent,
      },
      {
        path: 'vehicles',
        component: VehiclesComponent,
      },
    ]),
  ],
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    VehiclesService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
