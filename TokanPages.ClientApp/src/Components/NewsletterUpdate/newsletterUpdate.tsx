import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { IconType, OperationStatus } from "../../Shared/enums";
import { ValidateEmailForm } from "../../Shared/Services/FormValidation";
import { ReactChangeEvent } from "../../Shared/types";
import { ApplicationDialogAction, NewsletterUpdateAction } from "../../Store/Actions";
import { RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { NewsletterUpdateView } from "./View/newsletterUpdateView";
import Validate from "validate.js";

export interface ExtendedViewProps {
    className?: string;
    background?: string;
}

export interface NewsletterUpdateProps extends ExtendedViewProps {
    id: string;
}

export const NewsletterUpdate = (props: NewsletterUpdateProps): React.ReactElement => {
    const hasId = props.id === null ? false : true;
    const dispatch = useDispatch();

    const update = useSelector((state: ApplicationState) => state.newsletterUpdate);
    const error = useSelector((state: ApplicationState) => state.applicationError);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const template = data.components.templates;
    const newsletter = data.components.newsletterUpdate;

    const hasNotStarted = update?.status === OperationStatus.notStarted;
    const hasFinished = update?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState({ email: "" });
    const [hasButton, setHasButton] = React.useState(hasId);
    const [hasProgress, setHasProgress] = React.useState(false);

    const clearForm = React.useCallback(() => {
        if (!hasProgress) return;
        setHasProgress(false);
        setHasButton(true);
    }, [hasProgress]);

    React.useEffect(() => {
        if (hasError) {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) {
            dispatch(
                NewsletterUpdateAction.update({
                    id: props.id,
                    email: form.email,
                    isActivated: true,
                    count: 0,
                })
            );

            return;
        }

        if (hasFinished) {
            clearForm();
            setForm({ email: "" });
            dispatch(ApplicationDialogAction.raise({
                title: template.forms.textNewsletter,
                message: template.templates.newsletter.success,
                icon: IconType.info
            }));
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, template]);

    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const buttonHandler = React.useCallback(() => {
        if (props.id == null) {
            return;
        }

        const result = ValidateEmailForm({ email: form.email });
        if (!Validate.isDefined(result)) {
            setHasButton(false);
            setHasProgress(true);
            return;
        }

        dispatch(ApplicationDialogAction.raise({
            title: template.forms.textNewsletter,
            message: template.templates.newsletter.warning,
            validation: result,
            icon: IconType.warning
        }));
    }, [props.id, form, template]);

    return (
        <NewsletterUpdateView
            isLoading={data.isLoading}
            caption={newsletter.caption}
            formHandler={formHandler}
            email={form.email}
            buttonHandler={buttonHandler}
            buttonState={hasButton}
            progress={hasProgress}
            buttonText={newsletter.button}
            labelEmail={newsletter.labelEmail}
            className={props.className}
            background={props.background}
        />
    );
};
