import "../../../../setupTests";
import { SigninFormInput, ValidateSigninForm } from "..";

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

describe("verify signin form validation methods", () => {
    it("should return undefined, when signin form is filled correctly.", () => {
        // Arrange
        const form: SigninFormInput = {
            email: "ester.exposito@gmail.com",
            password: "ester1990spain",
            content: testContent,
        };

        // Act
        const result = ValidateSigninForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("should return defined, when signin form is filled incorrectly.", () => {
        // Arrange
        const form: SigninFormInput = {
            email: "ester.exposito@",
            password: "e",
            content: testContent,
        };

        // Act
        const result = ValidateSigninForm(form);

        // Assert
        expect(result).toBeDefined();
    });
});
