import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  HttpClient,
  HttpClientModule,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { MaterialModule } from './modules/material/material.module';
import { EnsureModuleLoadedOnceGuard } from './guards/ensure-model-loaded-once/ensure-modul-loaded-once.guard';
import { AuthGuard } from './guards/auth/auth.guard';
import { NoAuthGuard } from './guards/no-auth/no-auth.guard';
import { AuthService } from './services/auth/auth.service';
import { HttpGeneralService } from './services/http-general/http-general.service';
import { MaterialComponentsConfigProviderService } from './services/material-components-config-provider/material-components-config-provider.service';
import { RegexProviderService } from './services/regex-provider/regex-provider.service';
import { UtilityService } from './services/utility/utility.service';
import { ValidationErrorMessageProviderService } from './services/validation-error-messages-provider/validation-error-message-provider.service';
import { SignalrGeneralService } from './services/signalr-general/signalr-general.service';
import { ErrorHandlerInterceptor } from './interceptors/error-handler/error-handler.interceptor';
import { WindowResizeDetectorService } from './services/window-resize-detector/window-resize-detector.service';
import { OverlayService } from './services/overlay-service/overlay.service';
import { SpinnerComponent } from './components/spinner/spinner.component';

const modules = [MaterialModule, HttpClientModule];

const providers = [
  {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorHandlerInterceptor,
    multi: true,
  },
  HttpClient,
  EnsureModuleLoadedOnceGuard,
  AuthGuard,
  NoAuthGuard,
  AuthService,
  HttpGeneralService,
  MaterialComponentsConfigProviderService,
  RegexProviderService,
  UtilityService,
  ValidationErrorMessageProviderService,
  SignalrGeneralService,
  WindowResizeDetectorService,
  OverlayService,
];

@NgModule({
  imports: [modules],
  providers: [providers],
  exports: [modules],
})
export class CoreModule {
  constructor(
    @Optional() @SkipSelf() parentModule: CoreModule,
    private ensureModuleLoadedOnceGuard: EnsureModuleLoadedOnceGuard
  ) {
    ensureModuleLoadedOnceGuard.checkIfModuleIsLoaded(parentModule);
  }
}
