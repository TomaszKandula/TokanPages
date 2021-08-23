import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IGetUserSignupContent } from "../../Redux/States/Content/getUserSignupContentState";
import { ActionCreators as DialogAction } from "../../Redux/Actions/raiseDialogAction";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators } from "../../Redux/Actions/Users/signupUserAction";
import { IAddUserDto } from "../../Api/Models";
import SuccessMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/successMessage";
import WarningMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { ProduceWarningText } from "../../Shared/textWrappers";
import { RECEIVED_ERROR_MESSAGE, SIGNUP_FORM, SIGNUP_SUCCESS, SIGNUP_WARNING } from "../../Shared/constants";
import { IValidateSignupForm, ValidateSignupForm } from "../../Shared/validate";
import { OperationStatus } from "../../Shared/enums";
import UserSignupView from "./userSignupView";
import Validate from "validate.js";

const formDefaultValues: IValidateSignupForm =
{
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    terms: false
};

const UserSignup = (props: IGetUserSignupContent): JSX.Element => 
{
    const dispatch = useDispatch();
    const signupUserState = useSelector((state: IApplicationState) => state.signupUser);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);

    const [form, setForm] = React.useState(formDefaultValues);   
    const [progress, setProgress] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(SuccessMessage(SIGNUP_FORM, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(WarningMessage(SIGNUP_FORM, text))), [ dispatch ]);
    const signupUser = React.useCallback((payload: IAddUserDto) => dispatch(ActionCreators.signup(payload)), [ dispatch ]);

    const clearForm = React.useCallback(() => 
    {
        setProgress(false);
        setForm(formDefaultValues);
    }, [  ]);

    const callSignupUser = React.useCallback(() => 
    {
        switch(signupUserState?.operationStatus)
        {
            case OperationStatus.inProgress:
                if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
                    clearForm();
            break;

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
                showSuccess(SIGNUP_SUCCESS);
            break;
        }

    }, [ progress, clearForm, form, raiseErrorState, signupUser, signupUserState, showSuccess ]);

    React.useEffect(() => callSignupUser(), [ callSignupUser ]);

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

        showWarning(ProduceWarningText(validationResult, SIGNUP_WARNING));
    };

    return (<UserSignupView bind=
    {{
        isLoading: props.isLoading,
        caption: props.content.caption,
        label: props.content.label,
        button: props.content.button,
        link: props.content.link,
        buttonHandler: buttonHandler,
        formHandler: formHandler,
        progress: progress,
        firstName: form.firstName,
        lastName: form.lastName,
        email: form.email,
        password: form.password,
        terms: form.terms
    }}/>);
}

export default UserSignup;
