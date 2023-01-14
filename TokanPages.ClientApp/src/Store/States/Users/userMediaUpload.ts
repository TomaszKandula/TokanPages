import { IUploadUserMediaResultDto } from "../../../Api/Models";
import { OperationStatus } from "../../../Shared/enums";

export interface IUserMediaUpload
{
    handle?: string;
    status: OperationStatus;
    error: any;
    payload: IUploadUserMediaResultDto | undefined;
}
