import { OperationStatus } from "../../../Shared/enums";
import { IUserMediaUpload } from "../../States";

export const UserMediaUpload: IUserMediaUpload = 
{
    handle: undefined,
    status: OperationStatus.notStarted,
    error: { },
    payload: { blobName: "" }
}
