import Validate from "validate.js";
import { IValidateUpdateForm } from "./interface";

export const ValidateUpdateForm = (props: IValidateUpdateForm): any =>
{
    let constraints =  
    {
        newPassword:
        {
            presence: true,
            length: 
            {
                minimum: 8,
                message: "must be at least 8 characters"
            }
        },
        verifyPassword:
        {
            presence: true,
            length: 
            {
                minimum: 8,
                message: "must be at least 8 characters"
            }
        },
    }

    let result = Validate(
    {
        newPassword: props.newPassword,
        verifyPassword: props.verifyPassword
    }, 
    constraints);

    if (props.newPassword !== props.verifyPassword)
    {
        const errorText = "Password do not match";
        if (result === undefined) return { message: errorText };

        let isNewPasswordUndefined = result["newPassword"] === undefined;
        let isVerifyPasswordUndefined = result["verifyPassword"] === undefined; 

        if (!isNewPasswordUndefined && !isVerifyPasswordUndefined) return {
            newPassword: [...result.newPassword],
            verifyPassword: [...result.verifyPassword, errorText]
        };

        if (isNewPasswordUndefined && !isVerifyPasswordUndefined) return {
            verifyPassword: [...result.verifyPassword, errorText]
        };

        if (!isNewPasswordUndefined && isVerifyPasswordUndefined) return {
            newPassword: [...result.newPassword],
        };
    }

    return result;
}
