import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Validate from "validate.js";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators } from "../../Redux/Actions/updateSubscriberAction";
import { IGetUpdateSubscriberContent } from "../../Redux/States/getUpdateSubscriberContentState";
import { alertModalDefault } from "../../Shared/Components/AlertDialog/alertDialog";
import { IconType } from "../../Shared/enums";
import { ValidateEmail } from "../../Shared/validate";
import { NewsletterSuccess, NewsletterWarning } from "../../Shared/textWrappers";
import { IUpdateSubscriberDto } from "../../Api/Models";
import UpdateSubscriberView from "./UpdateSubscriberView";

interface IGetUpdateSubscriberContentExtended extends IGetUpdateSubscriberContent
{
    id: string;
}

export default function UpdateSubscriber(props: IGetUpdateSubscriberContentExtended)
{
    const [form, setForm] = React.useState({email: ""});
    const [buttonState, setButtonState] = React.useState(true);
    const [progress, setProgress] = React.useState(false);
    const [modal, setModal] = React.useState(alertModalDefault);

    const dispatch = useDispatch();
    const updateSubscriberState = useSelector((state: IApplicationState) => state.updateSubscriber);
    const updateSubscriber = React.useCallback((payload: IUpdateSubscriberDto) => dispatch(ActionCreators.updateSubscriber(payload)), [ dispatch ]);
    const executeUpdateSubscriber = React.useCallback(() => 
    {
        if (!updateSubscriberState.isUpdatingSubscriber && progress) 
        {
            setProgress(false);
            setButtonState(true);
            setForm({email: ""});
            setModal(
            { 
                State: true, 
                Title: "Update subscriber", 
                Message: NewsletterSuccess(), 
                Icon: IconType.info 
            });
        }
        
        if (!updateSubscriberState.isUpdatingSubscriber && progress)
            updateSubscriber({ id: props.id, email: form.email, isActivated: true, count: 0 });
    }, 
    [ updateSubscriber, updateSubscriberState, progress, form, props.id ]);

    React.useEffect(() => executeUpdateSubscriber(), [ executeUpdateSubscriber ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
    };

    const modalHandler = () => 
    { 
        setModal(alertModalDefault); 
    };

    const buttonHandler = () =>
    {
        if (props.id == null) 
            return;

        let validationResult = ValidateEmail(form.email);

        if (!Validate.isDefined(validationResult))
        {
            setButtonState(false);
            setProgress(true);
            return;
        }

        setModal(
        { 
            State: true, 
            Title: "Warning", 
            Message: NewsletterWarning(validationResult), 
            Icon: IconType.warning
        });
    };

    return (<UpdateSubscriberView bind =
    {{
        modalState: modal.State,
        modalHandler: modalHandler,
        modalTitle: modal.Title,
        modalMessage: modal.Message,
        modalIcon: modal.Icon,
        isLoading: props.isLoading,
        caption: props.content?.caption,
        formHandler: formHandler,
        email: form.email,
        buttonHandler: buttonHandler,
        buttonState: buttonState,
        progress: progress,
        buttonText: props.content?.button
    }}/>);
}
