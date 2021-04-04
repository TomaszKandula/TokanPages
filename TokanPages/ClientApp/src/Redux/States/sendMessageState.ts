import { OperationStatus } from "../../Shared/enums";

export interface ISendMessage
{
    isSendingMessage: OperationStatus;
    hasSentMessage: boolean;
    attachedErrorObject: any;
}
