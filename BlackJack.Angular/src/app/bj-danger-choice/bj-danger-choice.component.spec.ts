import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BjDangerChoiceComponent } from './bj-danger-choice.component';

describe('BjDangerChoiceComponent', () => {
  let component: BjDangerChoiceComponent;
  let fixture: ComponentFixture<BjDangerChoiceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BjDangerChoiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BjDangerChoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
