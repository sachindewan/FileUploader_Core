import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { MemberEditResolver } from '../_resolver/member-edit-resolver';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

@Injectable()
export class PreventUnSavedChages
  implements CanDeactivate<MemberEditComponent> {
  canDeactivate(component: MemberEditComponent) {
    if (component.editForm.dirty) {
      return confirm(
        'Are you sure you want to continue? Any unsaved changes will goes out'
      );
    }
    return true;
  }
}
