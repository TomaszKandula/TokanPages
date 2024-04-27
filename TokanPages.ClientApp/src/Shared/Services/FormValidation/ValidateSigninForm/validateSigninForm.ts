import Validate from "validate.js";
import { SigninFormInput } from "..";

export const ValidateSigninForm = (props: SigninFormInput): any => {
    let constraints = {
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
