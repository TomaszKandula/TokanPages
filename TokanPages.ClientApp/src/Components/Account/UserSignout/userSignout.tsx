import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { UserDataStoreAction, UserSignoutAction } from "../../../Store/Actions";
import { ApplicationState } from "../../../Store/Configuration";
import { OperationStatus } from "../../../Shared/enums";
import { UserSignoutView } from "./View/userSignoutView";
import Validate from "validate.js";

export interface UserSignoutProps {
    className?: string;
}

export const UserSignout = (props: UserSignoutProps): React.ReactElement => {
    const dispatch = useDispatch();
    const [hasProgress, setHasProgress] = React.useState(true);

    const signout = useSelector((state: ApplicationState) => state.userSignout);
    const store = useSelector((state: ApplicationState) => state.userDataStore);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage?.id);
    const contentData = data.components.accountUserSignout;

    const isUserTokenRevoked = signout.userTokenStatus === OperationStatus.hasFinished;
    const isRefreshTokenRevoked = signout.refreshTokenStatus === OperationStatus.hasFinished;
    const isAnonymous = Validate.isEmpty(store?.userData?.userId);

    const status = hasProgress ? contentData?.onProcessing : contentData?.onFinish;
    const hasContent = !data?.isLoading && contentData?.buttonText !== "";

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
            isLoading={data?.isLoading}
            languageId={languageId}
            caption={contentData?.caption}
            status={status}
            buttonText={contentData?.buttonText}
            isAnonymous={isAnonymous}
            className={props.className}
        />
    );
};
