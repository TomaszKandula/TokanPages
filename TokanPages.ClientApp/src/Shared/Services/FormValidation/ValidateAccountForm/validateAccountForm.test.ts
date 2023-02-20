import "../../../../setupTests";
import { AccountFormInput, ValidateAccountForm } from "..";

describe("verify account form validation methods", () => 
{
    it("should return undefined, when account form is filled correctly.", () => 
    {
        // Arrange
        const form: AccountFormInput = 
        {
            firstName: "Ester",
            lastName: "Exposito",
            email: "ester.exposito@gmail.com",
            userAboutText: "Spanish Software Developer"
        }

        // Act
        const result = ValidateAccountForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("should return defined, when contact form filled incorrectly.", () => 
    {
        // Arrange
        const form: AccountFormInput = 
        {
            firstName: "E",
            lastName: "", 
            email: " ",
            userAboutText: "Spanish"
        }

        // Act
        const result = ValidateAccountForm(form);

        // Assert
        expect(result).toBeDefined();
    });
});
