import { TestBed, inject } from '@angular/core/testing';
import { StartpageService } from './startpage.service';
describe('StartpageService', function () {
    beforeEach(function () {
        TestBed.configureTestingModule({
            providers: [StartpageService]
        });
    });
    it('should be created', inject([StartpageService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=startpage.service.spec.js.map