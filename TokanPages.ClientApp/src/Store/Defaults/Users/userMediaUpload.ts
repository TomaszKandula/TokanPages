import { OperationStatus } from "../../../Shared/enums";
import { UserMediaUploadState } from "../../States";

export const UserMediaUpload: UserMediaUploadState = {
    handle: undefined,
    status: OperationStatus.notStarted,
    error: { },
    payload: { 
        blobName: "" 
    },
};
