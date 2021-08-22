import Validate from "validate.js";

export const ValidateEmail = (email: string): any =>
{
    let constraints = 
    {
        Email: 
        {
            email: 
            {
                message: "does not look like a valid email"
            }
        }
    };

    return Validate({ Email: email }, constraints);
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
        FirstName:
        {
            presence: true,
            length: 
            {
                minimum: 2,
                message: "must be at least 2 characters"
            }
        },
        LastName:
        {
            presence: true,
            length: 
            {
                minimum: 2,
                message: "must be at least 2 characters"
            }
        },
        Email: 
        {
            email: 
            {
                message: "does not look like a valid email"
            }
        },
        Subject:
        {
            presence: true,
            length: 
            {
                minimum: 6,
                message: "must be at least 6 characters"
            }
        },
        Message:
        {
            presence: true,
            length: 
            {
                minimum: 20,
                message: "must be at least 20 characters"
            }
        },
        Terms:
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
        FirstName: props.firstName,
        LastName:  props.lastName,
        Email:     props.email,
        Subject:   props.subject,
        Message:   props.message,
        Terms:     props.terms
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
    }

    return Validate(
    {
        firstName: props.firstName,
        lastName: props.lastName,
        email: props.email,
        password: props.password
    }, 
    constraints);
}
