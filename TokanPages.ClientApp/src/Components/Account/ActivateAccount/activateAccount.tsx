import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { ApplicationState } from "../../../Store/Configuration";
import { UserActivateAction } from "../../../Store/Actions";
import { ContentActivateAccountState } from "../../../Store/States";
import { RECEIVED_ERROR_MESSAGE } from "../../../Shared/constants";
import { OperationStatus } from "../../../Shared/enums";
import { ActivateAccountView } from "./View/activateAccountView";

interface Properties extends ContentActivateAccountState {
    id: string;
}

export const ActivateAccount = (props: Properties): JSX.Element => {
    const onProcessing = props.content?.onProcessing;
    const onSuccess = props.content?.onSuccess;
    const onError = props.content?.onError;

    const dispatch = useDispatch();
    const history = useHistory();

    const activate = useSelector((state: ApplicationState) => state.userActivate);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = activate?.status === OperationStatus.notStarted;
    const hasFinished = activate?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [content, setContent] = React.useState(onProcessing);
    const [isButtonOff, setIsButtonOff] = React.useState(true);
    const [hasProgress, setHasProgress] = React.useState(true);
    const [isRequested, setIsRequested] = React.useState(false);

    React.useEffect(() => {
        if (!hasProgress) return;

        if (content.type === "Unset") {
            setContent(onProcessing);
            return;
        }

        if (hasError) {
            setContent(onError);
            setHasProgress(false);
            setIsButtonOff(false);
            return;
        }

        if (hasNotStarted && hasProgress && !isRequested) {
            setIsRequested(true);
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
            setContent(onSuccess);
            setHasProgress(false);
            setIsButtonOff(false);
        }
    }, [content.type, props.id, hasProgress, isRequested, hasError, hasNotStarted, hasFinished]);

    const buttonHandler = React.useCallback(() => {
        if (content.type === "Error") {
            setContent(onProcessing);
            setIsRequested(false);
            setHasProgress(true);
            setIsButtonOff(true);
            dispatch(UserActivateAction.clear());
        }

        if (content.type === "Success") {
            history.push("/");
        }
    }, [content.type]);

    return (
        <ActivateAccountView
            isLoading={props.isLoading}
            caption={content.caption}
            text1={content.text1}
            text2={content.text2}
            buttonHandler={buttonHandler}
            buttonDisabled={isButtonOff}
            progress={hasProgress}
            buttonText={content.button}
        />
    );
};
