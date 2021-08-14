import { ISigninUser } from "../../Redux/States/signinUserState";
import { OperationStatus } from "../../Shared/enums";

export const SigninUserDefault: ISigninUser = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { },
    userData:
    {
        userId: "",
        aliasName: "",
        avatarName: "",
        firstName: "",
        lastName: "",
        shortBio: "",
        registered: ""
    }
}
