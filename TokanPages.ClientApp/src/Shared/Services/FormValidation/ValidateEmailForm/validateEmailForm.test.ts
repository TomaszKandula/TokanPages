import "../../../../setupTests";
import { EmailFormInput, ValidateEmailForm } from "..";

describe("verify email address validation methods", () => {
    it("should return undefined, when valid email address.", () => {
        // Arrange
        const form: EmailFormInput = {
            email: "freddie.mercury@queen.com",
        };

        // Act
        const result = ValidateEmailForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("should return defined, when invalid email address.", () => {
        // Arrange
        const form: EmailFormInput = {
            email: "brian@queen",
        };

        // Act
        const result = ValidateEmailForm(form);

        // Assert
        expect(result).toBeDefined();
    });
});
