import { Component, OnInit, Input } from '@angular/core';
import { ValidationService } from '../ValidationService';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-control-message',
  template: `
    <div *ngIf="errorMessage !== null" class="invalid-feedback">
      {{ errorMessage }}
    </div>
  `,
  styleUrls: ['./control-message.component.css'],
})
export class ControlMessageComponent {
  @Input() control: FormControl;
  constructor() {}

  get errorMessage() {
    for (const propertyName in this.control.errors) {
      if (
        this.control.errors.hasOwnProperty(propertyName) &&
        this.control.touched
      ) {
        return ValidationService.getValidatorErrorMessage(
          propertyName,
          this.control.errors[propertyName]
        );
      }
    }

    return null;
  }
}
