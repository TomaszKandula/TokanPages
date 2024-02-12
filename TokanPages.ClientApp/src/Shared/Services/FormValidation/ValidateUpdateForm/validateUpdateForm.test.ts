import "../../../../setupTests";
import { UpdateFormInput, ValidateUpdateForm } from "..";

describe("verify update password form validation methods", () => {
    it("should return undefined, when update password form is filled correctly.", () => {
        // Arrange
        const form: UpdateFormInput = {
            newPassword: "Abcde#123456",
            verifyPassword: "Abcde#123456",
        };

        // Act
        const result = ValidateUpdateForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("should return defined, when update password form is filled incorrectly.", () => {
        // Arrange
        const form1: UpdateFormInput = {
            newPassword: "",
            verifyPassword: "",
        };

        const form2: UpdateFormInput = {
            newPassword: "123",
            verifyPassword: "654",
        };

        const form3: UpdateFormInput = {
            newPassword: "abcde123456",
            verifyPassword: "",
        };

        const form4: UpdateFormInput = {
            newPassword: "",
            verifyPassword: "abcde123456",
        };

        // Act
        const result1 = ValidateUpdateForm(form1);
        const result2 = ValidateUpdateForm(form2);
        const result3 = ValidateUpdateForm(form3);
        const result4 = ValidateUpdateForm(form4);

        // Assert
        expect(result1).toBeDefined();
        expect(result2).toBeDefined();
        expect(result3).toBeDefined();
        expect(result4).toBeDefined();
    });
});
