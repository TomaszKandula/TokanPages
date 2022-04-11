/**
 * @jest-environment jsdom
 */

import { 
    ValidateEmail, 
    ValidateContactForm, 
    ValidateSigninForm,
    ValidateSignupForm,
    ValidateResetForm,
    ValidateUpdateForm
} from "../validate";

describe("Verify validation methods.", () => 
{
    it("When valid email address. Should return undefined.", () => 
    {
        const TestEmail = "freddie.mercury@queen.com";
        expect(ValidateEmail(TestEmail)).toBeUndefined();
    });

    it("When invalid email address. Should return defined.", () => 
    {
        const TestEmail = "brian@queen";       
        expect(ValidateEmail(TestEmail)).toBeDefined();
    });

    it("When Contact Form filled correctly. Should return undefined.", () => 
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

    it("When Contact Form filled incorrectly. Should return defined.", () => 
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

    it("When Sign-in Form filled correctly. Should return undefined.", () => 
    {
        expect(ValidateSigninForm( 
        { 
            email: "ester.exposito@gmail.com",
            password: "ester1990spain"
        })).toBeUndefined();
    });

    it("When Sign-in Form filled incorrectly. Should return defined.", () => 
    {
        expect(ValidateSigninForm( 
        { 
            email: "ester.exposito@",
            password: "e"
        })).toBeDefined();
    });

    it("When Sign-up Form filled correctly. Should return undefined.", () => 
    {
        expect(ValidateSignupForm( 
        { 
            firstName: "ester",
            lastName: "exposito",
            email: "ester.exposito@gmail.com",
            password: "ester1990spain",
            terms: true
        })).toBeUndefined();
    });

    it("When Sign-up Form filled incorrectly. Should return defined.", () => 
    {
        expect(ValidateSignupForm( 
        { 
            firstName: "",
            lastName: "exposito",
            email: "e",
            password: "",
            terms: false
        })).toBeDefined();
    });

    it("When Reset Form filled correctly. Should return undefined.", () => 
    {
        expect(ValidateResetForm( 
        { 
            email: "ester.exposito@gmail.com"
        })).toBeUndefined();
    });

    it("When Reset Form filled incorrectly. Should return defined.", () => 
    {
        expect(ValidateResetForm( 
        { 
            email: "gmail.com"
        })).toBeDefined();

        expect(ValidateResetForm( 
        { 
                email: "ester@"
        })).toBeDefined();
    });

    it("When Update Password Form filled correctly. Should return undefined.", () => 
    {
        expect(ValidateUpdateForm( 
        { 
            newPassword: "abcde123456",
            verifyPassword: "abcde123456"
        })).toBeUndefined();
    });

    it("When Update Password Form filled incorrectly. Should return defined.", () => 
    {
        expect(ValidateUpdateForm( 
        { 
            newPassword: "",
            verifyPassword: ""
        })).toBeDefined();
        
        expect(ValidateUpdateForm( 
        { 
            newPassword: "123",
            verifyPassword: "654"
        })).toBeDefined();

        expect(ValidateUpdateForm( 
        { 
            newPassword: "abcde123456",
            verifyPassword: ""
        })).toBeDefined();

        expect(ValidateUpdateForm( 
        { 
            newPassword: "",
            verifyPassword: "abcde123456"
        })).toBeDefined();
    });
});
