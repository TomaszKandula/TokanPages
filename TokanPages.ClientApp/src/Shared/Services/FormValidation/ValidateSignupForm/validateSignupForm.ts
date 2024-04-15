import Validate from "validate.js";
import { SignupFormInput } from "..";
//import { containNumber, haveLargeLetter, haveSmallLetter, haveSpecialCharacter } from "../Helpers";
import {
    PASSWORD_MISSING_CHAR,
    PASSWORD_MISSING_LARGE_LETTER,
    PASSWORD_MISSING_NUMBER,
    PASSWORD_MISSING_SMALL_LETTER,
} from "../../../../Shared/constants";

import { ContainNumber, HasProperty, HaveLargeLetter, HaveSmallLetter, HaveSpecialCharacter } from "../Helpers";

export const ValidateSignupForm = (props: SignupFormInput): any => {
    let constraints = {
        firstName: {
            presence: true,
            length: {
                minimum: 1,
                maximum: 255,
                message: "must be between 1..255 characters",
            },
        },
        lastName: {
            presence: true,
            length: {
                minimum: 1,
                maximum: 255,
                message: "must be between 1..255 characters",
            },
        },
        email: {
            email: {
                message: "does not look like a valid email",
            },
        },
        password: {
            presence: true,
            length: {
                minimum: 8,
                maximum: 50,
                message: "must be between 8..50 characters",
            },
        },
        terms: {
            presence: true,
            inclusion: {
                within: [true],
                message: "^You must accept terms of use and privacy policy",
            },
        },
    };

    let result = Validate(
        {
            firstName: props.firstName,
            lastName: props.lastName,
            email: props.email,
            password: props.password,
            terms: props.terms,
        },
        constraints
    );

    //TODO: use below methods after issues are resolved
    // containNumber(props.password, result);
    // haveSpecialCharacter(props.password, result);
    // haveLargeLetter(props.password, result);
    // haveSmallLetter(props.password, result);

    if (!HaveSpecialCharacter(props.password)) {
        const data = HasProperty(result, "password")
            ? [...result.password, "Password " + PASSWORD_MISSING_CHAR]
            : [PASSWORD_MISSING_CHAR];
        result = { ...result, password: data };
    }

    if (!ContainNumber(props.password)) {
        const data = HasProperty(result, "password")
            ? [...result.password, "Password " + PASSWORD_MISSING_NUMBER]
            : [PASSWORD_MISSING_NUMBER];
        result = { ...result, password: data };
    }

    if (!HaveLargeLetter(props.password)) {
        const data = HasProperty(result, "password")
            ? [...result.password, "Password " + PASSWORD_MISSING_LARGE_LETTER]
            : [PASSWORD_MISSING_LARGE_LETTER];
        result = { ...result, password: data };
    }

    if (!HaveSmallLetter(props.password)) {
        const data = HasProperty(result, "password")
            ? [...result.password, "Password " + PASSWORD_MISSING_SMALL_LETTER]
            : [PASSWORD_MISSING_SMALL_LETTER];
        result = { ...result, password: data };
    }

    return result;
};
