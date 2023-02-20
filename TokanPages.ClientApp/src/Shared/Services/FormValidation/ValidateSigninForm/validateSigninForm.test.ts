import "../../../../setupTests";
import { SigninFormInput, ValidateSigninForm } from "..";

describe("verify signin form validation methods", () => 
{
    it("should return undefined, when signin form is filled correctly.", () => 
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

    it("should return defined, when signin form is filled incorrectly.", () => 
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
