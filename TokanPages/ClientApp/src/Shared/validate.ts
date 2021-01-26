import Validate from "validate.js";

function ValidateEmail(email: string): any
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

interface IValidateContactForm
{
    FirstName: string;
    LastName: string;
    Email: string;
    Subject: string;
    Message: string;
    Terms: boolean;
}

function ValidateContactForm(props: IValidateContactForm): any
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
        FirstName: props.FirstName,
        LastName:  props.LastName,
        Email:     props.Email,
        Subject:   props.Subject,
        Message:   props.Message,
        Terms:     props.Terms
    }, 
    constraints);
}

export 
{
    ValidateEmail, 
    ValidateContactForm
}
