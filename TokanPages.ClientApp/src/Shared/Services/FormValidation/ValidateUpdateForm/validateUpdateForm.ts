import Validate from "validate.js";
import { UpdateFormInput } from "..";
import { 
    ContainNumber, 
    HasProperty, 
    HaveLargeLetter, 
    HaveSmallLetter, 
    HaveSpecialCharacter 
} from "../Helpers";

import { 
    PASSWORD_MISSING_CHAR, 
    PASSWORD_MISSING_LARGE_LETTER, 
    PASSWORD_MISSING_NUMBER, 
    PASSWORD_MISSING_SMALL_LETTER 
} from "../../../../Shared/constants";

export const ValidateUpdateForm = (props: UpdateFormInput): any =>
{
    let constraints =  
    {
        newPassword:
        {
            presence: true,
            length: 
            {
                minimum: 8,
                maximum: 50,
                message: "must be between 8..50 characters"
            }
        },
        verifyPassword:
        {
            presence: true,
            length: 
            {
                minimum: 8,
                maximum: 50,
                message: "must be between 8..50 characters"
            }
        },
    }

    let result = Validate(
    {
        newPassword: props.newPassword,
        verifyPassword: props.verifyPassword
    }, 
    constraints);

    if (!HaveSpecialCharacter(props.newPassword))
    {
        const data = HasProperty(result, "newPassword") ? [...result.newPassword, PASSWORD_MISSING_CHAR] : [PASSWORD_MISSING_CHAR];
        result = { ...result, password: data }
    }

    if (!ContainNumber(props.newPassword))
    {
        const data = HasProperty(result, "newPassword") ? [...result.newPassword, PASSWORD_MISSING_NUMBER] : [PASSWORD_MISSING_NUMBER];
        result = { ...result, password: data }
    }

    if (!HaveLargeLetter(props.newPassword))
    {
        const data = HasProperty(result, "newPassword") ? [...result.newPassword, PASSWORD_MISSING_LARGE_LETTER] : [PASSWORD_MISSING_LARGE_LETTER];
        result = { ...result, password: data }
    }

    if (!HaveSmallLetter(props.newPassword))
    {
        const data = HasProperty(result, "newPassword") ? [...result.newPassword, PASSWORD_MISSING_SMALL_LETTER] : [PASSWORD_MISSING_SMALL_LETTER];
        result = { ...result, password: data }
    }
    
    if (props.newPassword !== props.verifyPassword)
    {
        const errorText = "Password do not match";
        if (result === undefined) 
        {
            return { message: errorText };
        }

        let isNewPasswordUndefined = result["newPassword"] === undefined;
        let isVerifyPasswordUndefined = result["verifyPassword"] === undefined; 

        if (!isNewPasswordUndefined && !isVerifyPasswordUndefined) 
        {
            return { newPassword: [...result.newPassword], verifyPassword: [...result.verifyPassword, errorText] };
        }

        if (isNewPasswordUndefined && !isVerifyPasswordUndefined) 
        {
            return { verifyPassword: [...result.verifyPassword, errorText] };
        }

        if (!isNewPasswordUndefined && isVerifyPasswordUndefined) 
        {
            return { newPassword: [...result.newPassword] };
        }
    }

    return result;
}
