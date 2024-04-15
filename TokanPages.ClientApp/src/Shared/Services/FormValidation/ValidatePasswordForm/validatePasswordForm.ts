import Validate from "validate.js";
import { PasswordFormInput } from "..";
import { ContainNumber, HasProperty, HaveLargeLetter, HaveSmallLetter, HaveSpecialCharacter } from "../Helpers";

import {
    PASSWORD_MISSING_CHAR,
    PASSWORD_MISSING_LARGE_LETTER,
    PASSWORD_MISSING_NUMBER,
    PASSWORD_MISSING_SMALL_LETTER,
} from "../../../../Shared/constants";

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
        newPassword: {
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
            newPassword: props.newPassword,
            confirmPassword: props.confirmPassword,
        },
        constraints
    );

    haveSpecialCharacter(props.newPassword, result);
    containNumber(props.newPassword, result);
    haveLargeLetter(props.newPassword, result);
    haveSmallLetter(props.newPassword, result);

    if (props.newPassword !== props.confirmPassword) {
        const errorText = "Password do not match";
        if (result === undefined) {
            return { message: errorText };
        }

        const isNewPasswordUndefined = result["newPassword"] === undefined;
        const isConfirmPasswordUndefined = result["confirmPassword"] === undefined;

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

const haveSpecialCharacter = (newPassword: string, result: any): void => {
    if (!HaveSpecialCharacter(newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, "New password " + PASSWORD_MISSING_CHAR]
            : [PASSWORD_MISSING_CHAR];
        result = { ...result, newPassword: data };
    }
}

const containNumber = (newPassword: string, result: any): void => {
    if (!ContainNumber(newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, "New password " + PASSWORD_MISSING_NUMBER]
            : [PASSWORD_MISSING_NUMBER];
        result = { ...result, newPassword: data };
    }
}

const haveLargeLetter = (newPassword: string, result: any): void => {
    if (!HaveLargeLetter(newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, "New password " + PASSWORD_MISSING_LARGE_LETTER]
            : [PASSWORD_MISSING_LARGE_LETTER];
        result = { ...result, newPassword: data };
    }
}

const haveSmallLetter = (newPassword: string, result: any): void => {
    if (!HaveSmallLetter(newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, "New password " + PASSWORD_MISSING_SMALL_LETTER]
            : [PASSWORD_MISSING_SMALL_LETTER];
        result = { ...result, newPassword: data };
    }
}
