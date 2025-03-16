import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { UserDataStoreAction } from "../../../Store/Actions";
import { AuthenticateUserResultDto } from "../../../Api/Models";
import { ApplicationUserInfoView } from "./View/applicationUserInfoView";

export const ApplicationUserInfo = (): React.ReactElement => {
    const dispatch = useDispatch();
    const store = useSelector((state: ApplicationState) => state.userDataStore);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const navigation = data?.components?.layoutNavigation;

    const onClickHandler = React.useCallback(() => {
        dispatch(UserDataStoreAction.show(false));
    }, []);

    const dto: AuthenticateUserResultDto = {
        ...store?.userData,
        avatarName: store?.userData.avatarName,
    };

    return (
        <ApplicationUserInfoView
            state={store?.isShown ?? false}
            content={navigation?.userInfo}
            data={dto}
            closeHandler={onClickHandler}
        />
    );
};
