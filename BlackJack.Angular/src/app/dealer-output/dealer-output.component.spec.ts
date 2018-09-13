import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DealerOutputComponent } from './dealer-output.component';

describe('DealerOutputComponent', () => {
  let component: DealerOutputComponent;
  let fixture: ComponentFixture<DealerOutputComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DealerOutputComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DealerOutputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
