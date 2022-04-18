/**
 * @jest-environment jsdom
 */

import { IValidateEmailForm } from "./interface";
import { ValidateEmailForm } from "./validateEmailForm";

describe("Verify validation methods.", () => 
{
    it("When valid email address. Should return undefined.", () => 
    {
        // Arrange
        const form: IValidateEmailForm = 
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
        const form: IValidateEmailForm = 
        {
            email: "brian@queen"
        }

        // Act
        const result = ValidateEmailForm(form);

        // Assert
        expect(result).toBeDefined();
    });
});
