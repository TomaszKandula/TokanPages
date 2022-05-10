import Validate from "validate.js";
import { IValidateAccountForm } from "./interface";

export const ValidateAccountForm = (props: IValidateAccountForm): any =>
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
        shortBio:
        {
            presence: true,
            length: 
            {
                minimum: 2,
                message: "must be at least 2 characters"
            }
        }
    };

    return Validate(
    {
        firstName: props.firstName,
        lastName: props.lastName,
        shortBio: props.shortBio
    }, 
    constraints);
}
