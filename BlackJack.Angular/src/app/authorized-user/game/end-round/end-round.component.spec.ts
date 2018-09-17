import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EndRoundComponent } from './end-round.component';

describe('EndRoundComponent', () => {
  let component: EndRoundComponent;
  let fixture: ComponentFixture<EndRoundComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EndRoundComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EndRoundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
