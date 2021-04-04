import { ISendMessage } from "../../Redux/States/sendMessageState";
import { OperationStatus } from "../../Shared/Enums";

export const SendMessageStateDefault: ISendMessage = 
{
    isSendingMessage: OperationStatus.notStarted,
    hasSentMessage: false,
    attachedErrorObject: { }
}
