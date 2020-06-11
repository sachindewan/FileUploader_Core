/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FileImportComponent } from './file-import.component';

describe('FileImportComponent', () => {
  let component: FileImportComponent;
  let fixture: ComponentFixture<FileImportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FileImportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FileImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
