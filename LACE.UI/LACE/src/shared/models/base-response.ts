import { Message } from "./message";
import { MessageTypeEnum } from "./message-type-enum";

export class BaseResponse<T>{
    responseData?: T = undefined;
    messages:Message[] = [];
    isValid: boolean = !this.messages.find(x => x.messageType >= 0);
    inError: boolean = !!this.messages.find(x => x.messageType >= 4);
    hasAnyMessages: boolean = this.messages.length > 0;
    hasAnyCautionMessages: boolean = !!this.messages.find(x => x.code == MessageTypeEnum.Caution);
    hasValidationMessages: boolean = !!this.messages.find(x => x.code == MessageTypeEnum.Validation);
    hasWarningMessages: boolean = !!this.messages.find(x => x.code == MessageTypeEnum.Warning);
    hasErrorMessages: boolean = !!this.messages.find(x => x.code == MessageTypeEnum.Error);
    hasExceptionMessages: boolean = !!this.messages.find(x => x.code == MessageTypeEnum.Exception);
    hasFatalErrorMessages: boolean = !!this.messages.find(x => x.code == MessageTypeEnum.FatalError);
    hasResponseData: boolean = !!this.responseData;
}
