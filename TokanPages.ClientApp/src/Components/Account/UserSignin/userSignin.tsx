import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentUserSigninState } from "../../../Store/States";
import { OperationStatus } from "../../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { UserSigninView } from "./View/userSigninView";
import Validate from "validate.js";

import { ApplicationDialogAction, UserSigninAction } from "../../../Store/Actions";

import { GetTextWarning, WarningMessage } from "../../../Shared/Services/Utilities";

import { SigninFormInput, ValidateSigninForm } from "../../../Shared/Services/FormValidation";

import { RECEIVED_ERROR_MESSAGE, SIGNIN_FORM, SIGNIN_WARNING } from "../../../Shared/constants";

const formDefault: SigninFormInput = {
    email: "",
    password: "",
};

export const UserSignin = (props: ContentUserSigninState): JSX.Element => {
    const dispatch = useDispatch();
    const history = useHistory();

    const signin = useSelector((state: ApplicationState) => state.userSignin);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = signin?.status === OperationStatus.notStarted;
    const hasFinished = signin?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState(formDefault);
    const [hasProgress, setHasProgress] = React.useState(false);

    const showWarning = (text: string) => {
        dispatch(ApplicationDialogAction.raise(WarningMessage(SIGNIN_FORM, text)));
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

        showWarning(GetTextWarning({ object: result, template: SIGNIN_WARNING }));
    }, [form]);

    return (
        <UserSigninView
            isLoading={props.isLoading}
            caption={props.content.caption}
            button={props.content.button}
            link1={props.content.link1}
            link2={props.content.link2}
            buttonHandler={buttonHandler}
            progress={hasProgress}
            keyHandler={keyHandler}
            formHandler={formHandler}
            email={form.email}
            password={form.password}
            labelEmail={props.content.labelEmail}
            labelPassword={props.content.labelPassword}
        />
    );
};
