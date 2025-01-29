import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { RECEIVED_ERROR_MESSAGE } from "../../../../Shared/constants";
import { OperationStatus } from "../../../../Shared/enums";
import { ApplicationDialogAction, UserDataStoreAction, UserUpdateAction } from "../../../../Store/Actions";
import { ApplicationState } from "../../../../Store/Configuration";
import { SuccessMessage } from "../../../../Shared/Services/Utilities";
import { UserDeactivationView } from "./View/userDeactivationView";

export interface UserDeactivationProps {
    background?: React.CSSProperties;
}

export const UserDeactivation = (props: UserDeactivationProps): React.ReactElement => {
    const dispatch = useDispatch();
    const history = useHistory();

    const store = useSelector((state: ApplicationState) => state.userDataStore.userData);
    const update = useSelector((state: ApplicationState) => state.userUpdate);
    const error = useSelector((state: ApplicationState) => state.applicationError);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage.id);
    const template = data.components.templates;
    const account = data.components.accountSettings;

    const hasUpdateNotStarted = update?.status === OperationStatus.notStarted;
    const hasUpdateFinished = update?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const showSuccess = (text: string) =>
        dispatch(ApplicationDialogAction.raise(SuccessMessage(template.forms.textAccountSettings, text)));

    const [hasProgress, setHasProgress] = React.useState(false);

    const clear = React.useCallback(() => {
        if (!hasProgress) return;

        dispatch(UserUpdateAction.clear());
        setHasProgress(false);
    }, [hasProgress]);

    React.useEffect(() => {
        if (hasError) {
            clear();
            return;
        }

        if (hasUpdateNotStarted && hasProgress) {
            dispatch(
                UserUpdateAction.update({
                    id: store.userId,
                    isActivated: false,
                })
            );

            return;
        }

        if (hasUpdateFinished) {
            showSuccess(template.templates.user.deactivation);
            dispatch(UserDataStoreAction.clear());
            dispatch(UserUpdateAction.clear());
            history.push(`/${languageId}`);
        }
    }, [store, template, hasProgress, hasError, hasUpdateNotStarted, hasUpdateFinished]);

    const deactivateButtonHandler = React.useCallback(() => {
        if (update?.status !== OperationStatus.notStarted) {
            dispatch(UserUpdateAction.clear());
        }

        if (!hasProgress) {
            setHasProgress(true);
        }
    }, [hasProgress]);

    return (
        <UserDeactivationView
            isLoading={data.isLoading}
            buttonHandler={deactivateButtonHandler}
            progress={hasProgress}
            section={account.sectionAccountDeactivation}
            background={props.background}
        />
    );
};
