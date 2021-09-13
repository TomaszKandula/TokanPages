import Validate from "validate.js";

export const ValidateEmail = (email: string): any =>
{
    let constraints = 
    {
        email: 
        {
            email: 
            {
                message: "does not look like a valid email"
            }
        }
    };

    return Validate({ email: email }, constraints);
}

export interface IValidateContactForm
{
    firstName: string;
    lastName: string;
    email: string;
    subject: string;
    message: string;
    terms: boolean;
}

export const ValidateContactForm = (props: IValidateContactForm): any =>
{
    let constraints = 
    {
        firstName:
        {
            presence: true,
            length: 
            {
                minimum: 2,
                message: "must be at least 2 characters"
            }
        },
        lastName:
        {
            presence: true,
            length: 
            {
                minimum: 2,
                message: "must be at least 2 characters"
            }
        },
        email: 
        {
            email: 
            {
                message: "does not look like a valid email"
            }
        },
        subject:
        {
            presence: true,
            length: 
            {
                minimum: 6,
                message: "must be at least 6 characters"
            }
        },
        message:
        {
            presence: true,
            length: 
            {
                minimum: 20,
                message: "must be at least 20 characters"
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
    };

    return Validate(
    {
        firstName: props.firstName,
        lastName:  props.lastName,
        email:     props.email,
        subject:   props.subject,
        message:   props.message,
        terms:     props.terms
    }, 
    constraints);
}

export interface IValidateSigninForm
{
    email: string;
    password: string;
}

export const ValidateSigninForm = (props: IValidateSigninForm): any =>
{
    let constraints =  
    {
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
                message: "must be at least 8 characters"
            }
        },
    }

    return Validate(
    {
        email: props.email,
        password: props.password
    }, 
    constraints);
}

export interface IValidateSignupForm
{
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    terms: boolean;
}

export const ValidateSignupForm = (props: IValidateSignupForm): any =>
{
    let constraints =  
    {
        firstName:
        {
            presence: true,
            length: 
            {
                minimum: 2,
                message: "must be at least 2 characters"
            }
        },
        lastName:
        {
            presence: true,
            length: 
            {
                minimum: 2,
                message: "must be at least 2 characters"
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
                message: "must be at least 8 characters"
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

    return Validate(
    {
        firstName: props.firstName,
        lastName: props.lastName,
        email: props.email,
        password: props.password,
        terms: props.terms
    }, 
    constraints);
}

export interface IValidateResetForm
{
    email: string;
}

export const ValidateResetForm = (props: IValidateResetForm): any =>
{
    let constraints =  
    {
        email: 
        {
            email: 
            {
                message: "does not look like a valid email"
            }
        },
    }

    return Validate(
    {
        email: props.email
    }, 
    constraints);
}

export interface IValidateUpdateForm
{
    newPassword: string;
    verifyPassword: string;
}

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
