import { OperationStatus } from "../../Shared/enums";
import { IAddUserDto } from "../../Api/Models";

export interface ISigninUser
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;
    userData: IAddUserDto;
}
