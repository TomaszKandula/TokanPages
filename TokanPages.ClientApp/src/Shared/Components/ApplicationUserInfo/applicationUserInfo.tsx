import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { UserDataStoreAction } from "../../../Store/Actions";
import { AuthenticateUserResultDto } from "../../../Api/Models";
import { ApplicationUserInfoView } from "./View/applicationUserInfoView";

export const ApplicationUserInfo = (): JSX.Element => {
    const dispatch = useDispatch();
    const store = useSelector((state: ApplicationState) => state.userDataStore);
    const navigation = useSelector((state: ApplicationState) => state.contentNavigation);

    const onClickHandler = React.useCallback(() => {
        dispatch(UserDataStoreAction.show(false));
    }, []);

    const data: AuthenticateUserResultDto = {
        ...store?.userData,
        avatarName: store?.userData.avatarName,
    };

    return (
        <ApplicationUserInfoView
            state={store?.isShown ?? false}
            content={navigation?.content?.userInfo}
            data={data}
            closeHandler={onClickHandler}
        />
    );
};
