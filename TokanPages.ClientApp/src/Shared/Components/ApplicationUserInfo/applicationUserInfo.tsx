import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { UserDataAction } from "../../../Store/Actions";
import { IAuthenticateUserResultDto } from "../../../Api/Models";
import { ApplicationUserInfoView } from "./View/applicationUserInfoView";

// TODO: add component content from the server
export const ApplicationUserInfo = (): JSX.Element => 
{
    const dispatch = useDispatch();
    const user = useSelector((state: IApplicationState) => state.userDataStore);

    const onClickHandler = () => 
    {
        dispatch(UserDataAction.show(false));
    }

    const data: IAuthenticateUserResultDto = 
    {
        ...user?.userData,
        avatarName: user?.userData.avatarName
    }

    return (<ApplicationUserInfoView bind=
    {{
        state: user?.isShown ?? false,
        data: data,
        closeHandler: onClickHandler
    }}/>);
}
