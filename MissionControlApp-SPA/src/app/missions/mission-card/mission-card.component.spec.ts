/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MissionCardComponent } from './mission-card.component';

describe('MissionCardComponent', () => {
  let component: MissionCardComponent;
  let fixture: ComponentFixture<MissionCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MissionCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MissionCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
