import Validate from "validate.js";
import { SignupFormInput } from "./interface";

const PASSWORD_MISSING_CHAR = "The user password must contain at least one of the following characters: !, @, #, $, %, ^, &, *";
const PASSWORD_MISSING_NUMBER = "The user password must contain at least one number";
const PASSWORD_MISSING_LARGE_LETTER = "The user password must contain at least one large letter";
const PASSWORD_MISSING_SMALL_LETTER = "The user password must contain at least one small letter";

export const ValidateSignupForm = (props: SignupFormInput): any =>
{
    let constraints =  
    {
        firstName:
        {
            presence: true,
            length: 
            {
                minimum: 1,
                maximum: 255,
                message: "must be between 1..255 characters"
            }
        },
        lastName:
        {
            presence: true,
            length: 
            {
                minimum: 1,
                maximum: 255,
                message: "must be between 1..255 characters"
            }
        },
        email: 
        {
            email: 
            {
                message: "does not look like a valid email"
            }
        },
        password:
        {
            presence: true,
            length: 
            {
                minimum: 8,
                maximum: 50,
                message: "must be between 8..50 characters"
            }
        },
        terms:
        {
            presence: true,
            inclusion:
            {
                within: [true],
                message: "^You must accept terms of use and privacy policy"
            }
        }
    }

    let result = Validate(
    {
        firstName: props.firstName,
        lastName: props.lastName,
        email: props.email,
        password: props.password,
        terms: props.terms
    }, 
    constraints);

    if (!HaveSpecialCharacter(props.password))
    {
        const hasWarning = result !== undefined || result["password"] !== undefined;
        const data = hasWarning ? [...result.password, PASSWORD_MISSING_CHAR] : [PASSWORD_MISSING_CHAR];
        result = { ...result, password: data }
    }

    if (!ContainNumber(props.password))
    {
        const hasWarning = result !== undefined || result["password"] !== undefined;
        const data = hasWarning ? [...result.password, PASSWORD_MISSING_NUMBER] : [PASSWORD_MISSING_NUMBER];
        result = { ...result, password: data }
    }

    if (!HaveLargeLetter(props.password))
    {
        const hasWarning = result !== undefined || result["password"] !== undefined;
        const data = hasWarning ? [...result.password, PASSWORD_MISSING_LARGE_LETTER] : [PASSWORD_MISSING_LARGE_LETTER];
        result = { ...result, password: data }
    }

    if (!HaveSmallLetter(props.password))
    {
        const hasWarning = result !== undefined || result["password"] !== undefined;
        const data = hasWarning ? [...result.password, PASSWORD_MISSING_SMALL_LETTER] : [PASSWORD_MISSING_SMALL_LETTER];
        result = { ...result, password: data }
    }

    return result;
}

const HaveSpecialCharacter = (value: string): boolean => 
{
    const characters = [ "!", "@", "#", "$", "%", "^", "&", "*" ];
    for (let index: number = 0; index < value.length; index++) 
    {
        if (characters.includes(value[index]))
        {
            return true;
        }
    }

    return false;
}

const ContainNumber = (value: string): boolean => 
{
    for (let index: number = 0; index < value.length; index++) 
    {
        let charCode = value.charCodeAt(index);
        if (charCode >= 48 && charCode <= 57)
        {
            return true;
        }
    }

    return false;
}

const HaveLargeLetter = (value: string): boolean => 
{
    for (let index: number = 0; index < value.length; index++) 
    {
        let charCode = value.charCodeAt(index);
        if (charCode >= 65 && charCode <= 90)
        {
            return true;
        }
    }

    return false;
}

const HaveSmallLetter = (value: string): boolean => 
{
    for (let index: number = 0; index < value.length; index++) 
    {
        let charCode = value.charCodeAt(index);
        if (charCode >= 97 && charCode <= 122)
        {
            return true;
        }
    }

    return false;
}
