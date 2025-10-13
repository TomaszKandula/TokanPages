import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/Types";
import { ApplicationState } from "../../../Store/Configuration";
import { IconType, OperationStatus } from "../../../Shared/enums";
import { ValidateEmailForm } from "../../../Shared/Services/FormValidation";
import { NewsletterAddAction, ApplicationDialogAction } from "../../../Store/Actions";
import { RECEIVED_ERROR_MESSAGE } from "../../../Shared/constants";
import { NewsletterSectionView } from "./View/newsletterSectionView";
import Validate from "validate.js";

interface NewsletterProps {
    className?: string;
}

export const NewsletterSection = (props: NewsletterProps): React.ReactElement => {
    const dispatch = useDispatch();

    const add = useSelector((state: ApplicationState) => state.newsletterAdd);
    const error = useSelector((state: ApplicationState) => state.applicationError);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const template = data?.components?.templates;
    const newsletter = data?.components?.sectionNewsletter;

    const hasNotStarted = add?.status === OperationStatus.notStarted;
    const hasFinished = add?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState({ email: "" });
    const [hasProgress, setHasProgress] = React.useState(false);

    const clearForm = React.useCallback(() => {
        if (!hasProgress) return;
        setHasProgress(false);
        dispatch(NewsletterAddAction.clear());
    }, [hasProgress]);

    React.useEffect(() => {
        if (hasError) {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) {
            dispatch(NewsletterAddAction.add({ email: form.email }));
            return;
        }

        if (hasFinished) {
            clearForm();
            setForm({ email: "" });
            dispatch(
                ApplicationDialogAction.raise({
                    title: template.forms.textNewsletter,
                    message: template.templates.newsletter.success,
                    icon: IconType.info,
                    buttons: {
                        primaryButton: {
                            label: "OK",
                        },
                    },
                })
            );
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, template]);

    const keyHandler = React.useCallback(
        (event: ReactKeyboardEvent) => {
            if (event.code === "Enter") {
                buttonHandler();
            }
        },
        [form.email]
    );

    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const buttonHandler = React.useCallback(() => {
        const result = ValidateEmailForm({ email: form.email });

        if (!Validate.isDefined(result)) {
            setHasProgress(true);
            return;
        }

        dispatch(
            ApplicationDialogAction.raise({
                title: template.forms.textNewsletter,
                message: template.templates.newsletter.warning,
                validation: result,
                icon: IconType.warning,
                buttons: {
                    primaryButton: {
                        label: "OK",
                    },
                },
            })
        );
    }, [form, template]);

    return (
        <NewsletterSectionView
            isLoading={data?.isLoading}
            caption={newsletter?.caption}
            text={newsletter?.text}
            keyHandler={keyHandler}
            formHandler={formHandler}
            email={form.email}
            buttonHandler={buttonHandler}
            progress={hasProgress}
            buttonText={newsletter?.button}
            labelEmail={newsletter?.labelEmail}
            className={props.className}
        />
    );
};
