import * as React from "react";
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { IconType, OperationStatus } from "../../../../Shared/enums";
import { useDimensions } from "../../../../Shared/Hooks";
import { UserRemovalView } from "./View/userRemovalView";

import {
    ApplicationDialogAction,
    UserDataStoreAction,
    UserSigninAction,
    UserRemoveAction,
} from "../../../../Store/Actions";

import { RECEIVED_ERROR_MESSAGE } from "../../../../Shared/constants";

export interface UserRemovalProps {
    className?: string;
}

export const UserRemoval = (props: UserRemovalProps): React.ReactElement => {
    const dispatch = useDispatch();
    const history = useHistory();
    const media = useDimensions();

    const remove = useSelector((state: ApplicationState) => state.userRemove);
    const error = useSelector((state: ApplicationState) => state.applicationError);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage?.id);
    const template = data.components.templates;
    const account = data.components.accountSettings;

    const hasNotStarted = remove?.status === OperationStatus.notStarted;
    const hasFinished = remove?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [hasProgress, setHasProgress] = React.useState(false);

    const clear = React.useCallback(() => {
        if (!hasProgress) return;

        dispatch(UserRemoveAction.clear());
        dispatch(UserSigninAction.clear());
        dispatch(UserDataStoreAction.clear());

        setHasProgress(false);
        history.push(`/${languageId}`);
    }, [hasProgress, languageId]);

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

            dispatch(
                ApplicationDialogAction.raise({
                    title: template.forms.textAccountSettings,
                    message: template.templates.user.removal,
                    icon: IconType.info,
                    buttons: {
                        primaryButton: {
                            label: "OK",
                        }
                    }                })
            );
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
            isLoading={data.isLoading}
            isMobile={media.isMobile}
            deleteButtonHandler={deleteButtonHandler}
            deleteAccountProgress={hasProgress}
            sectionAccountRemoval={account.sectionAccountRemoval}
            className={props.className}
        />
    );
};
