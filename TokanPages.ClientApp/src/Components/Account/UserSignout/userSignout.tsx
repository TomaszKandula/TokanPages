import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentUserSignoutState } from "../../../Store/States";
import { OperationStatus } from "../../../Shared/enums";
import { UserSignoutView } from "./View/userSignoutView";

import { 
    UserDataStoreAction, 
    UserSignoutAction
} from "../../../Store/Actions";

export const UserSignout = (props: ContentUserSignoutState): JSX.Element => 
{
    const dispatch = useDispatch();
    const [hasProgress, setHasProgress] = React.useState(true);
    const signout = useSelector((state: ApplicationState) => state.userSignout);

    const isUserTokenRevoked = signout.userTokenStatus === OperationStatus.hasFinished;
    const isRefreshTokenRevoked = signout.refreshTokenStatus === OperationStatus.hasFinished;

    const status = hasProgress ? props.content.onProcessing : props.content.onFinish;
    const hasContent = !props.isLoading && props.content.buttonText !== "";

    React.useEffect(() => 
    {
        if (!isUserTokenRevoked || !isRefreshTokenRevoked) return;

        dispatch(UserSignoutAction.clearRefreshToken());
        dispatch(UserSignoutAction.clearUserToken());
        dispatch(UserDataStoreAction.clear());
    }, 
    [ isUserTokenRevoked, isRefreshTokenRevoked ]);

    React.useEffect(() => 
    {
        if (!hasContent) return;
        if (!hasProgress) return;

        dispatch(UserSignoutAction.revokeRefreshToken());
        dispatch(UserSignoutAction.revokeUserToken());
        setHasProgress(false);
    }, 
    [ hasProgress, hasContent ]);

    return (<UserSignoutView
        isLoading={props.isLoading}
        caption={props.content.caption}
        status={status}
        buttonText={props.content.buttonText}
    />);
}
