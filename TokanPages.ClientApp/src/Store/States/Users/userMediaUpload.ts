import { UploadUserMediaResultDto } from "../../../Api/Models";
import { OperationStatus } from "../../../Shared/Enums";

export interface UserMediaUploadState {
    handle?: string;
    status: OperationStatus;
    error: object;
    payload: UploadUserMediaResultDto | undefined;
}
