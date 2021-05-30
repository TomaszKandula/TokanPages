import { ISendMessage } from "../../Redux/States/sendMessageState";
import { OperationStatus } from "../../Shared/enums";

export const SendMessageStateDefault: ISendMessage = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
