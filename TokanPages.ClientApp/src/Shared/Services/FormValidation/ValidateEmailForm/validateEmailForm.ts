import Validate from "validate.js";
import { ValidateEmailFormProps } from "../Types";

export const ValidateEmailForm = (props: ValidateEmailFormProps): object | undefined => {
    const constraints = {
        email: {
            email: {
                message: "does not look like a valid email",
            },
        },
    };

    return Validate({ email: props.email }, constraints);
};
