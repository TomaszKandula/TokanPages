import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { ApplicationState } from "../../../Store/Configuration";
import { OperationStatus } from "../../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { UpdatePasswordView } from "./View/updatePasswordView";
import Validate from "validate.js";

import { ApplicationDialogAction, UserPasswordUpdateAction } from "../../../Store/Actions";

import { UpdateFormInput, ValidateUpdateForm } from "../../../Shared/Services/FormValidation";

import { GetTextWarning, SuccessMessage, WarningMessage } from "../../../Shared/Services/Utilities";

import { RECEIVED_ERROR_MESSAGE } from "../../../Shared/constants";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

const formDefaultValues: UpdateFormInput = {
    newPassword: "",
    verifyPassword: "",
    content: {
        emailInvalid: "",
        nameInvalid: "",
        surnameInvalid: "",
        passwordInvalid: "",
        missingTerms: "",
        missingChar: "",
        missingLargeLetter: "",
        missingNumber: "",
        missingSmallLetter: "",
    }
};

export const UpdatePassword = (): JSX.Element => {
    const queryParam = useQuery();
    const dispatch = useDispatch();

    const template = useSelector((state: ApplicationState) => state.contentTemplates?.content);
    const password = useSelector((state: ApplicationState) => state.contentUpdatePassword);
    const data = useSelector((state: ApplicationState) => state.userDataStore);
    const update = useSelector((state: ApplicationState) => state.userPasswordUpdate);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = update?.status === OperationStatus.notStarted;
    const hasFinished = update?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const resetId = queryParam.get("id");
    const userId = data?.userData.userId;
    const canDisableForm = Validate.isEmpty(resetId) && Validate.isEmpty(userId);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(template.forms.textAccountSettings, text)));
    const showWarning = (text: string) => dispatch(ApplicationDialogAction.raise(WarningMessage(template.forms.textAccountSettings, text)));

    const [form, setForm] = React.useState(formDefaultValues);
    const [hasProgress, setHasProgress] = React.useState(false);

    const clearForm = React.useCallback(() => {
        if (!hasProgress) return;
        setHasProgress(false);
        dispatch(UserPasswordUpdateAction.clear());
    }, [hasProgress]);

    React.useEffect(() => {
        if (hasError) {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) {
            dispatch(
                UserPasswordUpdateAction.update({
                    id: userId,
                    resetId: resetId as string,
                    newPassword: form.newPassword,
                })
            );

            return;
        }

        if (hasFinished) {
            clearForm();
            setForm(formDefaultValues);
            showSuccess(template.templates.password.updateSuccess);
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, template]);

    const keyHandler = React.useCallback(
        (event: ReactKeyboardEvent) => {
            if (event.code === "Enter") {
                buttonHandler();
            }
        },
        [form.newPassword, form.verifyPassword]
    );

    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const buttonHandler = React.useCallback(() => {
        let results = ValidateUpdateForm({
            newPassword: form.newPassword,
            verifyPassword: form.verifyPassword,
            content: {
                emailInvalid: template.templates.password.emailInvalid,
                nameInvalid: template.templates.password.nameInvalid,
                surnameInvalid: template.templates.password.surnameInvalid,
                passwordInvalid: template.templates.password.passwordInvalid,
                missingTerms: template.templates.password.missingTerms,
                missingChar: template.templates.password.missingChar,
                missingLargeLetter: template.templates.password.missingLargeLetter,
                missingNumber: template.templates.password.missingNumber,
                missingSmallLetter: template.templates.password.missingSmallLetter,
            },
        });

        if (!Validate.isDefined(results)) {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: results, template: template.templates.password.updateWarning }));
    }, [form, template]);

    return (
        <UpdatePasswordView
            isLoading={password.isLoading}
            progress={hasProgress}
            caption={password.content.caption}
            button={password.content.button}
            newPassword={form.newPassword}
            verifyPassword={form.verifyPassword}
            keyHandler={keyHandler}
            formHandler={formHandler}
            buttonHandler={buttonHandler}
            disableForm={canDisableForm}
            labelNewPassword={password.content.labelNewPassword}
            labelVerifyPassword={password.content.labelVerifyPassword}
        />
    );
};
