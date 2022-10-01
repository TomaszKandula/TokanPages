import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Store/Configuration";
import { ApplicationDialog, SubscriberUpdateAction } from "../../Store/Actions";
import { IContentUpdateSubscriber } from "../../Store/States";
import { OperationStatus } from "../../Shared/enums";
import { GetTextWarning } from "../../Shared/Services/Utilities";
import { ValidateEmailForm } from "../../Shared/Services/FormValidation";
import SuccessMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/successMessage";
import WarningMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { IUpdateSubscriberDto } from "../../Api/Models";
import { UpdateSubscriberView } from "./View/updateSubscriberView";
import Validate from "validate.js";

import { 
    NEWSLETTER_SUCCESS, 
    NEWSLETTER_WARNING, 
    RECEIVED_ERROR_MESSAGE, 
    UPDATE_SUBSCRIBER 
} from "../../Shared/constants";

interface IGetUpdateSubscriberContentExtended extends IContentUpdateSubscriber
{
    id: string;
}

export const UpdateSubscriber = (props: IGetUpdateSubscriberContentExtended): JSX.Element =>
{
    const buttonDefaultState = props.id === null ? false : true;
    const dispatch = useDispatch();
    const state = useSelector((state: IApplicationState) => state.subscriberUpdate);
    const error = useSelector((state: IApplicationState) => state.applicationError);

    const [form, setForm] = React.useState({email: ""});
    const [buttonState, setButtonState] = React.useState(buttonDefaultState);
    const [progress, setProgress] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(ApplicationDialog.raise(SuccessMessage(UPDATE_SUBSCRIBER, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string)=> dispatch(ApplicationDialog.raise(WarningMessage(UPDATE_SUBSCRIBER, text))), [ dispatch ]);
    const subscriber = React.useCallback((payload: IUpdateSubscriberDto) => dispatch(SubscriberUpdateAction.update(payload)), [ dispatch ]);

    const clearForm = React.useCallback(() => 
    { 
        if (!progress) return;
        setProgress(false);
        setButtonState(true);
    }, 
    [ progress ]);

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
                if (progress) subscriber(
                { 
                    id: props.id, 
                    email: form.email, 
                    isActivated: true, 
                    count: 0 
                });
            break;

            case OperationStatus.hasFinished:
                clearForm();
                setForm({email: ""});
                showSuccess(NEWSLETTER_SUCCESS);
            break;
        }           
    }, 
    [ progress, error?.defaultErrorMessage, state?.operationStatus, 
        OperationStatus.notStarted, OperationStatus.hasFinished ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
    };

    const buttonHandler = () =>
    {
        if (props.id == null) 
            return;

        let validationResult = ValidateEmailForm({ email: form.email });

        if (!Validate.isDefined(validationResult))
        {
            setButtonState(false);
            setProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: validationResult, template: NEWSLETTER_WARNING }));
    };

    return (<UpdateSubscriberView bind=
    {{
        isLoading: props.isLoading,
        caption: props.content?.caption,
        formHandler: formHandler,
        email: form.email,
        buttonHandler: buttonHandler,
        buttonState: buttonState,
        progress: progress,
        buttonText: props.content?.button,
        labelEmail: props.content.labelEmail
    }}/>);
}
