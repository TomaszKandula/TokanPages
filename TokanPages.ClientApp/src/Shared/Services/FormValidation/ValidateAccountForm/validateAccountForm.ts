import Validate from "validate.js";
import { ValidateAccountFormProps } from "../Types";

export const ValidateAccountForm = (props: ValidateAccountFormProps): object | undefined => {
    const constraints = {
        firstName: {
            presence: true,
            length: {
                minimum: 2,
                message: "must be at least 2 characters",
            },
        },
        lastName: {
            presence: true,
            length: {
                minimum: 2,
                message: "must be at least 2 characters",
            },
        },
        email: {
            email: {
                message: "does not look like a valid email",
            },
        },
        description: {
            presence: true,
            length: {
                minimum: 2,
                message: "must be at least 2 characters",
            },
        },
    };

    return Validate(
        {
            firstName: props.firstName,
            lastName: props.lastName,
            email: props.email,
            description: props.description,
        },
        constraints
    );
};
