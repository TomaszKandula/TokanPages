import Validate from "validate.js";
import { EmailFormInput } from "..";

export const ValidateEmailForm = (props: EmailFormInput): object | undefined => {
    const constraints = {
        email: {
            email: {
                message: "does not look like a valid email",
            },
        },
    };

    return Validate({ email: props.email }, constraints);
};
