import "../../../../setupTests";
import { EmailFormInput, ValidateEmailForm } from "..";

describe("Verify validation methods.", () => 
{
    it("When valid email address. Should return undefined.", () => 
    {
        // Arrange
        const form: EmailFormInput = 
        { 
            email: "freddie.mercury@queen.com"
        }

        // Act
        const result = ValidateEmailForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("When invalid email address. Should return defined.", () => 
    {
        // Arrange
        const form: EmailFormInput = 
        {
            email: "brian@queen"
        }

        // Act
        const result = ValidateEmailForm(form);

        // Assert
        expect(result).toBeDefined();
    });
});
