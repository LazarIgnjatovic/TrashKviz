import { Injectable } from '@angular/core';

@Injectable()
export class UtilityService {
  removeNullEntriesFromObject(obj: any) {
    Object.keys(obj).forEach((key: string) => {
      if (obj[key] == null || obj[key] == '') delete obj[key];
    });
  }
}
