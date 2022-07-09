import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ActionCreators } from "../../../Redux/Actions/Users/storeUserDataAction";
import { IApplicationState } from "../../../Redux/applicationState";
import { IAuthenticateUserResultDto } from "../../../Api/Models";
import { AVATARS_PATH } from "../../../Shared/constants";
import ApplicationUserInfoView from "./applicationUserInfoView";
// TODO: add component content from the server
const ApplicationUserInfo = (): JSX.Element => 
{
    const dispatch = useDispatch();
    const user = useSelector((state: IApplicationState) => state.storeUserData);

    const onClickHandler = () => 
    {
        dispatch(ActionCreators.show(false));
    }

    const data: IAuthenticateUserResultDto = 
    {
        ...user?.userData,
        avatarName: `${AVATARS_PATH}${user?.userData.avatarName}`
    }

    return (<ApplicationUserInfoView bind=
    {{
        state: user?.isShown ?? false,
        data: data,
        closeHandler: onClickHandler
    }}/>);
}

export default ApplicationUserInfo;
