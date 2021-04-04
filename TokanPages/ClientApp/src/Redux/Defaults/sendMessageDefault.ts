import { ISendMessage } from "../../Redux/States/sendMessageState";
import { OperationStatuses } from "../../Shared/Enums";

export const SendMessageStateDefault: ISendMessage = 
{
    isSendingMessage: OperationStatuses.notStarted,
    hasSentMessage: false,
    attachedErrorObject: { }
}
