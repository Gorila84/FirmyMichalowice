/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { Polityka_prywatnosciComponent } from './polityka_prywatnosci.component';

describe('Polityka_prywatnosciComponent', () => {
  let component: Polityka_prywatnosciComponent;
  let fixture: ComponentFixture<Polityka_prywatnosciComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Polityka_prywatnosciComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Polityka_prywatnosciComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
