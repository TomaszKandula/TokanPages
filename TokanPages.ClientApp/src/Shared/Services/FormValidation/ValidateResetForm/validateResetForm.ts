import Validate from "validate.js";
import { ResetFormInput } from "./interface";

export const ValidateResetForm = (props: ResetFormInput): any =>
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
