import "../../../../setupTests";
import { IValidateUpdateForm, ValidateUpdateForm } from "..";

describe("Verify validation methods.", () => 
{
    it("When Update Password Form filled correctly. Should return undefined.", () => 
    {
        // Arrange
        const form: IValidateUpdateForm = 
        {
            newPassword: "abcde123456",
            verifyPassword: "abcde123456"
        }

        // Act
        const result = ValidateUpdateForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("When Update Password Form filled incorrectly. Should return defined.", () => 
    {
        // Arrange
        const form1: IValidateUpdateForm = 
        {
            newPassword: "",
            verifyPassword: ""
        }

        const form2: IValidateUpdateForm = 
        {
            newPassword: "123",
            verifyPassword: "654"
        }

        const form3: IValidateUpdateForm = 
        {
            newPassword: "abcde123456",
            verifyPassword: ""
        }

        const form4: IValidateUpdateForm = 
        {
            newPassword: "",
            verifyPassword: "abcde123456"
        }

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
