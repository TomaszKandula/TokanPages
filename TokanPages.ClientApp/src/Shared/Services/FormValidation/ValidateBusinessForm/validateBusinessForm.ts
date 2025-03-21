import Validate from "validate.js";
import { BusinessFormInput } from "../Abstractions/BusinessFormInput";

export const ValidateBusinessForm = (props: BusinessFormInput): object | undefined => {
    let constraints = {
        company: {
            presence: true,
            length: {
                minimum: 2,
                maximum: 255,
                message: "must be between 2 and 255 characters",
            },
        },
        firstName: {
            presence: true,
            length: {
                minimum: 2,
                maximum: 255,
                message: "must be between 2 and 255 characters",
            },
        },
        lastName: {
            presence: true,
            length: {
                minimum: 2,
                maximum: 255,
                message: "must be between 2 and 255 characters",
            },
        },
        email: {
            email: {
                message: "does not look like a valid email",
            },
        },
        phone: {
            presence: true,
            length: {
                minimum: 2,
                maximum: 17,
                message: "does not look like a valid phone number",
            },
        },
        description: {
            presence: true,
            length: {
                minimum: 2,
                maximum: 10000,
                message: "must be between 2 and 10 000 characters",
            },
        },
        techStack: {
            presence: true,
            length: {
                minimum: 1,
                maximum: 100,
                message: "must be selected",
            },
        },
        services: {
            presence: true,
            length: {
                minimum: 1,
                maximum: 100,
                message: "must be selected",
            },
        },
    };

    return Validate(
        {
            company: props.company,
            firstName: props.firstName,
            lastName: props.lastName,
            email: props.email,
            phone: props.phone,
            description: props.description,
            techStack: props.techStack,
            services: props.services,
        },
        constraints
    );
};
