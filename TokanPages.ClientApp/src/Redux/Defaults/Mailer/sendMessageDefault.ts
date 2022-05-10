import { ISendMessage } from "../../../Redux/States/Mailer/sendMessageState";
import { OperationStatus } from "../../../Shared/enums";

export const SendMessageDefault: ISendMessage = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
