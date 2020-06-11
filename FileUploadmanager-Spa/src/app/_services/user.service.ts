import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { map } from 'rxjs/operators';
import { Filedescription } from '../_models/filedescription';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  baseUrl = environment.baseUrl + 'user/';
  FileDescription: any[] = [];
  constructor(private http: HttpClient) {}

  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }
  getAllFilesDesc(userId): Observable<Filedescription[]> {
    return this.http.get<Filedescription[]>(
      this.baseUrl + userId + '/fileuploader/'
    );
  }
  deleteUserFile(userId, fileId) {
    return this.http.delete(
      this.baseUrl + userId + '/fileuploader/deleteuserfile/' + fileId
    );
  }
  getAllimportedFileDesc(userId): Observable<Filedescription[]> {
    return this.http.get<Filedescription[]>(
      this.baseUrl + userId + '/fileuploader/getallimportedfile'
    );
  }
}
