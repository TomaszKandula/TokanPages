import "../../../../setupTests";
import { ResetFormInput, ValidateResetForm } from "..";

describe("Verify validation methods.", () => 
{
    it("When Reset Form filled correctly. Should return undefined.", () => 
    {
        // Arrange
        const form: ResetFormInput = 
        {
            email: "ester.exposito@gmail.com"
        }

        // Act
        const result = ValidateResetForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("When Reset Form filled incorrectly. Should return defined.", () => 
    {
        // Arrange
        const form1: ResetFormInput = 
        {
            email: "gmail.com"
        }

        const form2: ResetFormInput = 
        {
            email: "ester@"
        }

        // Act
        const result1 = ValidateResetForm(form1);
        const result2 = ValidateResetForm(form2);

        // Assert
        expect(result1).toBeDefined();
        expect(result2).toBeDefined();
    });
});
