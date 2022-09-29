import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Store/Configuration";
import { DialogAction, SubscriberUpdateAction } from "../../Store/Actions";
import { IGetUpdateSubscriberContent } from "../../Store/States";
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

interface IGetUpdateSubscriberContentExtended extends IGetUpdateSubscriberContent
{
    id: string;
}

export const UpdateSubscriber = (props: IGetUpdateSubscriberContentExtended): JSX.Element =>
{
    const buttonDefaultState = props.id === null ? false : true;
    const dispatch = useDispatch();
    const updateSubscriberState = useSelector((state: IApplicationState) => state.updateSubscriber);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);

    const [form, setForm] = React.useState({email: ""});
    const [buttonState, setButtonState] = React.useState(buttonDefaultState);
    const [progress, setProgress] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(SuccessMessage(UPDATE_SUBSCRIBER, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string)=> dispatch(DialogAction.raiseDialog(WarningMessage(UPDATE_SUBSCRIBER, text))), [ dispatch ]);
    const updateSubscriber = React.useCallback((payload: IUpdateSubscriberDto) => dispatch(SubscriberUpdateAction.updateSubscriber(payload)), [ dispatch ]);

    const clearForm = React.useCallback(() => 
    { 
        if (!progress) return;
        setProgress(false);
        setButtonState(true);
    }, 
    [ progress ]);

    React.useEffect(() => 
    {
        if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clearForm();
            return;
        }

        switch(updateSubscriberState?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (progress) updateSubscriber(
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
    [ progress, raiseErrorState?.defaultErrorMessage, updateSubscriberState?.operationStatus, 
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
