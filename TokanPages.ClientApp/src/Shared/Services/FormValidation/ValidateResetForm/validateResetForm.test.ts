/**
 * @jest-environment jsdom
 */

import { IValidateResetForm } from "./interface";
import { ValidateResetForm } from "./validateResetForm";

describe("Verify validation methods.", () => 
{
    it("When Reset Form filled correctly. Should return undefined.", () => 
    {
        // Arrange
        const form: IValidateResetForm = 
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
        const form1: IValidateResetForm = 
        {
            email: "gmail.com"
        }

        const form2: IValidateResetForm = 
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
