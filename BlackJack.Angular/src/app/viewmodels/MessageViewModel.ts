import { JsonProperty } from 'json-typescript-mapper';

export class MessageViewModel {
    @JsonProperty('Message')
    Message: string;

    constructor() {
        this.Message = void 0;
    }
}