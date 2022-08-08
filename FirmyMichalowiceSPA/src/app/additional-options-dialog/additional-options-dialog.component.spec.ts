import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdditionalOptionsDialogComponent } from './additional-options-dialog.component';

describe('AdditionalOptionsDialogComponent', () => {
  let component: AdditionalOptionsDialogComponent;
  let fixture: ComponentFixture<AdditionalOptionsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdditionalOptionsDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdditionalOptionsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
