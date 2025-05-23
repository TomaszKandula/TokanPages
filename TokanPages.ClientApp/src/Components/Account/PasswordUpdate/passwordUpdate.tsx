import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { ApplicationState } from "../../../Store/Configuration";
import { IconType, OperationStatus } from "../../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { ApplicationDialogAction, UserPasswordUpdateAction } from "../../../Store/Actions";
import { UpdateFormInput, ValidateUpdateForm } from "../../../Shared/Services/FormValidation";
import { RECEIVED_ERROR_MESSAGE } from "../../../Shared/constants";
import { PasswordUpdateView } from "./View/passwordUpdateView";
import Validate from "validate.js";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

const formDefaultValues: UpdateFormInput = {
    newPassword: "",
    verifyPassword: "",
    content: {
        emailInvalid: "",
        nameInvalid: "",
        surnameInvalid: "",
        passwordInvalid: "",
        missingTerms: "",
        missingChar: "",
        missingLargeLetter: "",
        missingNumber: "",
        missingSmallLetter: "",
    },
};

export interface PasswordUpdateProps {
    className?: string;
    background?: string;
}

export const PasswordUpdate = (props: PasswordUpdateProps): React.ReactElement => {
    const queryParam = useQuery();
    const dispatch = useDispatch();

    const store = useSelector((state: ApplicationState) => state.userDataStore);
    const update = useSelector((state: ApplicationState) => state.userPasswordUpdate);
    const error = useSelector((state: ApplicationState) => state.applicationError);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const template = data?.components.templates;
    const password = data?.components.pagePasswordUpdate;

    const hasNotStarted = update?.status === OperationStatus.notStarted;
    const hasFinished = update?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const resetId = queryParam.get("id");
    const userId = store?.userData?.userId;
    const canDisableForm = Validate.isEmpty(resetId) && Validate.isEmpty(userId);

    const [form, setForm] = React.useState(formDefaultValues);
    const [hasProgress, setHasProgress] = React.useState(false);

    const clearForm = React.useCallback(() => {
        if (!hasProgress) return;
        setHasProgress(false);
        dispatch(UserPasswordUpdateAction.clear());
    }, [hasProgress]);

    React.useEffect(() => {
        if (hasError) {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) {
            dispatch(
                UserPasswordUpdateAction.update({
                    id: userId,
                    resetId: resetId as string,
                    newPassword: form.newPassword,
                })
            );

            return;
        }

        if (hasFinished) {
            clearForm();
            setForm(formDefaultValues);
            dispatch(
                ApplicationDialogAction.raise({
                    title: template.forms.textAccountSettings,
                    message: template.templates.password.updateSuccess,
                    icon: IconType.info,
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
        [form.newPassword, form.verifyPassword]
    );

    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const buttonHandler = React.useCallback(() => {
        let result = ValidateUpdateForm({
            newPassword: form.newPassword,
            verifyPassword: form.verifyPassword,
            content: {
                emailInvalid: template.templates.password.emailInvalid,
                nameInvalid: template.templates.password.nameInvalid,
                surnameInvalid: template.templates.password.surnameInvalid,
                passwordInvalid: template.templates.password.passwordInvalid,
                missingTerms: template.templates.password.missingTerms,
                missingLargeLetter: template.templates.password.missingLargeLetter,
                missingNumber: template.templates.password.missingNumber,
                missingSmallLetter: template.templates.password.missingSmallLetter,
            },
        });

        if (!Validate.isDefined(result)) {
            setHasProgress(true);
            return;
        }

        dispatch(
            ApplicationDialogAction.raise({
                title: template.forms.textAccountSettings,
                message: template.templates.password.updateWarning,
                validation: result,
                icon: IconType.warning,
            })
        );
    }, [form, template]);

    return (
        <PasswordUpdateView
            isLoading={data?.isLoading}
            progress={hasProgress}
            caption={password?.caption}
            button={password?.button}
            newPassword={form.newPassword}
            verifyPassword={form.verifyPassword}
            keyHandler={keyHandler}
            formHandler={formHandler}
            buttonHandler={buttonHandler}
            disableForm={canDisableForm}
            labelNewPassword={password?.labelNewPassword}
            labelVerifyPassword={password?.labelVerifyPassword}
            className={props.className}
            background={props.background}
        />
    );
};
