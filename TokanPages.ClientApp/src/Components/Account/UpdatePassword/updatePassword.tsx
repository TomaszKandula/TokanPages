import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentUpdatePasswordState } from "../../../Store/States";
import { OperationStatus } from "../../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { UpdatePasswordView } from "./View/updatePasswordView";
import Validate from "validate.js";

import { 
    ApplicationDialogAction, 
    UserPasswordUpdateAction 
} from "../../../Store/Actions";

import { 
    UpdateFormInput, 
    ValidateUpdateForm 
} from "../../../Shared/Services/FormValidation";

import { 
    GetTextWarning, 
    SuccessMessage, 
    WarningMessage 
} from "../../../Shared/Services/Utilities";

import {
    RECEIVED_ERROR_MESSAGE, 
    UPDATE_FORM, 
    UPDATE_PASSWORD_SUCCESS, 
    UPDATE_PASSWORD_WARNING 
} from "../../../Shared/constants";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

const formDefaultValues: UpdateFormInput =
{
    newPassword: "",
    verifyPassword: ""
};

export const UpdatePassword = (props: ContentUpdatePasswordState): JSX.Element =>
{
    const queryParam = useQuery();
    const dispatch = useDispatch();

    const data = useSelector((state: ApplicationState) => state.userDataStore);
    const update = useSelector((state: ApplicationState) => state.userPasswordUpdate);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = update?.status === OperationStatus.notStarted;
    const hasFinished = update?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const resetId = queryParam.get("id");
    const userId = data?.userData.userId;
    const canDisableForm = Validate.isEmpty(resetId) && Validate.isEmpty(userId);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(UPDATE_FORM, text)));
    const showWarning = (text: string) => dispatch(ApplicationDialogAction.raise(WarningMessage(UPDATE_FORM, text)));

    const [form, setForm] = React.useState(formDefaultValues);
    const [hasProgress, setHasProgress] = React.useState(false);

    const clearForm = React.useCallback(() => 
    {
        if (!hasProgress) return;
        setHasProgress(false);
        dispatch(UserPasswordUpdateAction.clear());
    }, 
    [ hasProgress ]);

    React.useEffect(() => 
    {
        if (hasError)
        {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) 
        {
            dispatch(UserPasswordUpdateAction.update(
            {
                id: userId,
                resetId: resetId as string,
                newPassword: form.newPassword
            }));

            return;
        }

        if (hasFinished) 
        {
            clearForm();
            setForm(formDefaultValues);
            showSuccess(UPDATE_PASSWORD_SUCCESS);
        }
    }, 
    [ hasProgress, hasError, hasNotStarted, hasFinished ]);

    const keyHandler = React.useCallback((event: ReactKeyboardEvent) => 
    {
        if (event.code === "Enter")
        {
            buttonHandler();
        }
    }, []);

    const formHandler = React.useCallback((event: ReactChangeEvent) => 
    { 
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); 
    }, [ form ]);

    const buttonHandler = React.useCallback(() =>
    {
        let results = ValidateUpdateForm(
        { 
            newPassword: form.newPassword, 
            verifyPassword: form.verifyPassword 
        });

        if (!Validate.isDefined(results))
        {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: results, template: UPDATE_PASSWORD_WARNING }));
    }, [ form ]);

    return (
        <UpdatePasswordView
            isLoading={props.isLoading}
            progress={hasProgress}
            caption={props.content.caption}
            button={props.content.button}
            newPassword={form.newPassword}
            verifyPassword={form.verifyPassword}
            keyHandler={keyHandler}
            formHandler={formHandler}
            buttonHandler={buttonHandler}
            disableForm={canDisableForm}
            labelNewPassword={props.content.labelNewPassword}
            labelVerifyPassword={props.content.labelVerifyPassword}
        />
    );
}
