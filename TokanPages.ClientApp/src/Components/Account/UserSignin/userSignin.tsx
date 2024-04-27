import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import { ApplicationState } from "../../../Store/Configuration";
import { OperationStatus } from "../../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { UserSigninView } from "./View/userSigninView";
import Validate from "validate.js";

import { ApplicationDialogAction, UserSigninAction } from "../../../Store/Actions";

import { GetTextWarning, WarningMessage } from "../../../Shared/Services/Utilities";

import { SigninFormInput, ValidateSigninForm } from "../../../Shared/Services/FormValidation";

import { RECEIVED_ERROR_MESSAGE } from "../../../Shared/constants";

const formDefault: SigninFormInput = {
    email: "",
    password: "",
};

export const UserSignin = (): JSX.Element => {
    const dispatch = useDispatch();
    const history = useHistory();

    const template = useSelector((state: ApplicationState) => state.contentTemplates?.content);
    const content = useSelector((state: ApplicationState) => state.contentUserSignin);
    const signin = useSelector((state: ApplicationState) => state.userSignin);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = signin?.status === OperationStatus.notStarted;
    const hasFinished = signin?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState(formDefault);
    const [hasProgress, setHasProgress] = React.useState(false);

    const showWarning = (text: string) => {
        dispatch(ApplicationDialogAction.raise(WarningMessage(template.forms.textSigning, text)));
    };

    const clearForm = React.useCallback(() => {
        if (!hasProgress) return;
        setHasProgress(false);
        dispatch(UserSigninAction.clear());
    }, [hasProgress]);

    React.useEffect(() => {
        if (hasError) {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) {
            dispatch(
                UserSigninAction.signin({
                    emailAddress: form.email,
                    password: form.password,
                })
            );

            return;
        }

        if (hasFinished) {
            clearForm();
            history.push("/");
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished]);

    const keyHandler = React.useCallback(
        (event: ReactKeyboardEvent) => {
            if (event.code === "Enter") {
                buttonHandler();
            }
        },
        [form.email, form.password]
    );

    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const buttonHandler = React.useCallback(() => {
        const result = ValidateSigninForm({
            email: form.email,
            password: form.password,
        });

        if (!Validate.isDefined(result)) {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: result, template: template.templates.user.signingWarning }));
    }, [form, template]);

    return (
        <UserSigninView
            isLoading={content?.isLoading}
            caption={content?.content?.caption}
            button={content?.content?.button}
            link1={content?.content?.link1}
            link2={content?.content?.link2}
            buttonHandler={buttonHandler}
            progress={hasProgress}
            keyHandler={keyHandler}
            formHandler={formHandler}
            email={form.email}
            password={form.password}
            labelEmail={content?.content?.labelEmail}
            labelPassword={content?.content?.labelPassword}
        />
    );
};
