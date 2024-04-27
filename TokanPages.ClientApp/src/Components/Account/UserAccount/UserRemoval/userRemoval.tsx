import * as React from "react";
import { useHistory } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { OperationStatus } from "../../../../Shared/enums";
import { UserRemovalView } from "./View/userRemovalView";

import {
    ApplicationDialogAction,
    UserDataStoreAction,
    UserSigninAction,
    UserRemoveAction,
} from "../../../../Store/Actions";

import { SuccessMessage } from "../../../../Shared/Services/Utilities";

import { RECEIVED_ERROR_MESSAGE } from "../../../../Shared/constants";

export const UserRemoval = (): JSX.Element => {
    const dispatch = useDispatch();
    const history = useHistory();

    const template = useSelector((state: ApplicationState) => state.contentTemplates?.content);
    const account = useSelector((state: ApplicationState) => state.contentAccount);
    const remove = useSelector((state: ApplicationState) => state.userRemove);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = remove?.status === OperationStatus.notStarted;
    const hasFinished = remove?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [hasProgress, setHasProgress] = React.useState(false);

    const showSuccess = (text: string) =>
        dispatch(ApplicationDialogAction.raise(SuccessMessage(template.forms.textAccountSettings, text)));

    const clear = React.useCallback(() => {
        if (!hasProgress) return;

        dispatch(UserRemoveAction.clear());
        dispatch(UserSigninAction.clear());
        dispatch(UserDataStoreAction.clear());

        setHasProgress(false);
        history.push("/");
    }, [hasProgress]);

    React.useEffect(() => {
        if (hasError) {
            setHasProgress(false);
            return;
        }

        if (hasNotStarted && hasProgress) {
            dispatch(UserRemoveAction.remove({}));
            return;
        }

        if (hasFinished) {
            clear();
            showSuccess(template.templates.user.removal);
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, template]);

    const deleteButtonHandler = React.useCallback(() => {
        if (remove?.status !== OperationStatus.notStarted) {
            dispatch(UserRemoveAction.clear());
        }

        if (!hasProgress) {
            setHasProgress(true);
        }
    }, [hasProgress]);

    return (
        <UserRemovalView
            isLoading={account.isLoading}
            deleteButtonHandler={deleteButtonHandler}
            deleteAccountProgress={hasProgress}
            sectionAccessDenied={account.content?.sectionAccessDenied}
            sectionAccountRemoval={account.content?.sectionAccountRemoval}
        />
    );
};
