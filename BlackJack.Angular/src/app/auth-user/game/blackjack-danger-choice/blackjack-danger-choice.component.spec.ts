import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BlackjackDangerChoiceComponent } from './blackjack-danger-choice.component';

describe('BlackjackDangerChoiceComponent', () => {
  let component: BlackjackDangerChoiceComponent;
  let fixture: ComponentFixture<BlackjackDangerChoiceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BlackjackDangerChoiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlackjackDangerChoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
