import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { IApplicationState } from "../../../Store/Configuration";
import { IGetUpdatePasswordContent } from "../../../Store/States";
import { ApplicationDialog, UserUpdatePasswordAction } from "../../../Store/Actions";
import { IUpdateUserPasswordDto } from "../../../Api/Models";
import SuccessMessage from "../../../Shared/Components/ApplicationDialogBox/Helpers/successMessage";
import WarningMessage from "../../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { IValidateUpdateForm, ValidateUpdateForm } from "../../../Shared/Services/FormValidation";
import { GetTextWarning } from "../../../Shared/Services/Utilities";
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

export const UpdatePassword = (props: IGetUpdatePasswordContent): JSX.Element =>
{
    const queryParam = useQuery();
    const dispatch = useDispatch();
    
    const data = useSelector((state: IApplicationState) => state.userDataStore);
    const password = useSelector((state: IApplicationState) => state.userPasswordUpdate);
    const error = useSelector((state: IApplicationState) => state.applicationError);
    
    const resetId = queryParam.get("id");
    const userId = data?.userData.userId;
    const disableForm = Validate.isEmpty(resetId) && Validate.isEmpty(userId);

    const showSuccess = React.useCallback((text: string) => dispatch(ApplicationDialog.raise(SuccessMessage(UPDATE_FORM, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string) => dispatch(ApplicationDialog.raise(WarningMessage(UPDATE_FORM, text))), [ dispatch ]);

    const [form, setForm] = React.useState(formDefaultValues);
    const [progress, setProgress] = React.useState(false);

    const update = React.useCallback((payload: IUpdateUserPasswordDto) => dispatch(UserUpdatePasswordAction.update(payload)), [ dispatch ]);
    const clear = React.useCallback(() => dispatch(UserUpdatePasswordAction.clear()), [ dispatch ]);
    
    const clearForm = React.useCallback(() => 
    {
        if (!progress) return;
        setProgress(false);
        clear();
    }, 
    [ progress, clear ]);

    React.useEffect(() => 
    {
        if (error?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clearForm();
            return;
        }

        switch(password?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (progress) update(
                {
                    id: userId,
                    resetId: resetId as string,
                    newPassword: form.newPassword
                });
            break;

            case OperationStatus.hasFinished:
                clearForm();
                setForm(formDefaultValues);
                showSuccess(UPDATE_PASSWORD_SUCCESS);
            break;
        }
    }, 
    [ progress, error?.defaultErrorMessage, password?.operationStatus, 
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
