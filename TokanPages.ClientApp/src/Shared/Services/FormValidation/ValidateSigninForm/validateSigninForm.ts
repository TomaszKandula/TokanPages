import Validate from "validate.js";
import { IValidateSigninForm } from "./interface";

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
