import * as React from "react";
import { useHistory } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../../Store/Configuration";
import { IContentAccount } from "../../../../Store/States";
import { OperationStatus } from "../../../../Shared/enums";
import { UserRemovalView } from "./View/userRemovalView";

import { 
    ApplicationDialogAction, 
    UserDataStoreAction, 
    UserSigninAction, 
    UserRemoveAction, 
} from "../../../../Store/Actions";

import { 
    SuccessMessage, 
} from "../../../../Shared/Services/Utilities";

import { 
    ACCOUNT_FORM, 
    RECEIVED_ERROR_MESSAGE, 
    REMOVE_USER, 
} from "../../../../Shared/constants";

export const UserRemoval = (props: IContentAccount): JSX.Element => 
{
    const dispatch = useDispatch();
    const history = useHistory();
    
    const appState = useSelector((state: IApplicationState) => state.userRemove);
    const appError = useSelector((state: IApplicationState) => state.applicationError);

    const [deleteAccountProgress, setDeleteAccountProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(ACCOUNT_FORM, text)));

    const clear = React.useCallback(() => 
    {
        if (!deleteAccountProgress) return;

        dispatch(UserRemoveAction.clear);
        setDeleteAccountProgress(false);
        dispatch(UserSigninAction.clear());
        dispatch(UserDataStoreAction.clear());
        history.push("/");
    }, 
    [ deleteAccountProgress ]);

    React.useEffect(() => 
    {
        if (appError?.errorMessage === RECEIVED_ERROR_MESSAGE)
        {
            setDeleteAccountProgress(false);
            return;
        }

        switch(appState?.status)
        {
            case OperationStatus.notStarted:
                if (deleteAccountProgress) 
                {
                    dispatch(UserRemoveAction.remove({ }));
                }
            break;

            case OperationStatus.hasFinished:
                clear();
                showSuccess(REMOVE_USER);
            break;
        }
    }, 
    [ deleteAccountProgress, appError?.errorMessage, appState?.status, 
    OperationStatus.notStarted, OperationStatus.hasFinished ]);

    const deleteButtonHandler = () => 
    {
        if (!deleteAccountProgress) 
        {
            setDeleteAccountProgress(true);
        }
    };

    return(
        <UserRemovalView bind=
        {{
            isLoading: props.isLoading,
            deleteButtonHandler: deleteButtonHandler,
            deleteAccountProgress: deleteAccountProgress,
            sectionAccessDenied: props.content?.sectionAccessDenied,
            sectionAccountRemoval: props.content?.sectionAccountRemoval
        }} />
    );
}
