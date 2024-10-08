import "../../../../setupTests";
import { BusinessFormInput } from "../Abstractions/BusinessFormInput";
import { ValidateBusinessForm } from "./validateBusinessForm";

describe("verify business form validation methods", () => {
    it("should return undefined, when business form is filled correctly.", () => {
        // Arrange
        const form: BusinessFormInput = {
            company: "Espana Software",
            firstName: "Ester",
            lastName: "Exposito",
            email: "ester.exposito@gmail.com",
            phone: "(00) 111 222 333",
            description: "Write me an awesome app...",
            techStack: ["C#", "TypeScript", "ReactNative"],
            services: ["web", "mobile"],
        };

        // Act
        const result = ValidateBusinessForm(form);

        // Assert
        expect(result).toBeUndefined();
    });

    it("should return defined, when business form is missing 'company'.", () => {
        // Arrange
        const form: BusinessFormInput = {
            company: "",
            firstName: "Ester",
            lastName: "Exposito",
            email: "ester.exposito@gmail.com",
            phone: "(00) 111 222 333",
            description: "Write me an awesome app...",
            techStack: ["C#", "TypeScript", "ReactNative"],
            services: ["web", "mobile"],
        };

        // Act
        const result = ValidateBusinessForm(form);

        // Assert
        expect(result).toBeDefined();
    });

    it("should return defined, when business form is missing 'firstName'.", () => {
        // Arrange
        const form: BusinessFormInput = {
            company: "Espana Software",
            firstName: "",
            lastName: "Exposito",
            email: "ester.exposito@gmail.com",
            phone: "(00) 111 222 333",
            description: "Write me an awesome app...",
            techStack: ["C#", "TypeScript", "ReactNative"],
            services: ["web", "mobile"],
        };

        // Act
        const result = ValidateBusinessForm(form);

        // Assert
        expect(result).toBeDefined();
    });

    it("should return defined, when business form is missing 'lastName'.", () => {
        // Arrange
        const form: BusinessFormInput = {
            company: "Espana Software",
            firstName: "Ester",
            lastName: "",
            email: "ester.exposito@gmail.com",
            phone: "(00) 111 222 333",
            description: "Write me an awesome app...",
            techStack: ["C#", "TypeScript", "ReactNative"],
            services: ["web", "mobile"],
        };

        // Act
        const result = ValidateBusinessForm(form);

        // Assert
        expect(result).toBeDefined();
    });

    it("should return defined, when business form is missing 'email'.", () => {
        // Arrange
        const form: BusinessFormInput = {
            company: "Espana Software",
            firstName: "Ester",
            lastName: "Exposito",
            email: "",
            phone: "(00) 111 222 333",
            description: "Write me an awesome app...",
            techStack: ["C#", "TypeScript", "ReactNative"],
            services: ["web", "mobile"],
        };

        // Act
        const result = ValidateBusinessForm(form);

        // Assert
        expect(result).toBeDefined();
    });

    it("should return defined, when business form is missing 'description'.", () => {
        // Arrange
        const form: BusinessFormInput = {
            company: "Espana Software",
            firstName: "Ester",
            lastName: "Exposito",
            email: "ester.exposito@gmail.com",
            phone: "(00) 111 222 333",
            description: "",
            techStack: ["C#", "TypeScript", "ReactNative"],
            services: ["web", "mobile"],
        };

        // Act
        const result = ValidateBusinessForm(form);

        // Assert
        expect(result).toBeDefined();
    });

    it("should return defined, when business form is missing 'techStack'.", () => {
        // Arrange
        const form: BusinessFormInput = {
            company: "Espana Software",
            firstName: "Ester",
            lastName: "Exposito",
            email: "ester.exposito@gmail.com",
            phone: "(00) 111 222 333",
            description: "Write me an awesome app...",
            services: ["web", "mobile"],
        };

        // Act
        const result = ValidateBusinessForm(form);

        // Assert
        expect(result).toBeDefined();
    });

    it("should return defined, when business form is missing 'services'.", () => {
        // Arrange
        const form: BusinessFormInput = {
            company: "Espana Software",
            firstName: "Ester",
            lastName: "Exposito",
            email: "ester.exposito@gmail.com",
            phone: "(00) 111 222 333",
            description: "Write me an awesome app...",
            techStack: ["C#", "TypeScript", "ReactNative"],
        };

        // Act
        const result = ValidateBusinessForm(form);

        // Assert
        expect(result).toBeDefined();
    });
});
