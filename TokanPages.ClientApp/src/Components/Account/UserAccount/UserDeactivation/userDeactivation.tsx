import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { RECEIVED_ERROR_MESSAGE } from "../../../../Shared/constants";
import { IconType, OperationStatus } from "../../../../Shared/enums";
import { useDimensions } from "../../../../Shared/Hooks";
import { ApplicationDialogAction, UserDataStoreAction, UserUpdateAction } from "../../../../Store/Actions";
import { ApplicationState } from "../../../../Store/Configuration";
import { UserDeactivationView } from "./View/userDeactivationView";

export interface UserDeactivationProps {
    className?: string;
}

export const UserDeactivation = (props: UserDeactivationProps): React.ReactElement => {
    const dispatch = useDispatch();
    const history = useHistory();
    const media = useDimensions();

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

    const [hasProgress, setHasProgress] = React.useState(false);

    const clear = React.useCallback(() => {
        if (!hasProgress) return;

        dispatch(UserUpdateAction.clear());
        setHasProgress(false);
    }, [hasProgress]);

    React.useEffect(() => {
        if (!hasProgress) {
            return;
        }

        if (hasError) {
            clear();
            return;
        }

        if (hasUpdateNotStarted) {
            dispatch(
                UserUpdateAction.update({
                    id: store.userId,
                    isActivated: false,
                })
            );

            return;
        }

        if (hasUpdateFinished) {
            dispatch(
                ApplicationDialogAction.raise({
                    title: template.forms.textAccountSettings,
                    message: template.templates.user.deactivation,
                    icon: IconType.info,
                })
            );

            dispatch(UserDataStoreAction.clear());
            dispatch(UserUpdateAction.clear());
            history.push(`/${languageId}`);
        }
    }, [store, template, languageId, hasProgress, hasError, hasUpdateNotStarted, hasUpdateFinished]);

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
            isMobile={media.isMobile}
            buttonHandler={deactivateButtonHandler}
            progress={hasProgress}
            section={account.sectionAccountDeactivation}
            className={props.className}
        />
    );
};
