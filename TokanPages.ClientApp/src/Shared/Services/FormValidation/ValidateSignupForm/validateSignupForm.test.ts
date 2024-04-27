import "../../../../setupTests";
import { SignupFormInput, ValidateSignupForm } from "..";

const testContent = {
    emailInvalid: "does not look like a valid email.",
    nameInvalid: "must be between 1..255 characters.",
    surnameInvalid: "must be between 1..255 characters.",
    passwordInvalid: "must be between 8..50 characters.",
    missingTerms: "^You must accept terms of use and privacy policy.",
    missingChar: "Missing char",
    missingLargeLetter: "Missing large letter",
    missingNumber: "Missing number",
    missingSmallLetter: "Missing small letter",
};

describe("verify signup form validation methods", () => {
    it("should return defined, when missing first name.", () => {
        // Arrange
        const form: SignupFormInput = {
            firstName: "",
            lastName: "exposito",
            email: "ester@gmail.com",
            password: "QwertyQwerty#2020%",
            content: testContent,
            terms: true
        };

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        expect(result).toBeDefined();
    });

    it("should return defined, when missing last name.", () => {
        // Arrange
        const form: SignupFormInput = {
            firstName: "ester",
            lastName: "",
            email: "ester@gmail.com",
            password: "QwertyQwerty#2020%",
            content: testContent,
            terms: true
        };

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        expect(result).toBeDefined();
    });

    it("should return defined, when have invalid email.", () => {
        // Arrange
        const form: SignupFormInput = {
            firstName: "ester",
            lastName: "exposito",
            email: "ester",
            password: "QwertyQwerty#2020%",
            content: testContent,
            terms: true
        };

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        expect(result).toBeDefined();
    });

    it("should return defined, when have missing request field.", () => {
        // Arrange
        const form: SignupFormInput = {
            firstName: "ester",
            lastName: "exposito",
            email: "ester",
            password: "QwertyQwerty#2020%",
            content: testContent,
            terms: true
        };

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        expect(result).toBeDefined();
    });

    it("should return defined, when password is too short.", () => {
        // Arrange
        const form: SignupFormInput = {
            firstName: "ester",
            lastName: "exposito",
            email: "ester@gmail.com",
            password: "qwerty",
            content: testContent,
            terms: true
        };

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        console.debug(result);
        expect(result).toBeDefined();
    });

    it("should return defined, when password does not contain: number, sign, and large letter.", () => {
        // Arrange
        const form: SignupFormInput = {
            firstName: "ester",
            lastName: "exposito",
            email: "ester@gmail.com",
            password: "qwertyqwerty",
            content: testContent,
            terms: true
        };

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        console.debug(result);
        expect(result).toBeDefined();
    });

    it("should return defined, when password does not contain: sign, and large letter.", () => {
        // Arrange
        const form: SignupFormInput = {
            firstName: "ester",
            lastName: "exposito",
            email: "ester@gmail.com",
            password: "qwertyqwerty2020",
            content: testContent,
            terms: true
        };

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        console.debug(result);
        expect(result).toBeDefined();
    });

    it("should return defined, when password does not contain: large letter.", () => {
        // Arrange
        const form: SignupFormInput = {
            firstName: "ester",
            lastName: "exposito",
            email: "ester@gmail.com",
            password: "qwertyqwerty#2020%",
            content: testContent,
            terms: true
        };

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        console.debug(result);
        expect(result).toBeDefined();
    });

    it("should return undefined, when password contains all the required characters.", () => {
        // Arrange
        const form: SignupFormInput = {
            firstName: "ester",
            lastName: "exposito",
            email: "ester@gmail.com",
            password: "QwertyQwerty#2020%",
            content: testContent,
            terms: true
        };

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        console.debug(result);
        expect(result).toBeUndefined();
    });
});
