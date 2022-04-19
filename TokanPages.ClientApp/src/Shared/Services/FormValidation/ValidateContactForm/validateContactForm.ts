import Validate from "validate.js";
import { IValidateContactForm } from "./interface";

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
