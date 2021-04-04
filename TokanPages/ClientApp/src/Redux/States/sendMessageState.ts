import { OperationStatus } from "../../Shared/Enums";

export interface ISendMessage
{
    isSendingMessage: OperationStatus;
    hasSentMessage: boolean;
    attachedErrorObject: any;
}
