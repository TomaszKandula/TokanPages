import Validate from "validate.js";
import { PasswordFormInput } from "..";
import { ContainNumber, HasProperty, HaveLargeLetter, HaveSmallLetter, HaveSpecialCharacter } from "../Helpers";

export const ValidatePasswordForm = (props: PasswordFormInput): any => {
    let constraints = {
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

    if (!HaveSpecialCharacter(props.newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, "New password " + props.content.missingChar]
            : [props.content.missingChar];
        result = { ...result, newPassword: data };
    }

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
