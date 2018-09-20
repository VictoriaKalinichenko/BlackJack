import { TestBed, inject } from '@angular/core/testing';
import { HttpService } from './http.service';
describe('HttpService', function () {
    beforeEach(function () {
        TestBed.configureTestingModule({
            providers: [HttpService]
        });
    });
    it('should be created', inject([HttpService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=http.service.spec.js.map