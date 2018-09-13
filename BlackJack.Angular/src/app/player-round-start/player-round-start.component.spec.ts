import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerRoundStartComponent } from './player-round-start.component';

describe('PlayerRoundStartComponent', () => {
  let component: PlayerRoundStartComponent;
  let fixture: ComponentFixture<PlayerRoundStartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerRoundStartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerRoundStartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
