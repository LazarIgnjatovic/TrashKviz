import { Injectable, NgModule } from '@angular/core';

@Injectable()
export class EnsureModuleLoadedOnceGuard {
  constructor() {}
  checkIfModuleIsLoaded(targetModule: any) {
    if (targetModule) {
      throw new Error(
        `${targetModule.constructor.name} has already been loaded! Import it in AppModule only!`
      );
    }
  }
}
