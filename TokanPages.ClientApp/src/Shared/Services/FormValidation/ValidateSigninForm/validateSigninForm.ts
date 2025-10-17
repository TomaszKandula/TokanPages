import Validate from "validate.js";
import { ValidateSigninFormProps } from "../Types";

export const ValidateSigninForm = (props: ValidateSigninFormProps): object | undefined => {
    const constraints = {
        email: {
            email: {
                message: props.content.emailInvalid,
            },
        },
        password: {
            presence: true,
            length: {
                minimum: 8,
                message: props.content.passwordInvalid,
            },
        },
    };

    return Validate(
        {
            email: props.email,
            password: props.password,
        },
        constraints
    );
};
