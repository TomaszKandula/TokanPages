import { ValidateEmail, ValidateContactForm } from "../validate";

describe("Verify validation methods.", () => 
{
    it("Should validate email address.", () => 
    {
        const TestEmail = "freddie.mercury@queen.com";
        expect(ValidateEmail(TestEmail)).toBeUndefined();
    });

    it("Should not validate email address.", () => 
    {
        const TestEmail = "brian@queen";       
        expect(ValidateEmail(TestEmail)).toBeDefined();
    });

    it("Should validate Contact Form fields.", () => 
    {
        expect(ValidateContactForm( 
        { 
            firstName: "Ester",
            lastName: "Exposito", 
            email: "ester.exposito@gmail.com", 
            subject: "Vaccation in Spain", 
            message: "Let's got to Barcelona...", 
            terms: true 
        })).toBeUndefined();
    });

    it("Should not validate Contact Form fields.", () => 
    {
        expect(ValidateContactForm( 
        { 
            firstName: "",
            lastName: "Deacon", 
            email: "john@gmail", 
            subject: "Bass guitar lessons", 
            message: "", 
            terms: false 
        })).toBeDefined();
    });
});
