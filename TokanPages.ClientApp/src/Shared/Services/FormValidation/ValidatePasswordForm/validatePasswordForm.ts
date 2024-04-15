import Validate from "validate.js";
import { PasswordFormInput } from "..";
import { containNumber, haveLargeLetter, haveSmallLetter, haveSpecialCharacter } from "../Helpers";

export const ValidatePasswordForm = (props: PasswordFormInput): any => {
    const constraints = {
        oldPassword: {
            presence: true,
            length: {
                minimum: 8,
                maximum: 50,
                message: "must be between 8..50 characters",
            },
        },
        password: {
            presence: true,
            length: {
                minimum: 8,
                maximum: 50,
                message: "must be between 8..50 characters",
            },
        },
        confirmPassword: {
            presence: true,
            length: {
                minimum: 8,
                maximum: 50,
                message: "must be between 8..50 characters",
            },
        },
    };

    let result = Validate(
        {
            oldPassword: props.oldPassword,
            password: props.newPassword,
            confirmPassword: props.confirmPassword,
        },
        constraints
    );

    containNumber(props.newPassword, result);
    haveSpecialCharacter(props.newPassword, result);
    haveLargeLetter(props.newPassword, result);
    haveSmallLetter(props.newPassword, result);

    if (props.newPassword !== props.confirmPassword) {
        const errorText = "Password do not match";
        if (result === undefined) {
            return { message: errorText };
        }

        const isNewPasswordUndefined = result["password"] === undefined;
        const isConfirmPasswordUndefined = result["confirmPassword"] === undefined;

        if (!isNewPasswordUndefined && !isConfirmPasswordUndefined) {
            return { password: [...result.password], confirmPassword: [...result.confirmPassword, errorText] };
        }

        if (isNewPasswordUndefined && !isConfirmPasswordUndefined) {
            return { confirmPassword: [...result.confirmPassword, errorText] };
        }

        if (!isNewPasswordUndefined && isConfirmPasswordUndefined) {
            return { password: [...result.password] };
        }
    }

    return result;
};
