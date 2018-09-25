import { TestBed, inject } from '@angular/core/testing';
import { UserNameService } from './user-name.service';
describe('UserNameService', function () {
    beforeEach(function () {
        TestBed.configureTestingModule({
            providers: [UserNameService]
        });
    });
    it('should be created', inject([UserNameService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=user-name.service.spec.js.map