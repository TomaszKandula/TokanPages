import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { IGetUserSignupContent } from "../../../Store/States";
import { DialogAction, UserSignupAction } from "../../../Store/Actions";
import { IAddUserDto } from "../../../Api/Models";
import SuccessMessage from "../../../Shared/Components/ApplicationDialogBox/Helpers/successMessage";
import WarningMessage from "../../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { GetTextWarning } from "../../../Shared/Services/Utilities";
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

export const UserSignup = (props: IGetUserSignupContent): JSX.Element => 
{
    const dispatch = useDispatch();
    const signupUserState = useSelector((state: IApplicationState) => state.signupUser);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);

    const [form, setForm] = React.useState(formDefaultValues);   
    const [progress, setProgress] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(SuccessMessage(SIGNUP_FORM, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(WarningMessage(SIGNUP_FORM, text))), [ dispatch ]);
    const signupUser = React.useCallback((payload: IAddUserDto) => dispatch(UserSignupAction.signup(payload)), [ dispatch ]);
    const clearUser = React.useCallback(() => dispatch(UserSignupAction.clear()), [ dispatch ]);

    const clearForm = React.useCallback(() => 
    {
        if (!progress) return;
        setProgress(false);
        clearUser();
    }, 
    [ progress, clearUser ]);

    React.useEffect(() => 
    {
        if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clearForm();
            return;
        }

        switch(signupUserState?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (progress)
                {
                    const userAlias: string = `${form.firstName.substring(0, 2)}${form.lastName.substring(0, 3)}`;
                    signupUser(
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
    [ progress, raiseErrorState?.defaultErrorMessage, signupUserState?.operationStatus, 
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
