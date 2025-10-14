import "../../../../setupTests";
import { ValidateEmailForm } from "..";
import { ValidateEmailFormProps } from "../Types";

describe("verify email address validation methods", () => {
    it("should return undefined, when valid email address.", () => {
        // Arrange
        const form: ValidateEmailFormProps = {
            email: "freddie.mercury@queen.com",
        };

        // Act
        const result = ValidateEmailForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("should return defined, when invalid email address.", () => {
        // Arrange
        const form: ValidateEmailFormProps = {
            email: "brian@queen",
        };

        // Act
        const result = ValidateEmailForm(form);

        // Assert
        expect(result).toBeDefined();
    });
});
