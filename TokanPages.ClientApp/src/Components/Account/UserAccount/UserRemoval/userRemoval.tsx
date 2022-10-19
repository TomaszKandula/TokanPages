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
    
    const remove = useSelector((state: IApplicationState) => state.userRemove);
    const error = useSelector((state: IApplicationState) => state.applicationError);

    const hasNotStarted = remove?.status === OperationStatus.notStarted;
    const hasFinished = remove?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [progress, setProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(ACCOUNT_FORM, text)));

    const clear = React.useCallback(() => 
    {
        if (!progress) return;

        dispatch(UserRemoveAction.clear);
        setProgress(false);
        dispatch(UserSigninAction.clear());
        dispatch(UserDataStoreAction.clear());
        history.push("/");
    }, 
    [ progress ]);

    React.useEffect(() => 
    {
        if (hasError)
        {
            setProgress(false);
            return;
        }

        if (hasNotStarted && progress)
        {
            dispatch(UserRemoveAction.remove({ }));
            return;
        }

        if (hasFinished)
        {
            clear();
            showSuccess(REMOVE_USER);        
        }
    }, 
    [ progress, hasError, hasNotStarted, hasFinished ]);

    const deleteButtonHandler = () => 
    {
        if (!progress) 
        {
            setProgress(true);
        }
    };

    return(
        <UserRemovalView bind=
        {{
            isLoading: props.isLoading,
            deleteButtonHandler: deleteButtonHandler,
            deleteAccountProgress: progress,
            sectionAccessDenied: props.content?.sectionAccessDenied,
            sectionAccountRemoval: props.content?.sectionAccountRemoval
        }} />
    );
}
