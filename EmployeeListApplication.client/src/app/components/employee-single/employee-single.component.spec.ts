import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeSingleComponent } from './employee-single.component';

describe('EmployeeSingleComponent', () => {
  let component: EmployeeSingleComponent;
  let fixture: ComponentFixture<EmployeeSingleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeSingleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeSingleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
