import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { UserActivateAction, UserNotificationAction } from "../../../Store/Actions";
import { RECEIVED_ERROR_MESSAGE } from "../../../Shared/constants";
import { OperationStatus } from "../../../Shared/enums";
import { ActivateAccountView } from "./View/activateAccountView";

export interface ActivateAccountProps {
    id: string;
    type: string;
}

const DefaultValues = {
    type: "Unset",
    caption: "",
    text1: "",
    text2: "",
    button: "",
};

export const ActivateAccount = (props: ActivateAccountProps): JSX.Element => {
    const dispatch = useDispatch();
    const contentData = useSelector((state: ApplicationState) => state.contentActivateAccount);

    const onSuccess = contentData?.content?.onSuccess;
    const onVerifying = contentData?.content?.onVerifying;
    const onProcessing = contentData?.content?.onProcessing;
    const onError = contentData?.content?.onError;

    const onSuccessWithoutLock = {
        ...onSuccess,
        text1: onSuccess?.noBusinessLock.text1,
        text2: onSuccess?.noBusinessLock.text2,
    };
    const onSuccessWithLock = {
        ...onSuccess,
        text1: onSuccess?.businessLock.text1,
        text2: onSuccess?.businessLock.text2,
    };

    const userActivation = useSelector((state: ApplicationState) => state.userActivate);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = userActivation?.status === OperationStatus.notStarted;
    const hasFinished = userActivation?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [content, setContent] = React.useState(DefaultValues);
    const [hasProgress, setHasProgress] = React.useState(true);
    const [hasRequested, setHasRequested] = React.useState(false);

    React.useEffect(() => {
        if (!hasProgress) return;
        if (content?.type === "Unset") {
            if (props.type === "verification") {
                setContent(onVerifying);
                return;
            }

            setContent(onProcessing);
            return;
        }

        if (hasError) {
            setContent(onError);
            setHasProgress(false);
            return;
        }

        if (hasNotStarted && hasProgress && !hasRequested) {
            setHasRequested(true);
            setTimeout(
                () =>
                    dispatch(
                        UserActivateAction.activate({
                            activationId: props.id,
                        })
                    ),
                1500
            );

            return;
        }

        if (hasFinished) {
            dispatch(
                UserNotificationAction.notify({
                    userId: userActivation.response.userId,
                    handler: "user_activated",
                })
            );

            setHasProgress(false);

            return userActivation.response.hasBusinessLock
                ? setContent(onSuccessWithLock)
                : setContent(onSuccessWithoutLock);
        }
    }, [
        props.id,
        props.type,
        content?.type,
        hasError,
        hasProgress,
        hasRequested,
        hasNotStarted,
        hasFinished,
        onProcessing,
        onVerifying,
        userActivation?.response.hasBusinessLock,
        userActivation?.response.userId,
        onSuccessWithLock,
        onSuccessWithoutLock,
    ]);

    return (
        <ActivateAccountView
            isLoading={contentData?.isLoading}
            caption={content?.caption}
            text1={content?.text1}
            text2={content?.text2}
            progress={hasProgress}
        />
    );
};
