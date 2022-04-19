import Validate from "validate.js";
import { IValidateResetForm } from "./interface";

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
