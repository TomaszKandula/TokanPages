import Validate from "validate.js";
import { EmailFormInput } from "./interface";

export const ValidateEmailForm = (props: EmailFormInput): any =>
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
