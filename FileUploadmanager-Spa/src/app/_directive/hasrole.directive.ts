import {
  Directive,
  ViewContainerRef,
  TemplateRef,
  Input,
  OnInit,
} from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Directive({
  selector: '[appHasRole]',
})
export class HasroleDirective implements OnInit {
  @Input() appHasRole: string[];
  isVisible = false;
  constructor(
    private viewContainerRef: ViewContainerRef,
    private templateref: TemplateRef<any>,
    private authservice: AuthService
  ) {}
  ngOnInit() {
    const roles = this.authservice.decodedToken.role as Array<string>;
    // if user has no role then clear the viewcontainerref
    if (!roles) {
      this.viewContainerRef.clear();
    }
    // if you user has needed role then render the element
    if (this.authservice.roleMatch(this.appHasRole)) {
      if (!this.isVisible) {
        this.isVisible = true;
        this.viewContainerRef.createEmbeddedView(this.templateref);
      } else {
        this.isVisible = false;
        this.viewContainerRef.clear();
      }
    }
  }
}
