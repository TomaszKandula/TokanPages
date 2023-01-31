import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentUserSignupState } from "../../../Store/States";
import { OperationStatus } from "../../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { UserSignupView } from "./View/userSignupView";
import Validate from "validate.js";

import { 
    ApplicationDialogAction, 
    UserSignupAction 
} from "../../../Store/Actions";

import { 
    GetTextWarning, 
    SuccessMessage, 
    WarningMessage 
} from "../../../Shared/Services/Utilities";

import { 
    SignupFormInput, 
    ValidateSignupForm 
} from "../../../Shared/Services/FormValidation";

import {
    RECEIVED_ERROR_MESSAGE, 
    SIGNUP_FORM, 
    SIGNUP_SUCCESS, 
    SIGNUP_WARNING 
} from "../../../Shared/constants";

const formDefaultValues: SignupFormInput =
{
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    terms: false
};

export const UserSignup = (props: ContentUserSignupState): JSX.Element => 
{
    const dispatch = useDispatch();
    const signup = useSelector((state: ApplicationState) => state.userSignup);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = signup?.status === OperationStatus.notStarted;
    const hasFinished = signup?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState(formDefaultValues);   
    const [hasProgress, setHasProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(SIGNUP_FORM, text)));
    const showWarning = (text: string) => dispatch(ApplicationDialogAction.raise(WarningMessage(SIGNUP_FORM, text)));

    const clearForm = React.useCallback(() => 
    {
        if (!hasProgress) return;
        setHasProgress(false);
        dispatch(UserSignupAction.clear());
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
            const userAlias: string = `${form.firstName.substring(0, 2)}${form.lastName.substring(0, 3)}`;
            dispatch(UserSignupAction.signup(
            {
                userAlias: userAlias,
                firstName: form.firstName,
                lastName: form.lastName,
                emailAddress: form.email,
                password: form.password
            }));

            return;
        }

        if (hasFinished) 
        {
            clearForm();
            setForm(formDefaultValues);
            showSuccess(SIGNUP_SUCCESS);
        }
    }, 
    [ hasProgress, hasError, hasNotStarted, hasFinished ]);

    const keyHandler = (event: ReactKeyboardEvent) => 
    {
        if (event.code === "Enter")
        {
            buttonHandler();
        }
    }

    const formHandler = (event: ReactChangeEvent) => 
    {
        if (event.currentTarget.name !== "terms")
        {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value});    
            return;
        }

        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.checked});
    };

    const buttonHandler = () => 
    {
        let validationResult = ValidateSignupForm( 
        { 
            firstName: form.firstName,
            lastName: form.lastName,
            email: form.email, 
            password: form.password,
            terms: form.terms
        });

        if (!Validate.isDefined(validationResult))
        {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: validationResult, template: SIGNUP_WARNING }));
    };

    return (<UserSignupView
        isLoading={props.isLoading}
        caption={props.content.caption}
        consent={props.content.consent}
        button={props.content.button}
        link={props.content.link}
        buttonHandler={buttonHandler}
        keyHandler={keyHandler}
        formHandler={formHandler}
        progress={hasProgress}
        firstName={form.firstName}
        lastName={form.lastName}
        email={form.email}
        password={form.password}
        terms={form.terms}
        labelFirstName={props.content.labelFirstName}
        labelLastName={props.content.labelLastName}
        labelEmail={props.content.labelEmail}
        labelPassword={props.content.labelPassword}
    />);
}
