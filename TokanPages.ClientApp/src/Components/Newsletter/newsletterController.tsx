import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Store/Configuration";
import { IGetNewsletterContent } from "../../Store/States";
import { SubscriberAddAction } from "../../Store/Actions";
import { ApplicationDialog } from "../../Store/Actions";
import { OperationStatus } from "../../Shared/enums";
import { GetTextWarning } from "../../Shared/Services/Utilities";
import { ValidateEmailForm } from "../../Shared/Services/FormValidation";
import SuccessMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/successMessage";
import WarningMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { NEWSLETTER, NEWSLETTER_SUCCESS, NEWSLETTER_WARNING, RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { IAddSubscriberDto } from "../../Api/Models";
import { NewsletterView } from "./View/newsletterView";
import Validate from "validate.js";

export const Newsletter = (props: IGetNewsletterContent): JSX.Element =>
{
    const dispatch = useDispatch();
    const addSubscriberState = useSelector((state: IApplicationState) => state.addSubscriber);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);

    const [form, setForm] = React.useState({email: ""});
    const [progress, setProgress] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(ApplicationDialog.raise(SuccessMessage(NEWSLETTER, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string) => dispatch(ApplicationDialog.raise(WarningMessage(NEWSLETTER, text))), [ dispatch ]);
    const addSubscriber = React.useCallback((payload: IAddSubscriberDto) => dispatch(SubscriberAddAction.add(payload)), [ dispatch ]);
    const addSubscriberClear = React.useCallback(() => dispatch(SubscriberAddAction.clear()), [ dispatch ]);

    const clearForm = React.useCallback(() => 
    {
        if (!progress) return;
        setProgress(false);
        addSubscriberClear();
    }, 
    [ progress, addSubscriberClear ]);

    React.useEffect(() => 
    {
        if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clearForm();
            return;
        }

        switch(addSubscriberState?.operationStatus)
        {
            case OperationStatus.notStarted: 
                if (progress) addSubscriber({ email: form.email });
            break;
        
            case OperationStatus.hasFinished: 
                clearForm();
                setForm({email: ""});
                showSuccess(NEWSLETTER_SUCCESS);
            break;
        }
    }, 
    [ progress, raiseErrorState?.defaultErrorMessage, addSubscriberState?.operationStatus, 
        OperationStatus.notStarted, OperationStatus.hasFinished ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    { 
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); 
    };
    
    const buttonHandler = () =>
    {
        let results = ValidateEmailForm({ email: form.email });

        if (!Validate.isDefined(results))
        {
            setProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: results, template: NEWSLETTER_WARNING }));
    };

    return (<NewsletterView bind=
    {{
        isLoading: props.isLoading,
        caption: props.content?.caption,
        text: props.content?.text,
        formHandler: formHandler,
        email: form.email,
        buttonHandler: buttonHandler,
        progress: progress,
        buttonText: props.content?.button,
        labelEmail: props.content?.labelEmail
    }}/>);
}
