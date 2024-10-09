import * as React from "react";
import { BusinessFormView } from "./View/businessFormView";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ApplicationDialogAction, ApplicationMessageAction } from "../../Store/Actions";
import { OperationStatus } from "../../Shared/enums";
import { INTERNAL_MESSAGE_TEXT, INTERNAL_SUBJECT_TEXT, RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { formatPhoneNumber } from "../../Shared/Services/Converters";
import { GetTextWarning, SuccessMessage, WarningMessage } from "../../Shared/Services/Utilities";
import { ValidateBusinessForm } from "../../Shared/Services/FormValidation";
import { ReactChangeEvent, ReactKeyboardEvent, ReactMouseEvent } from "../../Shared/types";
import { BusinessFormProps, MessageFormProps } from "./Models";
import Validate from "validate.js";
import { TechItemsDto } from "Api/Models";

const formDefault: MessageFormProps = {
    company: "",
    firstName: "",
    lastName: "",
    email: "",
    phone: "",
    description: "",
    techStack: [""],
    services: [""],
};

const valueCleanUp = (input: string): string => {
    return input.replaceAll(" ", "").replaceAll("(", "").replaceAll(")", "");
};

const getTechSelection = (input?: TechItemsDto[]): string[] => {
    if (!input) {
        return [""];
    }

    let result: string[] = [];
    input.forEach(item => {
        if (item.isChecked) {
            result.push(item.value);
        }
    });

    return result;
};

const resetTechStack = (input?: TechItemsDto[]): TechItemsDto[] => {
    if (!input) {
        return [];
    }

    let result: TechItemsDto[] = [];
    input.forEach(item => {
        result.push({ ...item, isChecked: false });
    });

    return result;
};

export const BusinessForm = (props: BusinessFormProps): React.ReactElement => {
    const dispatch = useDispatch();

    const email = useSelector((state: ApplicationState) => state.applicationEmail);
    const error = useSelector((state: ApplicationState) => state.applicationError);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const templates = data.components.templates;
    const businessForm = data.components.businessForm;

    const hasNotStarted = email?.status === OperationStatus.notStarted;
    const hasFinished = email?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState<MessageFormProps>(formDefault);
    const [techStackItems, setTechStackItems] = React.useState<TechItemsDto[] | undefined>(undefined);
    const [services, setServices] = React.useState<string[]>([]);
    const [hasProgress, setHasProgress] = React.useState(false);

    const showSuccess = (text: string) =>
        dispatch(ApplicationDialogAction.raise(SuccessMessage(templates.forms.textBusinessForm, text)));
    const showWarning = (text: string) =>
        dispatch(ApplicationDialogAction.raise(WarningMessage(templates.forms.textBusinessForm, text)));

    const clearForm = React.useCallback(() => {
        if (!hasProgress) {
            return;
        }

        setTechStackItems(resetTechStack(techStackItems));
        setServices([""]);
        setForm(formDefault);
        setHasProgress(false);
        dispatch(ApplicationMessageAction.clear());
    }, [hasProgress, techStackItems]);

    React.useEffect(() => {
        if (!techStackItems && businessForm.techItems.length > 0) {
            setTechStackItems(businessForm.techItems);
        }
    }, [businessForm.techItems]);

    React.useEffect(() => {
        if (hasError) {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) {
            const techStack = getTechSelection(techStackItems);
            const data = JSON.stringify({
                ...form,
                techStack: techStack,
                services: services,
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
            showSuccess(templates.templates.messageOut.success);
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, templates, techStackItems, services]);

    const keyHandler = React.useCallback(
        (event: ReactKeyboardEvent) => {
            if (event.code === "Enter") {
                buttonHandler();
            }
        },
        [form.company, form.description, form.email, form.firstName, form.lastName, form.phone]
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

    const techHandler = React.useCallback(
        (event: ReactChangeEvent, isChecked: boolean) => {
            if (!techStackItems) {
                return;
            }

            const index = Number(event.currentTarget.id);
            const data = techStackItems.slice();
            data[index].isChecked = isChecked;
            setTechStackItems(data);
        },
        [techStackItems]
    );

    const serviceHandler = React.useCallback(
        (event: ReactMouseEvent) => {
            event.preventDefault();
            const data = event.currentTarget.getAttribute("data-disabled") as string;
            const isDisabled = JSON.parse(data as string);
            if (isDisabled) {
                return;
            }

            const id = event.currentTarget.id;
            if (!services) {
                const data = [];
                data.push(id);
                setServices(data);
            } else {
                const data = services.slice();
                const index = data.indexOf(id);
                if (!data.includes(id)) {
                    data.push(id);
                    setServices(data);
                } else {
                    data.splice(index, 1);
                    setServices(data);
                }
            }
        },
        [services]
    );

    const buttonHandler = React.useCallback(() => {
        const techStack = getTechSelection(techStackItems);
        const result = ValidateBusinessForm({
            company: form.company,
            firstName: form.firstName,
            lastName: form.lastName,
            email: form.email,
            phone: form.phone,
            description: form.description,
            techStack: techStack,
            services: services,
        });

        if (!Validate.isDefined(result)) {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: result, template: templates.templates.messageOut.warning }));
    }, [form, templates, services, techStackItems]);

    return (
        <BusinessFormView
            isLoading={data.isLoading}
            caption={businessForm.caption}
            progress={hasProgress}
            buttonText={businessForm.buttonText}
            keyHandler={keyHandler}
            formHandler={formHandler}
            buttonHandler={buttonHandler}
            techHandler={techHandler}
            serviceHandler={serviceHandler}
            serviceSelection={services}
            companyText={form.company}
            companyLabel={businessForm.companyLabel}
            firstNameText={form.firstName}
            firstNameLabel={businessForm.firstNameLabel}
            lastNameText={form.lastName}
            lastNameLabel={businessForm.lastNameLabel}
            emailText={form.email}
            emailLabel={businessForm.emailLabel}
            phoneText={form.phone}
            phoneLabel={businessForm.phoneLabel}
            techLabel={businessForm.techLabel}
            techItems={techStackItems ?? []}
            description={{
                text: form.description,
                label: businessForm.description.label,
                multiline: businessForm.description.multiline,
                rows: businessForm.description.rows,
                required: businessForm.description.required,
            }}
            pricing={{
                caption: businessForm.pricing.caption,
                disclaimer: businessForm.pricing.disclaimer,
                services: businessForm.pricing.services,
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
