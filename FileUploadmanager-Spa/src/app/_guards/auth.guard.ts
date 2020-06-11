import { Injectable } from '@angular/core';
import {
  CanActivate,
  Router,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const roles = route.firstChild.data['roles'] as Array<string>;

    if (roles) {
      const isAccesseable = this.authService.roleMatch(roles);
      if (isAccesseable) {
        return true;
      } else {
        this.router.navigate(['/member']);
        this.alertify.error('you are not authorized to access this area');
      }
    }
    if (this.authService.loggedIn()) {
      return true;
    }
    this.alertify.error('you shall not pass!!!');
    // not logged in so redirect to login page with the return url
    this.router.navigate(['/'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}
