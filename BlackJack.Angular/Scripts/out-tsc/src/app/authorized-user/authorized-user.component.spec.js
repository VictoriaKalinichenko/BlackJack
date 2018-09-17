import { async, TestBed } from '@angular/core/testing';
import { AuthorizedUserComponent } from './authorized-user.component';
describe('AuthorizedUserComponent', function () {
    var component;
    var fixture;
    beforeEach(async(function () {
        TestBed.configureTestingModule({
            declarations: [AuthorizedUserComponent]
        })
            .compileComponents();
    }));
    beforeEach(function () {
        fixture = TestBed.createComponent(AuthorizedUserComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', function () {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=authorized-user.component.spec.js.map