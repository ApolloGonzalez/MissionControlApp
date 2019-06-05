/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MissionNavComponent } from './mission-nav.component';

describe('MissionDetailNavComponent', () => {
  let component: MissionNavComponent;
  let fixture: ComponentFixture<MissionNavComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MissionNavComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MissionNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
