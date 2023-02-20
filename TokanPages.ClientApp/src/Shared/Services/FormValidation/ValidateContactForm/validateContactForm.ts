import Validate from "validate.js";
import { ContactFormInput } from "..";

export const ValidateContactForm = (props: ContactFormInput): any =>
{
    let constraints = 
    {
        firstName:
        {
            presence: true,
            length: 
            {
                minimum: 2,
                maximum: 255,
                message: "must be between 2 and 255 characters"
            }
        },
        lastName:
        {
            presence: true,
            length: 
            {
                minimum: 2,
                maximum: 255,
                message: "must be between 2 and 255 characters"
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
                maximum: 255,
                message: "must be between 6 and 255 characters"
            }
        },
        message:
        {
            presence: true,
            length: 
            {
                minimum: 20,
                maximum: 4096,
                message: "must be between 20 and 10000 characters"
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
