import Validate from "validate.js";
import { UpdateFormInput } from "..";
import { containNumber, haveLargeLetter, haveSmallLetter, haveSpecialCharacter } from "../Helpers";

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
