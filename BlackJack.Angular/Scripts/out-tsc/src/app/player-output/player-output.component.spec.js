import { async, TestBed } from '@angular/core/testing';
import { PlayerOutputComponent } from './player-output.component';
describe('PlayerOutputComponent', function () {
    var component;
    var fixture;
    beforeEach(async(function () {
        TestBed.configureTestingModule({
            declarations: [PlayerOutputComponent]
        })
            .compileComponents();
    }));
    beforeEach(function () {
        fixture = TestBed.createComponent(PlayerOutputComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', function () {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=player-output.component.spec.js.map