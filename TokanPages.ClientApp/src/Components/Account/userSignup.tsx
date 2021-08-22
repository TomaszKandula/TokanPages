import * as React from "react";
import { useDispatch } from "react-redux";
import { IGetUserSignupContent } from "../../Redux/States/Content/getUserSignupContentState";
import WarningMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { ActionCreators as DialogAction } from "../../Redux/Actions/raiseDialogAction";
import UserSignupView from "./userSignupView";
import Validate from "validate.js";
import { MessageOutWarning } from "../../Shared/textWrappers";
import { SIGNUP_FORM } from "../../Shared/constants";
import { IValidateSignupForm, ValidateSignupForm } from "../../Shared/validate";

const formDefaultValues: IValidateSignupForm =
{
    firstName: "",
    lastName: "",
    email: "",
    password: ""
};

const UserSignup = (props: IGetUserSignupContent): JSX.Element => 
{
    const dispatch = useDispatch();
    const [form, setForm] = React.useState(formDefaultValues);   
    const [progress, setProgress] = React.useState(false);

    const showWarning = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(WarningMessage(SIGNUP_FORM, text))), [ dispatch ]);

    // TODO: action call here...

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value});
    const buttonHandler = () => 
    {
        let validationResult = ValidateSignupForm( 
        { 
            firstName: form.firstName,
            lastName: form.lastName,
            email: form.email, 
            password: form.password
        });

        if (!Validate.isDefined(validationResult))
        {
            setProgress(true);
            return;
        }

        showWarning(MessageOutWarning(validationResult));
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
        password: form.password
    }}/>);
}

export default UserSignup;
