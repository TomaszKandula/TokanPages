import "../../../../setupTests";
import { PasswordFormInput, ValidatePasswordForm } from "..";

const testContent = {
    missingChar: "Missing char",
    missingLargeLetter: "Missing large letter",
    missingNumber: "Missing number",
    missingSmallLetter: "Missing small letter",
};

describe("verify new password validation methods", () => {
    it("should return undefined, when passwords are correct.", () => {
        // Arrange
        const form: PasswordFormInput = {
            oldPassword: "123456789abcde",
            newPassword: "Abcde#123456",
            confirmPassword: "Abcde#123456",
            content: testContent,
        };

        // Act
        const result = ValidatePasswordForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("should return defined, when passwords are incorrect.", () => {
        // Arrange
        const form1: PasswordFormInput = {
            oldPassword: "",
            newPassword: "",
            confirmPassword: "",
            content: testContent,
        };

        const form2: PasswordFormInput = {
            oldPassword: "456",
            newPassword: "123",
            confirmPassword: "654",
            content: testContent,
        };

        const form3: PasswordFormInput = {
            oldPassword: "",
            newPassword: "abcde123456",
            confirmPassword: "",
            content: testContent,
        };

        const form4: PasswordFormInput = {
            oldPassword: "951",
            newPassword: "",
            confirmPassword: "abcde123456",
            content: testContent,
        };

        // Act
        const result1 = ValidatePasswordForm(form1);
        const result2 = ValidatePasswordForm(form2);
        const result3 = ValidatePasswordForm(form3);
        const result4 = ValidatePasswordForm(form4);

        // Assert
        expect(result1).toBeDefined();
        expect(result2).toBeDefined();
        expect(result3).toBeDefined();
        expect(result4).toBeDefined();
    });
});
