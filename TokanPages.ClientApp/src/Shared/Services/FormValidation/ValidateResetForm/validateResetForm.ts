import Validate from "validate.js";
import { ValidateResetFormProps } from "../Types";

export const ValidateResetForm = (props: ValidateResetFormProps): object | undefined => {
    const constraints = {
        email: {
            email: {
                message: "does not look like a valid email",
            },
        },
    };

    return Validate(
        {
            email: props.email,
        },
        constraints
    );
};
