import Validate from "validate.js";
import { IValidatePasswordForm } from "./interface";

export const ValidatePasswordForm = (props: IValidatePasswordForm): any =>
{
    let constraints =  
    {
        oldPassword:
        {
            presence: true,
            length: 
            {
                minimum: 8,
                message: "must be at least 8 characters"
            }
        },
        newPassword:
        {
            presence: true,
            length: 
            {
                minimum: 8,
                message: "must be at least 8 characters"
            }
        },
        confirmPassword:
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
        oldPassword: props.oldPassword,
        newPassword: props.newPassword,
        confirmPassword: props.confirmPassword
    }, 
    constraints);

    if (props.newPassword !== props.confirmPassword)
    {
        const errorText = "Password do not match";
        if (result === undefined) return { message: errorText };

        let isNewPasswordUndefined = result["newPassword"] === undefined;
        let isConfirmPasswordUndefined = result["confirmPassword"] === undefined; 

        if (!isNewPasswordUndefined && !isConfirmPasswordUndefined) return {
            newPassword: [...result.newPassword],
            confirmPassword: [...result.confirmPassword, errorText]
        };

        if (isNewPasswordUndefined && !isConfirmPasswordUndefined) return {
            confirmPassword: [...result.confirmPassword, errorText]
        };

        if (!isNewPasswordUndefined && isConfirmPasswordUndefined) return {
            newPassword: [...result.newPassword]
        };
    }

    return result;
}
