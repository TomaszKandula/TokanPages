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

        const FirstName: string = "Ester";
        const LastName:  string = "Exposito";
        const Email:     string = "ester.exposito@gmail.com";
        const Subject:   string = "Vaccation in Spain";
        const Message:   string = "Let's got to Barcelona...";
        const Terms:     boolean = true;

        expect(ValidateContactForm( 
        { 
            FirstName: FirstName,
            LastName:  LastName, 
            Email:     Email, 
            Subject:   Subject, 
            Message:   Message, 
            Terms:     Terms 
        })).toBeUndefined();

    });

    it("Should not validate Contact Form fields.", () => 
    {

        const FirstName: string = "";
        const LastName:  string = "Deacon";
        const Email:     string = "john@gmail";
        const Subject:   string = "Bass guitar lessons";
        const Message:   string = "";
        const Terms:     boolean = false;

        expect(ValidateContactForm( 
        { 
            FirstName: FirstName,
            LastName:  LastName, 
            Email:     Email, 
            Subject:   Subject, 
            Message:   Message, 
            Terms:     Terms 
        })).toBeDefined();
        
    });

});
