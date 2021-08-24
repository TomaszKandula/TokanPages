import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Redux/applicationState";
import { IGetResetPasswordContent } from "../../Redux/States/Content/getResetPasswordContentState";
import { ActionCreators as DialogAction } from "../../Redux/Actions/raiseDialogAction";
import WarningMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { ValidateEmail } from "../../Shared/validate";
import { ProduceWarningText } from "../../Shared/textWrappers";
import { RESET_FORM, RESET_PASSWORD_WARNING } from "../../Shared/constants";
import ResetPasswordView from "./resetPasswordView";
import Validate from "validate.js";

const ResetPassword = (props: IGetResetPasswordContent): JSX.Element =>
{
    const dispatch = useDispatch();
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);
    
    const showWarning = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(WarningMessage(RESET_FORM, text))), [ dispatch ]);

    const [form, setForm] = React.useState({email: ""});
    const [progress, setProgress] = React.useState(false);

    // TODO: add action call...

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    { 
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); 
    };

    const buttonHandler = () =>
    {
        let results = ValidateEmail(form.email);

        if (!Validate.isDefined(results))
        {
            setProgress(true);
            return;
        }

        showWarning(ProduceWarningText(results, RESET_PASSWORD_WARNING));
    };

    return (
        <ResetPasswordView bind=
        {{
            isLoading: props.isLoading,
            progress: progress,
            caption: props.content.caption,
            button: props.content.button,
            formHandler: formHandler,
            buttonHandler: buttonHandler
        }}/>
    );
}

export default ResetPassword;
