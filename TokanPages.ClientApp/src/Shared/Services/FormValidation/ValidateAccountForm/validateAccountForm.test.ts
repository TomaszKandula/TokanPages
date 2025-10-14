import "../../../../setupTests";
import { ValidateAccountForm } from "..";
import { ValidateAccountFormProps } from "../Types";

describe("verify account form validation methods", () => {
    it("should return undefined, when account form is filled correctly.", () => {
        // Arrange
        const form: ValidateAccountFormProps = {
            firstName: "Ester",
            lastName: "Exposito",
            email: "ester.exposito@gmail.com",
            description: "Spanish Software Developer",
        };

        // Act
        const result = ValidateAccountForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("should return defined, when contact form filled incorrectly.", () => {
        // Arrange
        const form: ValidateAccountFormProps = {
            firstName: "E",
            lastName: "",
            email: " ",
            description: "Spanish",
        };

        // Act
        const result = ValidateAccountForm(form);

        // Assert
        expect(result).toBeDefined();
    });
});
