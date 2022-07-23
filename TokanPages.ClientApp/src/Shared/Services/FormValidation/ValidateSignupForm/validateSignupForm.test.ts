import "../../../../setupTests";
import { IValidateSignupForm, ValidateSignupForm } from "..";

describe("Verify validation methods.", () => 
{
    it("When Sign-up Form filled correctly. Should return undefined.", () => 
    {
        // Arrange
        const form: IValidateSignupForm = 
        {
            firstName: "ester",
            lastName: "exposito",
            email: "ester.exposito@gmail.com",
            password: "ester1990spain",
            terms: true
        }

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("When Sign-up Form filled incorrectly. Should return defined.", () => 
    {
        // Arrange
        const form: IValidateSignupForm = 
        {
            firstName: "",
            lastName: "exposito",
            email: "e",
            password: "",
            terms: false
        }

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        expect(result).toBeDefined();
    });
});