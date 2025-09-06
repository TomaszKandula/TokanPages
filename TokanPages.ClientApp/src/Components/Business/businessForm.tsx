import * as React from "react";
import { BusinessFormView } from "./View/businessFormView";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ApplicationDialogAction, ApplicationMessageAction } from "../../Store/Actions";
import { IconType, OperationStatus } from "../../Shared/enums";
import { INTERNAL_MESSAGE_TEXT, INTERNAL_SUBJECT_TEXT, RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { formatPhoneNumber } from "../../Shared/Services/Converters";
import { ValidateBusinessForm } from "../../Shared/Services/FormValidation";
import { ReactChangeEvent, ReactChangeTextEvent, ReactKeyboardEvent } from "../../Shared/types";
import { getSelection, resetSelection, valueCleanUp } from "./Utilities";
import { BusinessFormProps, MessageFormProps, OfferItemProps } from "./Types";
import Validate from "validate.js";

const formDefault: MessageFormProps = {
    company: "",
    firstName: "",
    lastName: "",
    email: "",
    phone: "",
    description: "",
};

export const BusinessForm = (props: BusinessFormProps): React.ReactElement => {
    const dispatch = useDispatch();

    const email = useSelector((state: ApplicationState) => state.applicationEmail);
    const error = useSelector((state: ApplicationState) => state.applicationError);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage?.id);
    const templates = data.components.templates;
    const businessForm = data.components.pageBusinessForm;

    const hasNotStarted = email?.status === OperationStatus.notStarted;
    const hasFinished = email?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState<MessageFormProps>(formDefault);
    const [description, setDescription] = React.useState({ description: "" });
    const [technologyItems, setTechnologyItems] = React.useState<OfferItemProps[] | undefined>(undefined);
    const [serviceItems, setServiceItems] = React.useState<OfferItemProps[] | undefined>(undefined);
    const [hasProgress, setHasProgress] = React.useState(false);

    const clearAll = React.useCallback(() => {
        if (serviceItems && serviceItems.length > 0) {
            const items = resetSelection(serviceItems);
            setServiceItems(items);
        }

        if (technologyItems && technologyItems.length > 0) {
            const items = resetSelection(technologyItems);
            setTechnologyItems(items);
        }

        setForm(formDefault);
        setDescription({ description: "" });
    }, [formDefault, technologyItems, serviceItems]);

    const clearForm = React.useCallback(() => {
        if (!hasProgress) {
            return;
        }

        clearAll();
        setHasProgress(false);
        dispatch(ApplicationMessageAction.clear());
    }, [hasProgress]);

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

    const descriptionHandler = React.useCallback(
        (event: ReactChangeTextEvent) => {
            setDescription({ ...description, [event.currentTarget.name]: event.currentTarget.value });
        },
        [description]
    );

    const techHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            if (!technologyItems) {
                return;
            }

            const index = Number(event.target.id);
            const data = technologyItems.slice();
            data[index].isChecked = event.target.checked;
            setTechnologyItems(data);
        },
        [technologyItems]
    );

    const serviceHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            if (!serviceItems) {
                return;
            }

            const index = Number(event.target.id);
            const data = serviceItems.slice();
            data[index].isChecked = event.target.checked;
            setServiceItems(data);
        },
        [serviceItems]
    );

    const buttonHandler = React.useCallback(() => {
        const techStack = getSelection(technologyItems);
        const services = getSelection(serviceItems);
        const result = ValidateBusinessForm(
            {
                company: form.company,
                firstName: form.firstName,
                lastName: form.lastName,
                email: form.email,
                phone: form.phone,
                description: description.description,
                techStack: techStack,
                services: services,
            },
            businessForm.technology.canDisplay
        );

        if (!Validate.isDefined(result)) {
            setHasProgress(true);
            return;
        }

        dispatch(
            ApplicationDialogAction.raise({
                title: templates.forms.textBusinessForm,
                message: templates.templates.messageOut.warning,
                validation: result,
                icon: IconType.warning,
            })
        );
    }, [form, description, templates, serviceItems, technologyItems]);

    React.useEffect(() => {
        if (!technologyItems && businessForm.technology.items.length > 0) {
            const items = businessForm.technology.items.slice() as OfferItemProps[];
            const data = resetSelection(items);
            setTechnologyItems(data);
        }

        if (!serviceItems && businessForm.pricing.items.length > 0) {
            const items = businessForm.pricing.items.slice() as OfferItemProps[];
            const data = resetSelection(items);
            setServiceItems(data);
        }
    }, [businessForm.technology.items, businessForm.pricing.items]);

    React.useEffect(() => {
        if (hasError) {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) {
            const techStack = getSelection(technologyItems);
            const services = getSelection(serviceItems);
            const data = JSON.stringify({
                ...form,
                techStack: techStack,
                services: services,
            });

            dispatch(
                ApplicationMessageAction.send({
                    languageId: languageId,
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
            dispatch(
                ApplicationDialogAction.raise({
                    title: templates.forms.textBusinessForm,
                    message: templates.templates.messageOut.success,
                    icon: IconType.info,
                })
            );
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, templates, technologyItems, serviceItems, languageId]);

    React.useEffect(() => {
        return () => { 
            clearAll();
        };
    }, []);

    return (
        <BusinessFormView
            isLoading={data.isLoading}
            caption={businessForm.caption}
            progress={hasProgress}

            buttonText={businessForm.buttonText}

            keyHandler={keyHandler}
            formHandler={formHandler}
            buttonHandler={buttonHandler}

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
            description={{
                text: description.description,
                label: businessForm.description.label,
                rows: businessForm.description.rows,
                required: businessForm.description.required,
                handler: descriptionHandler,
            }}
            technology={{
                caption: businessForm.technology.caption,
                canDisplay: businessForm.technology.canDisplay,
                items: technologyItems ?? [],
                handler: techHandler,
            }}
            pricing={{
                caption: businessForm.pricing.caption,
                disclaimer: businessForm.pricing.disclaimer,
                items: serviceItems ?? [],
                handler: serviceHandler,
            }}
            presentation={businessForm.presentation}
            className={props.className}
            hasIcon={props.hasIcon}
            hasCaption={props.hasCaption}
            hasShadow={props.hasShadow}
        />
    );
};
