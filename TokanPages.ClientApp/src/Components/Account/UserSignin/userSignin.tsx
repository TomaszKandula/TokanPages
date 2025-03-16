import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { ApplicationState } from "../../../Store/Configuration";
import { IconType, OperationStatus } from "../../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { ApplicationDialogAction, UserSigninAction } from "../../../Store/Actions";
import { SigninFormInput, ValidateSigninForm } from "../../../Shared/Services/FormValidation";
import { RECEIVED_ERROR_MESSAGE } from "../../../Shared/constants";
import { UserSigninView } from "./View/userSigninView";
import Validate from "validate.js";

const formDefault: SigninFormInput = {
    email: "",
    password: "",
    content: {
        emailInvalid: "",
        passwordInvalid: "",
    },
};

export interface UserSigninProps {
    className?: string;
    background?: string;
}

export const UserSignin = (props: UserSigninProps): React.ReactElement => {
    const dispatch = useDispatch();
    const history = useHistory();

    const signin = useSelector((state: ApplicationState) => state.userSignin);
    const error = useSelector((state: ApplicationState) => state.applicationError);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage.id);
    const template = data?.components.templates;
    const content = data?.components.accountUserSignin;

    const hasNotStarted = signin?.status === OperationStatus.notStarted;
    const hasFinished = signin?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState(formDefault);
    const [hasProgress, setHasProgress] = React.useState(false);

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
            history.push(`/${languageId}`);
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, languageId]);

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
            content: {
                emailInvalid: template.templates.password.emailInvalid,
                passwordInvalid: template.templates.password.passwordInvalid,
            },
        });

        if (!Validate.isDefined(result)) {
            setHasProgress(true);
            return;
        }

        dispatch(
            ApplicationDialogAction.raise({
                title: template.forms.textSigning,
                message: template.templates.user.signingWarning,
                validation: result,
                icon: IconType.warning,
            })
        );
    }, [form, template]);

    return (
        <UserSigninView
            isLoading={data?.isLoading}
            caption={content?.caption}
            button={content?.button}
            link1={content?.link1}
            link2={content?.link2}
            buttonHandler={buttonHandler}
            progress={hasProgress}
            keyHandler={keyHandler}
            formHandler={formHandler}
            email={form.email}
            password={form.password}
            labelEmail={content?.labelEmail}
            labelPassword={content?.labelPassword}
            className={props.className}
            background={props.background}
        />
    );
};
