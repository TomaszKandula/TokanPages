import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import { IApplicationState } from "../../../Store/Configuration";
import { IContentUserSignin } from "../../../Store/States";
import { OperationStatus } from "../../../Shared/enums";
import { UserSigninView } from "./View/userSigninView";
import Validate from "validate.js";

import { 
    ApplicationDialogAction, 
    UserSigninAction 
} from "../../../Store/Actions";

import { 
    GetTextWarning, 
    WarningMessage 
} from "../../../Shared/Services/Utilities";

import { 
    IValidateSigninForm, 
    ValidateSigninForm 
} from "../../../Shared/Services/FormValidation";

import { 
    RECEIVED_ERROR_MESSAGE, 
    SIGNIN_FORM, 
    SIGNIN_WARNING 
} from "../../../Shared/constants";

const formDefaultValues: IValidateSigninForm =
{
    email: "",
    password: ""
};

export const UserSignin = (props: IContentUserSignin): JSX.Element =>
{
    const dispatch = useDispatch();
    const history = useHistory();

    const signin = useSelector((state: IApplicationState) => state.userSignin);
    const error = useSelector((state: IApplicationState) => state.applicationError);

    const hasNotStarted = signin?.status === OperationStatus.notStarted;
    const hasFinished = signin?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState(formDefaultValues);
    const [progress, setProgress] = React.useState(false);

    const showWarning = (text: string) => 
    {
        dispatch(ApplicationDialogAction.raise(WarningMessage(SIGNIN_FORM, text)));
    }

    const clearForm = React.useCallback(() => 
    {
        if (!progress) return;
        setProgress(false);
        dispatch(UserSigninAction.clear());
    }, 
    [ progress ]);

    React.useEffect(() => 
    {
        if (hasError)
        {
            clearForm();
            return;
        }

        if (hasNotStarted && progress)
        {
            dispatch(UserSigninAction.signin(
            {
                emailAddress: form.email,
                password: form.password
            }));

            return;
        }

        if (hasFinished)
        {
            clearForm();
            history.push("/");
        }
    }, 
    [ progress, hasError, hasNotStarted, hasFinished ]);

    const keyHandler = (event: React.KeyboardEvent<HTMLInputElement>) => 
    {
        if (event.code === "Enter")
        {
            buttonHandler();
        }
    }

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value});
    }

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

        showWarning(GetTextWarning({ object: validationResult, template: SIGNIN_WARNING }));
    };

    return(
        <UserSigninView bind=
        {{
            isLoading: props.isLoading, 
            caption: props.content.caption,
            button: props.content.button,
            link1: props.content.link1,
            link2: props.content.link2,
            buttonHandler: buttonHandler,
            progress: progress,
            keyHandler: keyHandler,
            formHandler: formHandler,
            email: form.email,
            password: form.password,
            labelEmail: props.content.labelEmail,
            labelPassword: props.content.labelPassword
        }}/>
    );
}
