import { IApplicationMessage } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const ApplicationMessage: IApplicationMessage = 
{
    status: OperationStatus.notStarted,
    response: { }
}
