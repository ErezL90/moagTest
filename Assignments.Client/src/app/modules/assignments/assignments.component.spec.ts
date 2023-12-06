import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssinmentsComponent } from './assignments.component';

describe('AssinmentsComponent', () => {
  let component: AssinmentsComponent;
  let fixture: ComponentFixture<AssinmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AssinmentsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AssinmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
