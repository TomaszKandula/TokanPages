import * as React from "react";
import { BusinessFormView } from "./View/businessFormView";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ApplicationDialogAction, ApplicationMessageAction } from "../../Store/Actions";
import { OperationStatus } from "../../Shared/enums";
import { INTERNAL_MESSAGE_TEXT, INTERNAL_SUBJECT_TEXT, RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { formatPhoneNumber } from "../../Shared/Services/Converters";
import { SuccessMessage } from "../../Shared/Services/Utilities";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../Shared/types";
import { BusinessFormProps, MessageFormProps, TechStackItem } from "./Models";

const formDefault: MessageFormProps = {
    company: "",
    firstName: "",
    lastName: "",
    email: "",
    phone: "",
    description: "",
}

const valueCleanUp = (input: string): string => {
    return input.replaceAll(" ", "").replaceAll("(", "").replaceAll(")", "");
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

    const [form, setForm] = React.useState<MessageFormProps>(formDefault);
    const [techStack, setTechStack] = React.useState<string[] | undefined>(undefined);
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
            const data = JSON.stringify({ 
                ...form,
                techStack: techStack,
            });

            dispatch(
                ApplicationMessageAction.send({
                    firstName: form.firstName,
                    lastName: form.lastName,
                    userEmail: form.email,
                    emailFrom: form.email,
                    emailTos: [form.email],
                    subject: INTERNAL_SUBJECT_TEXT,
                    message: INTERNAL_MESSAGE_TEXT,
                    businessData: data,
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
            form.company, 
            form.description, 
            form.email, 
            form.firstName, 
            form.lastName, 
            form.phone,
        ]
    );

    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            if (event.currentTarget.name === "phone") {
                const value = valueCleanUp(event.currentTarget.value);
                const hasDigitOnly = /^\d+$/.test(value);
                if (value !== "" && !hasDigitOnly) {
                    return;
                }

                const phone = formatPhoneNumber(value);
                if (phone !== null) {
                    setForm({ ...form, phone: phone });
                    return;
                }
            }

            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form, form.phone]
    );

    const techHandler = React.useCallback((item: TechStackItem, isChecked: boolean) => {
        if (isChecked) {
            if (!techStack) {
                const data = [];
                data.push(item.value);
                setTechStack(data);
            } else {
                const data = techStack.slice();
                if (!data.includes(item.value)) {
                    data.push(item.value);
                    setTechStack(data);
                }
            }
        } else {
            if (techStack) {
                const data = techStack.slice();
                const index = data.indexOf(item.value);
                if (index !== -1) {
                    data.splice(index, 1);
                    setTechStack(data);
                }
            }
        }
    }, [techStack]);

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
            companyText={form.company}
            companyLabel={businessForm.content.companyLabel}
            firstNameText={form.firstName}
            firstNameLabel={businessForm.content.firstNameLabel}
            lastNameText={form.lastName}
            lastNameLabel={businessForm.content.lastNameLabel}
            emailText={form.email}
            emailLabel={businessForm.content.emailLabel}
            phoneText={form.phone}
            phoneLabel={businessForm.content.phoneLabel}
            techLabel={businessForm.content.techLabel}
            techItems={businessForm.content.techItems}
            description={{
                text: form.description,
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
