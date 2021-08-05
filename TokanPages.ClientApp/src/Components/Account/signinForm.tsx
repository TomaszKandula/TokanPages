import * as React from "react";
import { useDispatch } from "react-redux";
import Validate from "validate.js";
import { IGetSigninFormContent } from "../../Redux/States/getSigninFormContentState";
import { ActionCreators as DialogAction } from "../../Redux/Actions/raiseDialogAction";
import WarningMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { MessageOutWarning } from "../../Shared/textWrappers";
import { ValidateSigninForm } from "../../Shared/validate";
import { SIGNIN_FORM } from "../../Shared/constants";
import SigninFormView from "./signinFormView";

interface IFormDefaultValues 
{
    email: string;
    password: string;
}

const formDefaultValues: IFormDefaultValues =
{
    email: "",
    password: ""
};

export default function SigninForm(props: IGetSigninFormContent) 
{
    const dispatch = useDispatch();
    
    const [form, setForm] = React.useState(formDefaultValues);   
    const [progress, setProgress] = React.useState(false);

    const showWarning = React.useCallback((text: string) => 
        dispatch(DialogAction.raiseDialog(WarningMessage(SIGNIN_FORM, text))), [ dispatch ]);

    // TODO: add action call...

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value});
    
    const buttonHandler = () => 
    {
        let validationResult = ValidateSigninForm( 
        { 
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

    return(
        <SigninFormView bind=
        {{
            isLoading: props.isLoading, 
            caption: props.content.caption,
            button: props.content.button,
            link1: props.content.link1,
            link2: props.content.link2,
            buttonHandler: buttonHandler,
            progress: progress,
            formHandler: formHandler,
            email: form.email,
            password: form.password
        }}/>
    );
}
