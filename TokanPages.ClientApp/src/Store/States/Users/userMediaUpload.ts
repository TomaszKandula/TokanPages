import { UploadUserMediaResultDto } from "../../../Api/Models";
import { OperationStatus } from "../../../Shared/enums";

export interface UserMediaUploadState {
    handle?: string;
    status: OperationStatus;
    error: object;
    payload: UploadUserMediaResultDto | undefined;
}
