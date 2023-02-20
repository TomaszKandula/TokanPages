import "../../../../setupTests";
import { GetTextError } from "..";

describe("verify GetTextError method", () => 
{
    it("should return HTML code, when warning object provided.", () => 
    {
        // Arrange
        const input = 
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
