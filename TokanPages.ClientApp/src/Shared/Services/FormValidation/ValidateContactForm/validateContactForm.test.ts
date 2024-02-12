import "../../../../setupTests";
import { ContactFormInput, ValidateContactForm } from "..";

describe("verify contact form validation methods", () => {
    it("should return undefined, when contact form is filled correctly.", () => {
        // Arrange
        const form: ContactFormInput = {
            firstName: "Ester",
            lastName: "Exposito",
            email: "ester.exposito@gmail.com",
            subject: "Vaccation in Spain",
            message: "Let's got to Barcelona...",
            terms: true,
        };

        // Act
        const result = ValidateContactForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("should return defined, when contact form is filled incorrectly.", () => {
        // Arrange
        const form: ContactFormInput = {
            firstName: "",
            lastName: "Deacon",
            email: "john@gmail",
            subject: "Bass guitar lessons",
            message: "",
            terms: false,
        };

        // Act
        const result = ValidateContactForm(form);

        // Assert
        expect(result).toBeDefined();
    });
});
