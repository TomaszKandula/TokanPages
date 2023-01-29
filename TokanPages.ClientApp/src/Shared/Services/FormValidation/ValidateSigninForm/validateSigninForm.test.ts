import "../../../../setupTests";
import { SigninFormInput, ValidateSigninForm } from "..";

describe("Verify validation methods.", () => 
{
    it("When Sign-in Form filled correctly. Should return undefined.", () => 
    {
        // Arrange
        const form: SigninFormInput = 
        {
            email: "ester.exposito@gmail.com",
            password: "ester1990spain"
        }

        // Act
        const result = ValidateSigninForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("When Sign-in Form filled incorrectly. Should return defined.", () => 
    {
        // Arrange
        const form: SigninFormInput = 
        {
            email: "ester.exposito@",
            password: "e"
        }

        // Act
        const result = ValidateSigninForm(form);

        // Assert
        expect(result).toBeDefined();
    });
});
