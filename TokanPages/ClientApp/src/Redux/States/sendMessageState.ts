import { OperationStatuses } from "../../Shared/Enums";

export interface ISendMessage
{
    isSendingMessage: OperationStatuses;
    hasSentMessage: boolean;
    attachedErrorObject: any;
}
