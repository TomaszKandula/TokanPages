import Validate from "validate.js";
import { SignupFormInput } from "..";

import { ContainNumber, HasProperty, HaveLargeLetter, HaveSmallLetter, HaveSpecialCharacter } from "../Helpers";

export const ValidateSignupForm = (props: SignupFormInput): any => {
    const nameConstraints = {
        firstName: {
            presence: true,
            length: {
                minimum: 1,
                maximum: 255,
                message: props.content.nameInvalid,
            },
        },
        lastName: {
            presence: true,
            length: {
                minimum: 1,
                maximum: 255,
                message: props.content.surnameInvalid,
            },
        },
    };

    const baseConstraints = {
        email: {
            email: {
                message: props.content.emailInvalid,
            },
        },
        password: {
            presence: true,
            length: {
                minimum: 8,
                maximum: 50,
                message: props.content.passwordInvalid,
            },
        },
    };

    const constraints = {
        ...baseConstraints,
        ...nameConstraints,
        terms: {
            presence: true,
            inclusion: {
                within: [true],
                message: props.content.missingTerms,
            },
        },
    };

    let result = Validate(
        {
            firstName: props.firstName,
            lastName: props.lastName,
            email: props.email,
            password: props.password,
            terms: props.terms
        },
        constraints
    );

    if (!HaveSpecialCharacter(props.password)) {
        const data = HasProperty(result, "password")
            ? [...result.password, "Password " + props.content.missingChar]
            : [props.content.missingChar];
        result = { ...result, password: data };
    }

    if (!ContainNumber(props.password)) {
        const data = HasProperty(result, "password")
            ? [...result.password, "Password " + props.content.missingNumber]
            : [props.content.missingNumber];
        result = { ...result, password: data };
    }

    if (!HaveLargeLetter(props.password)) {
        const data = HasProperty(result, "password")
            ? [...result.password, "Password " + props.content.missingLargeLetter]
            : [props.content.missingLargeLetter];
        result = { ...result, password: data };
    }

    if (!HaveSmallLetter(props.password)) {
        const data = HasProperty(result, "password")
            ? [...result.password, "Password " + props.content.missingSmallLetter]
            : [props.content.missingSmallLetter];
        result = { ...result, password: data };
    }

    return result;
};
