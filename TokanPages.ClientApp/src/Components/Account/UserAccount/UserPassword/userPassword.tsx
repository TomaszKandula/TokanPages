import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { IconType, OperationStatus } from "../../../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { ApplicationDialogAction, UserPasswordUpdateAction } from "../../../../Store/Actions";
import { PasswordFormInput, ValidatePasswordForm } from "../../../../Shared/Services/FormValidation";
import { RECEIVED_ERROR_MESSAGE } from "../../../../Shared/constants";
import { useDimensions } from "../../../../Shared/Hooks";
import { UserPasswordView } from "./View/userPasswordView";
import Validate from "validate.js";

export interface UserPasswordProps {
    className?: string;
}

export const UserPassword = (props: UserPasswordProps): React.ReactElement => {
    const dispatch = useDispatch();
    const media = useDimensions();

    const update = useSelector((state: ApplicationState) => state.userPasswordUpdate);
    const error = useSelector((state: ApplicationState) => state.applicationError);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const template = data.components.templates;
    const account = data.components.accountSettings;

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
            dispatch(
                ApplicationDialogAction.raise({
                    title: template.forms.textAccountSettings,
                    message: template.templates.password.updateSuccess,
                    icon: IconType.info,
                    buttons: {
                        primaryButton: {
                            label: "OK",
                        },
                    },
                })
            );
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
                missingLargeLetter: template.templates.password.missingLargeLetter,
                missingNumber: template.templates.password.missingNumber,
                missingSmallLetter: template.templates.password.missingSmallLetter,
            },
        });

        if (!Validate.isDefined(result)) {
            setHasProgress(true);
            return;
        }

        dispatch(
            ApplicationDialogAction.raise({
                title: template.forms.textAccountSettings,
                message: template.templates.password.updateWarning,
                validation: result,
                icon: IconType.warning,
                buttons: {
                    primaryButton: {
                        label: "OK",
                    },
                },
            })
        );
    }, [form]);

    return (
        <UserPasswordView
            isLoading={data.isLoading}
            isMobile={media.isMobile}
            oldPassword={form.oldPassword}
            newPassword={form.newPassword}
            confirmPassword={form.confirmPassword}
            keyHandler={keyHandler}
            formProgress={hasProgress}
            formHandler={formHandler}
            buttonHandler={buttonHandler}
            sectionAccountPassword={account.sectionAccountPassword}
            className={props.className}
        />
    );
};
