import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { IApplicationState } from "../../../Store/Configuration";
import { IContentUpdatePassword } from "../../../Store/States";
import { ApplicationDialogAction, UserPasswordUpdateAction } from "../../../Store/Actions";
import { IValidateUpdateForm, ValidateUpdateForm } from "../../../Shared/Services/FormValidation";
import { GetTextWarning, SuccessMessage, WarningMessage } from "../../../Shared/Services/Utilities";
import { OperationStatus } from "../../../Shared/enums";
import { UpdatePasswordView } from "./View/updatePasswordView";
import Validate from "validate.js";

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

const formDefaultValues: IValidateUpdateForm =
{
    newPassword: "",
    verifyPassword: ""
};

export const UpdatePassword = (props: IContentUpdatePassword): JSX.Element =>
{
    const queryParam = useQuery();
    const dispatch = useDispatch();

    const data = useSelector((state: IApplicationState) => state.userDataStore);
    const password = useSelector((state: IApplicationState) => state.userPasswordUpdate);
    const error = useSelector((state: IApplicationState) => state.applicationError);

    const resetId = queryParam.get("id");
    const userId = data?.userData.userId;
    const disableForm = Validate.isEmpty(resetId) && Validate.isEmpty(userId);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(UPDATE_FORM, text)));
    const showWarning = (text: string) => dispatch(ApplicationDialogAction.raise(WarningMessage(UPDATE_FORM, text)));

    const [form, setForm] = React.useState(formDefaultValues);
    const [progress, setProgress] = React.useState(false);

    const clearForm = React.useCallback(() => 
    {
        if (!progress) return;
        setProgress(false);
        dispatch(UserPasswordUpdateAction.clear());
    }, 
    [ progress ]);

    React.useEffect(() => 
    {
        if (error?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clearForm();
            return;
        }

        switch(password?.status)
        {
            case OperationStatus.notStarted:
                if (progress) dispatch(UserPasswordUpdateAction.update(
                {
                    id: userId,
                    resetId: resetId as string,
                    newPassword: form.newPassword
                }));
            break;

            case OperationStatus.hasFinished:
                clearForm();
                setForm(formDefaultValues);
                showSuccess(UPDATE_PASSWORD_SUCCESS);
            break;
        }
    }, 
    [ progress, error?.defaultErrorMessage, password?.status, 
    OperationStatus.notStarted, OperationStatus.hasFinished ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    { 
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); 
    };

    const buttonHandler = () =>
    {
        let results = ValidateUpdateForm(
        { 
            newPassword: form.newPassword, 
            verifyPassword: form.verifyPassword 
        });

        if (!Validate.isDefined(results))
        {
            setProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: results, template: UPDATE_PASSWORD_WARNING }));
    };

    return (
        <UpdatePasswordView bind=
        {{
            isLoading: props.isLoading,
            progress: progress,
            caption: props.content.caption,
            button: props.content.button,
            newPassword: form.newPassword,
            verifyPassword: form.verifyPassword,
            formHandler: formHandler,
            buttonHandler: buttonHandler,
            disableForm: disableForm,
            labelNewPassword: props.content.labelNewPassword,
            labelVerifyPassword: props.content.labelVerifyPassword
        }}/>
    );
}
