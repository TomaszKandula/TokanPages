import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Validate from "validate.js";
import { IGetNewsletterContent } from "../../Redux/States/getNewsletterContentState";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators as SubscriberAction } from "../../Redux/Actions/addSubscriberAction";
import { ActionCreators as DialogAction } from "../../Redux/Actions/raiseDialogAction";
import { ValidateEmail } from "../../Shared/validate";
import { OperationStatus } from "../../Shared/enums";
import { NewsletterSuccess, NewsletterWarning } from "../../Shared/textWrappers";
import SuccessMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/successMessage";
import WarningMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { NEWSLETTER, RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { IAddSubscriberDto } from "../../Api/Models";
import NewsletterView from "./newsletterView";

export default function Newsletter(props: IGetNewsletterContent)
{
    const dispatch = useDispatch();
    const addSubscriberState = useSelector((state: IApplicationState) => state.addSubscriber);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);

    const [form, setForm] = React.useState({email: ""});
    const [progress, setProgress] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(SuccessMessage(NEWSLETTER, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(WarningMessage(NEWSLETTER, text))), [ dispatch ]);
    const addSubscriber = React.useCallback((payload: IAddSubscriberDto) => dispatch(SubscriberAction.addSubscriber(payload)), [ dispatch ]);
    const addSubscriberClear = React.useCallback(() => dispatch(SubscriberAction.addSubscriberClear()), [ dispatch ]);

    const clearForm = React.useCallback(() => 
    {
        setProgress(false);
        setForm({email: ""});
        addSubscriberClear();
    }, [ addSubscriberClear ]);
  
    const callAddSubscriber = React.useCallback(() => 
    {
        switch(addSubscriberState?.operationStatus)
        {
            case OperationStatus.inProgress:
                if (raiseErrorState.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
                    clearForm();
            break;

            case OperationStatus.notStarted: 
                if (progress) 
                    addSubscriber({ email: form.email });
            break;
        
            case OperationStatus.hasFinished: 
                clearForm();
                showSuccess(NewsletterSuccess());
            break;
        }
    }, [ addSubscriber, addSubscriberState, clearForm, progress, 
    form, showSuccess, raiseErrorState.defaultErrorMessage ]);

    React.useEffect(() => callAddSubscriber(), [ callAddSubscriber ]);

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

        showWarning(NewsletterWarning(results));
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
        buttonText: props.content?.button
    }}/>);
}
