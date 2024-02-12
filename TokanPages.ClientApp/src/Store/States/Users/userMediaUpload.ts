import { UploadUserMediaResultDto } from "../../../Api/Models";
import { OperationStatus } from "../../../Shared/enums";

export interface UserMediaUploadState {
    handle?: string;
    status: OperationStatus;
    error: any;
    payload: UploadUserMediaResultDto | undefined;
}
