import "../../../../setupTests";
import { ValidateResetForm } from "..";
import { ValidateResetFormProps } from "../Types";

describe("verify reset form validation methods", () => {
    it("should return undefined, when reset form filled correctly.", () => {
        // Arrange
        const form: ValidateResetFormProps = {
            email: "ester.exposito@gmail.com",
        };

        // Act
        const result = ValidateResetForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("should return defined, when reset form filled incorrectly.", () => {
        // Arrange
        const form1: ValidateResetFormProps = {
            email: "gmail.com",
        };

        const form2: ValidateResetFormProps = {
            email: "ester@",
        };

        // Act
        const result1 = ValidateResetForm(form1);
        const result2 = ValidateResetForm(form2);

        // Assert
        expect(result1).toBeDefined();
        expect(result2).toBeDefined();
    });
});
