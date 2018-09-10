import { TestBed, inject } from '@angular/core/testing';
import { DataService } from './data.service';
describe('DataService', function () {
    beforeEach(function () {
        TestBed.configureTestingModule({
            providers: [DataService]
        });
    });
    it('should be created', inject([DataService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=data.service.spec.js.map