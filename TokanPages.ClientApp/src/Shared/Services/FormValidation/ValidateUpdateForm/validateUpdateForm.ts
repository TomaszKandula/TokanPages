import Validate from "validate.js";
import { UpdateFormInput } from "..";
import { ContainNumber, HasProperty, HaveLargeLetter, HaveSmallLetter, HaveSpecialCharacter } from "../Helpers";

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

    if (!HaveSpecialCharacter(props.newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, props.content.missingChar]
            : [props.content.missingChar];
        result = { ...result, password: data };
    }

    if (!ContainNumber(props.newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, props.content.missingNumber]
            : [props.content.missingNumber];
        result = { ...result, password: data };
    }

    if (!HaveLargeLetter(props.newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, props.content.missingLargeLetter]
            : [props.content.missingLargeLetter];
        result = { ...result, password: data };
    }

    if (!HaveSmallLetter(props.newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, props.content.missingSmallLetter]
            : [props.content.missingSmallLetter];
        result = { ...result, password: data };
    }

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
