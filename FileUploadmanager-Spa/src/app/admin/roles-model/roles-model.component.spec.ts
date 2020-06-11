/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { RolesModelComponent } from './roles-model.component';

describe('RolesModelComponent', () => {
  let component: RolesModelComponent;
  let fixture: ComponentFixture<RolesModelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RolesModelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RolesModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
