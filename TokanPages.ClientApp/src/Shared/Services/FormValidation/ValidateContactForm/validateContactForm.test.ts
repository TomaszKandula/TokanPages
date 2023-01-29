import "../../../../setupTests";
import { ContactFormInput, ValidateContactForm } from "..";

describe("Verify validation methods.", () => 
{
    it("When Contact Form filled correctly. Should return undefined.", () => 
    {
        // Arrange
        const form: ContactFormInput = 
        {
            firstName: "Ester",
            lastName: "Exposito", 
            email: "ester.exposito@gmail.com", 
            subject: "Vaccation in Spain", 
            message: "Let's got to Barcelona...", 
            terms: true 
        }

        // Act
        const result = ValidateContactForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("When Contact Form filled incorrectly. Should return defined.", () => 
    {
        // Arrange
        const form: ContactFormInput = 
        {
            firstName: "",
            lastName: "Deacon", 
            email: "john@gmail", 
            subject: "Bass guitar lessons", 
            message: "", 
            terms: false 
        }

        // Act
        const result = ValidateContactForm(form);

        // Assert
        expect(result).toBeDefined();
    });
});
