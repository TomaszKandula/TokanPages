import Validate from "validate.js";
import { SignupFormInput } from "./interface";

export const ValidateSignupForm = (props: SignupFormInput): any =>
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
