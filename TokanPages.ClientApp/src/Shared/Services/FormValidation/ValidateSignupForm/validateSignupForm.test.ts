import "../../../../setupTests";
import { SignupFormInput, ValidateSignupForm } from "..";

describe("Verify validation methods.", () => 
{
    it("should return defined, when missing first name.", () => 
    {
        // Arrange
        const form: SignupFormInput = 
        {
            firstName: "",
            lastName: "exposito",
            email: "ester@gmail.com",
            password: "QwertyQwerty#2020%",
            terms: true
        }

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        expect(result).toBeDefined();
    });

    it("should return defined, when missing last name.", () => 
    {
        // Arrange
        const form: SignupFormInput = 
        {
            firstName: "ester",
            lastName: "",
            email: "ester@gmail.com",
            password: "QwertyQwerty#2020%",
            terms: true
        }

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        expect(result).toBeDefined();
    });

    it("should return defined, when have invalid email.", () => 
    {
        // Arrange
        const form: SignupFormInput = 
        {
            firstName: "ester",
            lastName: "exposito",
            email: "ester",
            password: "QwertyQwerty#2020%",
            terms: true
        }

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        expect(result).toBeDefined();
    });

    it("should return defined, when password is too short.", () => 
    {
        // Arrange
        const form: SignupFormInput = 
        {
            firstName: "ester",
            lastName: "exposito",
            email: "ester@gmail.com",
            password: "qwerty",
            terms: true
        }

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        console.debug(result);
        expect(result).toBeDefined();
    });

    it("should return defined, when password does not contain: number, sign, and large letter.", () => 
    {
        // Arrange
        const form: SignupFormInput = 
        {
            firstName: "ester",
            lastName: "exposito",
            email: "ester@gmail.com",
            password: "qwertyqwerty",
            terms: true
        }

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        console.debug(result);
        expect(result).toBeDefined();
    });

    it("should return defined, when password does not contain: sign, and large letter.", () => 
    {
        // Arrange
        const form: SignupFormInput = 
        {
            firstName: "ester",
            lastName: "exposito",
            email: "ester@gmail.com",
            password: "qwertyqwerty2020",
            terms: true
        }

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        console.debug(result);
        expect(result).toBeDefined();
    });

    it("should return defined, when password does not contain: large letter.", () => 
    {
        // Arrange
        const form: SignupFormInput = 
        {
            firstName: "ester",
            lastName: "exposito",
            email: "ester@gmail.com",
            password: "qwertyqwerty#2020%",
            terms: true
        }

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        console.debug(result);
        expect(result).toBeDefined();
    });

    it("should return undefined, when password does not contain all required characters.", () => 
    {
        // Arrange
        const form: SignupFormInput = 
        {
            firstName: "ester",
            lastName: "exposito",
            email: "ester@gmail.com",
            password: "QwertyQwerty#2020%",
            terms: true
        }

        // Act
        const result = ValidateSignupForm(form);

        // Assert
        console.debug(result);
        expect(result).toBeDefined();
    });
});
