import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { OperationStatus } from "../../../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { UserPasswordView } from "./View/userPasswordView";
import Validate from "validate.js";

import { ApplicationDialogAction, UserPasswordUpdateAction } from "../../../../Store/Actions";

import { GetTextWarning, SuccessMessage, WarningMessage } from "../../../../Shared/Services/Utilities";

import { PasswordFormInput, ValidatePasswordForm } from "../../../../Shared/Services/FormValidation";

import { RECEIVED_ERROR_MESSAGE } from "../../../../Shared/constants";

export const UserPassword = (): JSX.Element => {
    const dispatch = useDispatch();

    const template = useSelector((state: ApplicationState) => state.contentTemplates?.content);
    const account = useSelector((state: ApplicationState) => state.contentAccount);
    const update = useSelector((state: ApplicationState) => state.userPasswordUpdate);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = update?.status === OperationStatus.notStarted;
    const hasFinished = update?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const formDefault: PasswordFormInput = {
        oldPassword: "",
        newPassword: "",
        confirmPassword: "",
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
        },
    };

    const [form, setForm] = React.useState(formDefault);
    const [hasProgress, setHasProgress] = React.useState(false);

    const showSuccess = (text: string) =>
        dispatch(ApplicationDialogAction.raise(SuccessMessage(template.forms.textAccountSettings, text)));
    const showWarning = (text: string) =>
        dispatch(ApplicationDialogAction.raise(WarningMessage(template.forms.textAccountSettings, text)));

    const clear = React.useCallback(() => {
        if (!hasProgress) return;

        dispatch(UserPasswordUpdateAction.clear());
        setForm(formDefault);
        setHasProgress(false);
    }, [hasProgress]);

    React.useEffect(() => {
        if (hasError) {
            clear();
            return;
        }

        if (hasNotStarted && hasProgress) {
            dispatch(UserPasswordUpdateAction.update(form));
            return;
        }

        if (hasFinished) {
            clear();
            showSuccess(template.templates.password.updateSuccess);
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, template]);

    const keyHandler = React.useCallback(
        (event: ReactKeyboardEvent) => {
            if (event.code === "Enter") {
                buttonHandler();
            }
        },
        [form.confirmPassword, form.newPassword, form.oldPassword]
    );

    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const buttonHandler = React.useCallback(() => {
        const result = ValidatePasswordForm({
            oldPassword: form.oldPassword,
            newPassword: form.newPassword,
            confirmPassword: form.confirmPassword,
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

        if (!Validate.isDefined(result)) {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: result, template: template.templates.password.updateWarning }));
    }, [form]);

    return (
        <UserPasswordView
            isLoading={account.isLoading}
            oldPassword={form.oldPassword}
            newPassword={form.newPassword}
            confirmPassword={form.confirmPassword}
            keyHandler={keyHandler}
            formProgress={hasProgress}
            formHandler={formHandler}
            buttonHandler={buttonHandler}
            sectionAccessDenied={account.content?.sectionAccessDenied}
            sectionAccountPassword={account.content?.sectionAccountPassword}
        />
    );
};
