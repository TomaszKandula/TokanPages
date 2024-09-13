import * as React from "react";
import { BusinessFormView } from "./View/businessFormView";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ApplicationDialogAction, ApplicationMessageAction } from "../../Store/Actions";
import { OperationStatus } from "../../Shared/enums";
import { RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { SuccessMessage } from "../../Shared/Services/Utilities";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../Shared/types";

export interface BusinessFormProps {
    pt?: number;
    pb?: number;
    hasCaption?: boolean;
    hasIcon?: boolean;
    hasShadow?: boolean;
    background?: React.CSSProperties;
}

const internalSubjectText = "Incoming Business Inquiry";
const internalMessageText = "Please check the internal payload for more details.";
const formDefault = {
    companyText: "",
    firstNameText: "",
    lastNameText: "",
    emailText: "",
    phoneText: "",
    descriptionText: "",
}

export const BusinessForm = (props: BusinessFormProps): JSX.Element => {
    const dispatch = useDispatch();

    const content = useSelector((state: ApplicationState) => state.contentTemplates?.content);
    const businessForm = useSelector((state: ApplicationState) => state.contentBusinessForm);
    const email = useSelector((state: ApplicationState) => state.applicationEmail);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = email?.status === OperationStatus.notStarted;
    const hasFinished = email?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState(formDefault);
    const [hasProgress, setHasProgress] = React.useState(false);

    const showSuccess = (text: string) =>
        dispatch(ApplicationDialogAction.raise(SuccessMessage(content.forms.textBusinessForm, text)));
    // const showWarning = (text: string) =>
    //     dispatch(ApplicationDialogAction.raise(WarningMessage(content.forms.textBusinessForm, text)));

    const clearForm = React.useCallback(() => {
        if (!hasProgress) return;
        setHasProgress(false);
        dispatch(ApplicationMessageAction.clear());
    }, [hasProgress]);

    React.useEffect(() => {
        if (hasError) {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) {
            dispatch(
                ApplicationMessageAction.send({
                    firstName: form.firstNameText,
                    lastName: form.lastNameText,
                    userEmail: form.emailText,
                    emailFrom: form.emailText,
                    emailTos: [form.emailText],
                    subject: internalSubjectText,
                    message: internalMessageText,
                    businessData: JSON.stringify(form)
                })
            );

            return;
        }

        if (hasFinished) {
            clearForm();
            setForm(formDefault);
            showSuccess(content.templates.messageOut.success);
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, content]);

    const keyHandler = React.useCallback(
        (event: ReactKeyboardEvent) => {
            if (event.code === "Enter") {
                buttonHandler();
            }
        },
        [
            form.companyText, 
            form.descriptionText, 
            form.emailText, 
            form.firstNameText, 
            form.lastNameText, 
            form.phoneText,
        ]
    );

    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const techHandler = React.useCallback((value: { value: string; key: number }, isChecked: boolean) => {

        console.log(value);
        console.log(isChecked);

    }, [  ]);

    const buttonHandler = React.useCallback(() => {
        // const result = ValidateContactForm({
        //     firstName: form.firstName,
        //     lastName: form.lastName,
        //     email: form.email,
        //     subject: form.subject,
        //     message: form.message,
        //     terms: form.terms,
        // });

        // if (!Validate.isDefined(result)) {
        //     setHasProgress(true);
        //     return;
        // }

        //showWarning(GetTextWarning({ object: result, template: content.templates.messageOut.warning }));

        console.log(form);

    }, [form, content]);

    return(
        <BusinessFormView
            isLoading={businessForm.isLoading}
            caption={businessForm.content.caption}
            progress={hasProgress}
            buttonText={businessForm.content.buttonText}
            keyHandler={keyHandler}
            formHandler={formHandler}
            buttonHandler={buttonHandler}
            techHandler={techHandler}
            companyText={form.companyText}
            companyLabel={businessForm.content.companyLabel}
            firstNameText={form.firstNameText}
            firstNameLabel={businessForm.content.firstNameLabel}
            lastNameText={form.lastNameText}
            lastNameLabel={businessForm.content.lastNameLabel}
            emailText={form.emailText}
            emailLabel={businessForm.content.emailLabel}
            phoneText={form.phoneText}
            phoneLabel={businessForm.content.phoneLabel}
            techLabel={businessForm.content.techLabel}
            techItems={businessForm.content.techItems}
            description={{
                text: form.descriptionText,
                label: businessForm.content.description.label,
                multiline: businessForm.content.description.multiline,
                rows: businessForm.content.description.rows,
                required: businessForm.content.description.required,
            }}
            pricing={{
                caption: businessForm.content.pricing.caption,
                programing: businessForm.content.pricing.programing,
                programmingPrice: businessForm.content.pricing.programmingPrice,
                hosting: businessForm.content.pricing.hosting,
                hostingPrice: businessForm.content.pricing.hostingPrice,
                support: businessForm.content.pricing.support,
                supportPrice: businessForm.content.pricing.supportPrice,
                info: businessForm.content.pricing.info,
            }}
            pt={props.pt}
            pb={props.pb}
            background={props.background}
            hasIcon={props.hasIcon}
            hasCaption={props.hasCaption}
            hasShadow={props.hasShadow}
        />
    );
};
