import "../../../../setupTests";
import { IValidatePasswordForm, ValidatePasswordForm } from "..";

describe("Verify validation methods.", () => 
{
    it("When Password Form is filled correctly. Should return undefined.", () => 
    {
        // Arrange
        const form: IValidatePasswordForm = 
        {
            oldPassword: "123456789abcde",
            newPassword: "abcde123456",
            confirmPassword: "abcde123456"
        }

        // Act
        const result = ValidatePasswordForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("When Password Form is filled incorrectly. Should return defined.", () => 
    {
        // Arrange
        const form1: IValidatePasswordForm = 
        {
            oldPassword: "",
            newPassword: "",
            confirmPassword: ""
        }

        const form2: IValidatePasswordForm = 
        {
            oldPassword: "456",
            newPassword: "123",
            confirmPassword: "654"
        }

        const form3: IValidatePasswordForm = 
        {
            oldPassword: "",
            newPassword: "abcde123456",
            confirmPassword: ""
        }

        const form4: IValidatePasswordForm = 
        {
            oldPassword: "951",
            newPassword: "",
            confirmPassword: "abcde123456"
        }

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
