import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { UserDataStoreAction } from "../../../Store/Actions";
import { IAuthenticateUserResultDto } from "../../../Api/Models";
import { ApplicationUserInfoView } from "./View/applicationUserInfoView";

// TODO: add component content from the server
export const ApplicationUserInfo = (): JSX.Element => 
{
    const dispatch = useDispatch();
    const store = useSelector((state: IApplicationState) => state.userDataStore);

    const onClickHandler = () => 
    {
        dispatch(UserDataStoreAction.show(false));
    }

    const data: IAuthenticateUserResultDto = 
    {
        ...store?.userData,
        avatarName: store?.userData.avatarName
    }

    return (<ApplicationUserInfoView
        state={store?.isShown ?? false}
        data={data}
        closeHandler={onClickHandler}
    />);
}
