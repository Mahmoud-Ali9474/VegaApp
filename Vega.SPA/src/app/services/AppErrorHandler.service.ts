import { ToastrService } from 'ngx-toastr';
import * as Sentry from '@sentry/angular';
import {
  ErrorHandler,
  Inject,
  Injectable,
  Injector,
  isDevMode,
} from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AppErrorHandler implements ErrorHandler {
  constructor(@Inject(Injector) private injector: Injector) {}
  private get toastrService(): ToastrService {
    return this.injector.get(ToastrService);
  }
  handleError(error: any): void {
    this.toastrService.error(error.message, 'Error', {
      onActivateTick: true,
    });
    if (!isDevMode()) Sentry.captureException(error);
    else throw error;
  }
}
