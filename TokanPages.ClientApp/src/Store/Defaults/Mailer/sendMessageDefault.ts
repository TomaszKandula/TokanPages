import { ISendMessage } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const SendMessageDefault: ISendMessage = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
