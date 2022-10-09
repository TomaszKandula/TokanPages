import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { IContentUserSignup } from "../../../Store/States";
import { ApplicationDialogAction, UserSignupAction } from "../../../Store/Actions";
import { IAddUserDto } from "../../../Api/Models";
import { GetTextWarning, SuccessMessage, WarningMessage } from "../../../Shared/Services/Utilities";
import { IValidateSignupForm, ValidateSignupForm } from "../../../Shared/Services/FormValidation";
import { OperationStatus } from "../../../Shared/enums";
import { UserSignupView } from "./View/userSignupView";
import Validate from "validate.js";

import {
    RECEIVED_ERROR_MESSAGE, 
    SIGNUP_FORM, 
    SIGNUP_SUCCESS, 
    SIGNUP_WARNING 
} from "../../../Shared/constants";

const formDefaultValues: IValidateSignupForm =
{
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    terms: false
};

export const UserSignup = (props: IContentUserSignup): JSX.Element => 
{
    const dispatch = useDispatch();
    const state = useSelector((state: IApplicationState) => state.userSignup);
    const error = useSelector((state: IApplicationState) => state.applicationError);

    const [form, setForm] = React.useState(formDefaultValues);   
    const [progress, setProgress] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(SIGNUP_FORM, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string) => dispatch(ApplicationDialogAction.raise(WarningMessage(SIGNUP_FORM, text))), [ dispatch ]);
    const signup = React.useCallback((payload: IAddUserDto) => dispatch(UserSignupAction.signup(payload)), [ dispatch ]);
    const clear = React.useCallback(() => dispatch(UserSignupAction.clear()), [ dispatch ]);

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

        switch(state?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (progress)
                {
                    const userAlias: string = `${form.firstName.substring(0, 2)}${form.lastName.substring(0, 3)}`;
                    signup(
                    {
                        userAlias: userAlias,
                        firstName: form.firstName,
                        lastName: form.lastName,
                        emailAddress: form.email,
                        password: form.password
                    });
                }
            break;

            case OperationStatus.hasFinished:
                clearForm();
                setForm(formDefaultValues);
                showSuccess(SIGNUP_SUCCESS);
            break;
        }
    }, 
    [ progress, error?.defaultErrorMessage, state?.operationStatus, 
        OperationStatus.notStarted, OperationStatus.hasFinished ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
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
            setProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: validationResult, template: SIGNUP_WARNING }));
    };

    return (<UserSignupView bind=
    {{
        isLoading: props.isLoading,
        caption: props.content.caption,
        consent: props.content.consent,
        button: props.content.button,
        link: props.content.link,
        buttonHandler: buttonHandler,
        formHandler: formHandler,
        progress: progress,
        firstName: form.firstName,
        lastName: form.lastName,
        email: form.email,
        password: form.password,
        terms: form.terms,
        labelFirstName: props.content.labelFirstName,
        labelLastName: props.content.labelLastName,
        labelEmail: props.content.labelEmail,
        labelPassword: props.content.labelPassword
    }}/>);
}
