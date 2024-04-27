import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { OperationStatus } from "../../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { UserSignupView } from "./View/userSignupView";
import Validate from "validate.js";

import { ApplicationDialogAction, UserSignupAction } from "../../../Store/Actions";

import { GetTextWarning, SuccessMessage, WarningMessage } from "../../../Shared/Services/Utilities";

import { SignupFormInput, ValidateSignupForm } from "../../../Shared/Services/FormValidation";

import { RECEIVED_ERROR_MESSAGE } from "../../../Shared/constants";

const defaultForm: SignupFormInput = {
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    terms: false,
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

export const UserSignup = (): JSX.Element => {
    const dispatch = useDispatch();

    const template = useSelector((state: ApplicationState) => state.contentTemplates?.content); 
    const content = useSelector((state: ApplicationState) => state.contentUserSignup);
    const signup = useSelector((state: ApplicationState) => state.userSignup);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = signup?.status === OperationStatus.notStarted;
    const hasFinished = signup?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState(defaultForm);
    const [hasProgress, setHasProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(template.forms.textSignup, text)));
    const showWarning = (text: string) => dispatch(ApplicationDialogAction.raise(WarningMessage(template.forms.textSignup, text)));

    const clearForm = React.useCallback(() => {
        if (!hasProgress) return;
        setHasProgress(false);
        dispatch(UserSignupAction.clear());
    }, [hasProgress]);

    React.useEffect(() => {
        if (hasError) {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) {
            const userAlias: string = `${form.firstName.substring(0, 2)}${form.lastName.substring(0, 3)}`;
            dispatch(
                UserSignupAction.signup({
                    userAlias: userAlias,
                    firstName: form.firstName,
                    lastName: form.lastName,
                    emailAddress: form.email,
                    password: form.password,
                })
            );

            return;
        }

        if (hasFinished) {
            clearForm();
            setForm(defaultForm);
            showSuccess(template.templates.user.signupSuccess);
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, template]);

    const keyHandler = React.useCallback(
        (event: ReactKeyboardEvent) => {
            if (event.code === "Enter") {
                buttonHandler();
            }
        },
        [form.email, form.firstName, form.lastName, form.password]
    );

    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            if (event.currentTarget.name !== "terms") {
                setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
                return;
            }

            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.checked });
        },
        [form]
    );

    const buttonHandler = React.useCallback(() => {
        const result = ValidateSignupForm({
            firstName: form.firstName,
            lastName: form.lastName,
            email: form.email,
            password: form.password,
            terms: form.terms,
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
        }
        });

        if (!Validate.isDefined(result)) {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: result, template: template.templates.user.signupWarning }));
    }, [form, template]);

    return (
        <UserSignupView
            isLoading={content?.isLoading}
            caption={content?.content?.caption}
            warning={content?.content?.warning}
            consent={content?.content?.consent}
            button={content?.content?.button}
            link={content?.content?.link}
            buttonHandler={buttonHandler}
            keyHandler={keyHandler}
            formHandler={formHandler}
            progress={hasProgress}
            firstName={form.firstName}
            lastName={form.lastName}
            email={form.email}
            password={form.password}
            terms={form.terms}
            labelFirstName={content?.content?.labelFirstName}
            labelLastName={content?.content?.labelLastName}
            labelEmail={content?.content?.labelEmail}
            labelPassword={content?.content?.labelPassword}
        />
    );
};
