import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpGeneralService } from 'src/app/core/services/http-general/http-general.service';
import { Login } from '../dtos/login.dto';

@Injectable()
export class UserService {
  constructor(private httpService: HttpGeneralService) {}

  // updateUser(user: User): Observable<void> {
  //   return this.httpService.put<void>('/auth/updateusercredentials', user);
  // }

  // deleteUser(): Observable<void> {
  //   return this.httpService.delete<void>('/users/deleteuser');
  // }

  // registerUser(user: User): Observable<void> {
  //   return this.httpService.post<void>('/users/createuser', user);
  // }
}
