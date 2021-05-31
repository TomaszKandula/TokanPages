import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Validate from "validate.js";
import { IGetNewsletterContent } from "../../Redux/States/getNewsletterContentState";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators } from "../../Redux/Actions/addSubscriberAction";
import { ValidateEmail } from "../../Shared/validate";
import { OperationStatus, IconType } from "../../Shared/enums";
import { alertModalDefault } from "../../Shared/Components/applicationDialogBox/applicationDialogBox";
import { NewsletterSuccess, NewsletterWarning, NewsletterError } from "../../Shared/textWrappers";
import { IAddSubscriberDto } from "../../Api/Models";
import NewsletterView from "./newsletterView";

export default function Newsletter(props: IGetNewsletterContent)
{
    const [form, setForm] = React.useState({email: ""});
    const [modal, setModal] = React.useState(alertModalDefault);
    const [progress, setProgress] = React.useState(false);

    const showSuccess = (text: string) => setModal({ State: true, Title: "Newsletter", Message: text, Icon: IconType.info });
    const showWarning = (text: string) => setModal({ State: true, Title: "Warning", Message: text, Icon: IconType.warning });
    const showError = (text: string) => setModal({ State: true, Title: "Error", Message: text, Icon: IconType.error });

    const dispatch = useDispatch();
    const addSubscriberState = useSelector((state: IApplicationState) => state.addSubscriber);
    const addSubscriber = React.useCallback((payload: IAddSubscriberDto) => dispatch(ActionCreators.addSubscriber(payload)), [ dispatch ]);
    const addSubscriberClear = React.useCallback(() => dispatch(ActionCreators.addSubscriberClear()), [ dispatch ]);

    const clearForm = React.useCallback(() => 
    {
        setProgress(false);
        setForm({email: ""});
        addSubscriberClear();
    }, [ setProgress, setForm, addSubscriberClear ]);
    
    React.useEffect(() => 
    { 
        switch(addSubscriberState?.operationStatus)
        {
            case OperationStatus.notStarted: 
                if (progress) 
                    addSubscriber({ email: form.email });
            break;
        
            case OperationStatus.hasFinished: 
                showSuccess(NewsletterSuccess());
                clearForm();
            break;
        
            case OperationStatus.hasFailed: 
                showError(NewsletterError(addSubscriberState?.attachedErrorObject));
                clearForm();
            break;
        }
    }, 
    [ addSubscriber, addSubscriberState, clearForm, progress, form ]);

    const modalHandler = () => setModal({ ...modal, State: false});
    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
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
        modalState: modal.State,
        modalHandler: modalHandler,
        modalTitle: modal.Title,
        modalMessage: modal.Message,
        modalIcon: modal.Icon,
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
