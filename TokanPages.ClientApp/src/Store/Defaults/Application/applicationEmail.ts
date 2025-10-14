import { ApplicationEmailState } from "../../States";
import { OperationStatus } from "../../../Shared/Enums";

export const ApplicationEmail: ApplicationEmailState = {
    status: OperationStatus.notStarted,
    response: {},
};
