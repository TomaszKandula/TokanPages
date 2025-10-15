import Validate from "validate.js";
import { ContainNumber, HasProperty, HaveLargeLetter, HaveSmallLetter } from "../Utilities";
import { ValidatePasswordFormProps } from "../Types";

export const ValidatePasswordForm = (props: ValidatePasswordFormProps): object | undefined => {
    const constraints = {
        oldPassword: {
            presence: true,
            length: {
                minimum: 8,
                maximum: 50,
                message: props.content.passwordInvalid,
            },
        },
        newPassword: {
            presence: true,
            length: {
                minimum: 8,
                maximum: 50,
                message: props.content.passwordInvalid,
            },
        },
        confirmPassword: {
            presence: true,
            length: {
                minimum: 8,
                maximum: 50,
                message: props.content.passwordInvalid,
            },
        },
    };

    let result = Validate(
        {
            oldPassword: props.oldPassword,
            newPassword: props.newPassword,
            confirmPassword: props.confirmPassword,
        },
        constraints
    );

    if (!ContainNumber(props.newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, "New password " + props.content.missingNumber]
            : [props.content.missingNumber];
        result = { ...result, newPassword: data };
    }

    if (!HaveLargeLetter(props.newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, "New password " + props.content.missingLargeLetter]
            : [props.content.missingLargeLetter];
        result = { ...result, newPassword: data };
    }

    if (!HaveSmallLetter(props.newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, "New password " + props.content.missingSmallLetter]
            : [props.content.missingSmallLetter];
        result = { ...result, newPassword: data };
    }

    if (props.newPassword !== props.confirmPassword) {
        const errorText = "Password do not match";
        if (result === undefined) {
            return { message: errorText };
        }

        let isNewPasswordUndefined = result["newPassword"] === undefined;
        let isConfirmPasswordUndefined = result["confirmPassword"] === undefined;

        if (!isNewPasswordUndefined && !isConfirmPasswordUndefined) {
            return { newPassword: [...result.newPassword], confirmPassword: [...result.confirmPassword, errorText] };
        }

        if (isNewPasswordUndefined && !isConfirmPasswordUndefined) {
            return { confirmPassword: [...result.confirmPassword, errorText] };
        }

        if (!isNewPasswordUndefined && isConfirmPasswordUndefined) {
            return { newPassword: [...result.newPassword] };
        }
    }

    return result;
};
