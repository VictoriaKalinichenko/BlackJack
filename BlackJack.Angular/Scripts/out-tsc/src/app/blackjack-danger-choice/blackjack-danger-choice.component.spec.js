import { async, TestBed } from '@angular/core/testing';
import { BlackjackDangerChoiceComponent } from './blackjack-danger-choice.component';
describe('BlackjackDangerChoiceComponent', function () {
    var component;
    var fixture;
    beforeEach(async(function () {
        TestBed.configureTestingModule({
            declarations: [BlackjackDangerChoiceComponent]
        })
            .compileComponents();
    }));
    beforeEach(function () {
        fixture = TestBed.createComponent(BlackjackDangerChoiceComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', function () {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=blackjack-danger-choice.component.spec.js.map