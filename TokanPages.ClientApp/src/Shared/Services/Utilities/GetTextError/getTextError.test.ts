/**
 * @jest-environment jsdom
 */
import { GetTextError } from "./getTextError";
import { IGetTextError } from "./interface";

describe("Verify helper methods.", () => 
{
    it("Given warning object. When GetTextWarning. Should return HTML code.", () => 
    {
        // Arrange
        const input: IGetTextError = 
        {
            error: "SMTP connection failed",
            template: "<p>Ouch!</p><p>The email could not be sent.</p><p>{ERROR}.</p>"
        } 

        const expectation = "<p>Ouch!</p><p>The email could not be sent.</p><p>SMTP connection failed.</p>";

        // Act
        const result = GetTextError(input);

        // Assert
        expect(result).toBe(expectation);
    });
});
