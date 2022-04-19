import Validate from "validate.js";
import { IValidateEmailForm } from "./interface";

export const ValidateEmailForm = (props: IValidateEmailForm): any =>
{
    const constraints = 
    {
        email: 
        {
            email: 
            {
                message: "does not look like a valid email"
            }
        }
    };

    return Validate({ email: props.email }, constraints);
}
