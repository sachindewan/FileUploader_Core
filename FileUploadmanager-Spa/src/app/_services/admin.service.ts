import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  baseUrl = environment.baseUrl;
  constructor(private httpClient: HttpClient) {}
  getUserWithRoles() {
    return this.httpClient.get<User[]>(this.baseUrl + 'admin/userWithRoles');
  }
  updateRoles(user: User, updateRoles: any) {
    return this.httpClient.post(
      this.baseUrl + 'admin/editRoles/' + user.userName,
      updateRoles
    );
  }
}
