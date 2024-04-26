import { ApplicationEmailState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const ApplicationEmail: ApplicationEmailState = {
    status: OperationStatus.notStarted,
    response: { },
};
