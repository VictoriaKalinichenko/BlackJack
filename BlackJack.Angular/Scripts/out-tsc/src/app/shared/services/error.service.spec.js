import { TestBed, inject } from '@angular/core/testing';
import { ErrorService } from './error.service';
describe('ErrorService', function () {
    beforeEach(function () {
        TestBed.configureTestingModule({
            providers: [ErrorService]
        });
    });
    it('should be created', inject([ErrorService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=error.service.spec.js.map