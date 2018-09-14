import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerOutputComponent } from './player-output.component';

describe('PlayerOutputComponent', () => {
  let component: PlayerOutputComponent;
  let fixture: ComponentFixture<PlayerOutputComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerOutputComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerOutputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
