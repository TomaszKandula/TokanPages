import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import { IGetUserSigninContent } from "../../Redux/States/Content/getUserSigninContentState";
import { ActionCreators as DialogAction } from "../../Redux/Actions/raiseDialogAction";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators } from "../../Redux/Actions/Users/signinUserAction";
import { IAuthenticateUserDto } from "../../Api/Models";
import WarningMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { ProduceWarningText } from "../../Shared/textWrappers";
import { IValidateSigninForm, ValidateSigninForm } from "../../Shared/validate";
import { OperationStatus } from "../../Shared/enums";
import { RECEIVED_ERROR_MESSAGE, SIGNIN_FORM, SIGNIN_WARNING } from "../../Shared/constants";
import UserSigninView from "./userSigninView";
import Validate from "validate.js";

const formDefaultValues: IValidateSigninForm =
{
    email: "",
    password: ""
};

const UserSignin = (props: IGetUserSigninContent): JSX.Element =>
{
    const dispatch = useDispatch();
    const history = useHistory();
    const signinUserState = useSelector((state: IApplicationState) => state.signinUser);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);

    const [form, setForm] = React.useState(formDefaultValues);   
    const [progress, setProgress] = React.useState(false);

    const showWarning = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(WarningMessage(SIGNIN_FORM, text))), [ dispatch ]);
    const signinUser = React.useCallback((payload: IAuthenticateUserDto) => dispatch(ActionCreators.signin(payload)), [ dispatch ]);
    const clearUser = React.useCallback(() => dispatch(ActionCreators.clear()), [ dispatch ]);

    const clearForm = React.useCallback(() => 
    {
        setProgress(false);
        setForm(formDefaultValues);
    }, [  ]);
    
    const callSigninUser = React.useCallback(() => 
    {
        switch(signinUserState?.operationStatus)
        {
            case OperationStatus.inProgress:
                if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
                {
                    clearUser();
                    clearForm();
                }
            break;

            case OperationStatus.notStarted:
                if (progress)
                    signinUser(
                    {
                        emailAddress: form.email,
                        password: form.password
                    });
            break;

            case OperationStatus.hasFinished:
                clearUser();
                clearForm();
                history.push("/");
            break;
        }

    }, [ progress, clearForm, form.email, form.password, history, raiseErrorState, signinUser, signinUserState ]);

    React.useEffect(() => callSigninUser(), [ callSigninUser ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value});
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

        showWarning(ProduceWarningText(validationResult, SIGNIN_WARNING));
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
            formHandler: formHandler,
            email: form.email,
            password: form.password
        }}/>
    );
}

export default UserSignin;
