import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Filedescription } from '../_models/filedescription';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class ImportedfileListResolver implements Resolve<Filedescription[]> {
  constructor(
    private userService: UserService,
    private alertify: AlertifyService,
    private router: Router,
    private authservice: AuthService
  ) {}
  resolve(route: ActivatedRouteSnapshot): Observable<Filedescription[]> {
    return this.userService
      .getAllimportedFileDesc(this.authservice.decodedToken.nameid)
      .pipe(
        catchError((error) => {
          this.alertify.error(error);
          this.router.navigate(['/home']);
          return of(null);
        })
      );
  }
}
