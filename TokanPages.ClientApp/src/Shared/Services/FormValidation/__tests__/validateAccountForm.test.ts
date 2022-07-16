import "../../../../setupTests";
import { IValidateAccountForm, ValidateAccountForm } from "../../FormValidation";

describe("Verify validation methods.", () => 
{
    it("When Account Form filled correctly. Should return undefined.", () => 
    {
        // Arrange
        const form: IValidateAccountForm = 
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

    it("When Contact Form filled incorrectly. Should return defined.", () => 
    {
        // Arrange
        const form: IValidateAccountForm = 
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
