import { async, TestBed } from '@angular/core/testing';
import { BjDangerChoiceComponent } from './bj-danger-choice.component';
describe('BjDangerChoiceComponent', function () {
    var component;
    var fixture;
    beforeEach(async(function () {
        TestBed.configureTestingModule({
            declarations: [BjDangerChoiceComponent]
        })
            .compileComponents();
    }));
    beforeEach(function () {
        fixture = TestBed.createComponent(BjDangerChoiceComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', function () {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=bj-danger-choice.component.spec.js.map