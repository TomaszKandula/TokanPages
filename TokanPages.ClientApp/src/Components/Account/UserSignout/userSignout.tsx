import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { UserDataStoreAction, UserSignoutAction } from "../../../Store/Actions";
import { ApplicationState } from "../../../Store/Configuration";
import { OperationStatus } from "../../../Shared/enums";
import { UserSignoutView } from "./View/userSignoutView";
import Validate from "validate.js";

export const UserSignout = (): JSX.Element => {
    const dispatch = useDispatch();
    const [hasProgress, setHasProgress] = React.useState(true);

    const contentData = useSelector((state: ApplicationState) => state.contentUserSignout);
    const signout = useSelector((state: ApplicationState) => state.userSignout);
    const store = useSelector((state: ApplicationState) => state.userDataStore);

    const isUserTokenRevoked = signout.userTokenStatus === OperationStatus.hasFinished;
    const isRefreshTokenRevoked = signout.refreshTokenStatus === OperationStatus.hasFinished;
    const isAnonymous = Validate.isEmpty(store?.userData?.userId);

    const status = hasProgress ? contentData?.content.onProcessing : contentData?.content.onFinish;
    const hasContent = !contentData?.isLoading && contentData?.content.buttonText !== "";

    React.useEffect(() => {
        if (!isUserTokenRevoked || !isRefreshTokenRevoked) return;

        dispatch(UserSignoutAction.clearRefreshToken());
        dispatch(UserSignoutAction.clearUserToken());
        dispatch(UserDataStoreAction.clear());
    }, [isUserTokenRevoked, isRefreshTokenRevoked]);

    React.useEffect(() => {
        if (!hasContent) return;
        if (!hasProgress) return;

        dispatch(UserSignoutAction.revokeRefreshToken());
        dispatch(UserSignoutAction.revokeUserToken());
        setHasProgress(false);
    }, [hasProgress, hasContent]);

    return (
        <UserSignoutView
            isLoading={contentData?.isLoading}
            caption={contentData?.content.caption}
            status={status}
            buttonText={contentData?.content.buttonText}
            isAnonymous={isAnonymous}
        />
    );
};
