import Validate from "validate.js";
import { UpdateFormInput } from "..";
import { ContainNumber, HasProperty, HaveLargeLetter, HaveSmallLetter, HaveSpecialCharacter } from "../Helpers";

import {
    PASSWORD_MISSING_CHAR,
    PASSWORD_MISSING_LARGE_LETTER,
    PASSWORD_MISSING_NUMBER,
    PASSWORD_MISSING_SMALL_LETTER,
} from "../../../../Shared/constants";

export const ValidateUpdateForm = (props: UpdateFormInput): any => {
    let constraints = {
        newPassword: {
            presence: true,
            length: {
                minimum: 8,
                maximum: 50,
                message: "must be between 8..50 characters",
            },
        },
        verifyPassword: {
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
            newPassword: props.newPassword,
            verifyPassword: props.verifyPassword,
        },
        constraints
    );

    haveSpecialCharacter(props.newPassword, result);
    containNumber(props.newPassword, result);
    haveLargeLetter(props.newPassword, result);
    haveSmallLetter(props.newPassword, result);

    if (props.newPassword !== props.verifyPassword) {
        const errorText = "Password do not match";
        if (result === undefined) {
            return { message: errorText };
        }

        let isNewPasswordUndefined = result["newPassword"] === undefined;
        let isVerifyPasswordUndefined = result["verifyPassword"] === undefined;

        if (!isNewPasswordUndefined && !isVerifyPasswordUndefined) {
            return { newPassword: [...result.newPassword], verifyPassword: [...result.verifyPassword, errorText] };
        }

        if (isNewPasswordUndefined && !isVerifyPasswordUndefined) {
            return { verifyPassword: [...result.verifyPassword, errorText] };
        }

        if (!isNewPasswordUndefined && isVerifyPasswordUndefined) {
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
