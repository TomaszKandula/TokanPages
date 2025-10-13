import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { UserActivateAction, UserNotificationAction } from "../../../Store/Actions";
import { RECEIVED_ERROR_MESSAGE } from "../../../Shared/constants";
import { OperationStatus } from "../../../Shared/enums";
import { AccountActivateView } from "./View/accountActivateView";
import { AccountActivateProps } from "./Types";
import Validate from "validate.js";

const DefaultValues = {
    type: "Unset",
    caption: "",
    text1: "",
    text2: "",
    button: "",
};

export const AccountActivate = (props: AccountActivateProps): React.ReactElement => {
    const dispatch = useDispatch();
    const hasEmptyId = Validate.isEmpty(props.id);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const contentData = data?.components?.accountActivate;

    const onSuccess = contentData?.onSuccess;
    const onVerifying = contentData?.onVerifying;
    const onProcessing = contentData?.onProcessing;
    const onError = contentData?.onError;

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

    const hasContentReady = !data.isLoading && data.status === OperationStatus.hasFinished;
    const hasOperationNotStarted = userActivation?.status === OperationStatus.notStarted;
    const hasOperationFinished = userActivation?.status === OperationStatus.hasFinished;
    const hasOperationError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [content, setContent] = React.useState(DefaultValues);
    const [hasProgress, setHasProgress] = React.useState(true);
    const [hasRequested, setHasRequested] = React.useState(false);
    const [hasSuccess, setHasSuccess] = React.useState(false);
    const [hasError, setHasError] = React.useState(false);

    const canProceed = hasContentReady && !hasEmptyId && hasProgress;

    React.useEffect(() => {
        if (!canProceed) return;

        if (content?.type === "Unset") {
            if (props.type === "verification") {
                setContent(onVerifying);
                return;
            }

            setContent(onProcessing);
            return;
        }

        if (hasOperationError) {
            setContent(onError);
            setHasError(true);
            setHasProgress(false);
            return;
        }

        if (hasOperationNotStarted && hasProgress && !hasRequested) {
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

        if (hasOperationFinished) {
            dispatch(
                UserNotificationAction.notify({
                    userId: userActivation.response.userId,
                    handler: "user_activated",
                })
            );

            setHasSuccess(true);
            setHasProgress(false);

            return userActivation.response.hasBusinessLock
                ? setContent(onSuccessWithLock)
                : setContent(onSuccessWithoutLock);
        }
    }, [
        canProceed,
        props.id,
        props.type,
        content?.type,
        hasOperationError,
        hasSuccess,
        hasProgress,
        hasRequested,
        hasOperationNotStarted,
        hasOperationFinished,
        userActivation?.response.hasBusinessLock,
        userActivation?.response.userId,
        onProcessing,
        onVerifying,
        onSuccessWithLock,
        onSuccessWithoutLock,
    ]);

    return (
        <AccountActivateView
            isLoading={data?.isLoading}
            shouldFallback={hasEmptyId}
            caption={content?.caption}
            text1={content?.text1}
            text2={content?.text2}
            fallback={{
                caption: contentData?.fallback.caption,
                text: contentData?.fallback.text,
            }}
            hasProgress={hasProgress}
            hasError={hasError}
            hasSuccess={hasSuccess}
            className={props.className}
        />
    );
};
