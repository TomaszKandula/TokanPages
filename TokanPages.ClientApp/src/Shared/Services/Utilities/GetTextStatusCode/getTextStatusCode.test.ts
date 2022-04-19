/**
 * @jest-environment jsdom
 */
import { GetTextStatusCode } from "./getTextStatusCode";
import { IGetTextStatusCode } from "./interface";
 
describe("Verify GetTextStatusCode.", () => 
{
    it("Given warning object. When invoke GetTextStatusCode. Should return HTML code.", () => 
    {
        // Arrange
        const input: IGetTextStatusCode = 
        {
            statusCode: 404
        } 
 
        const expectation = "Received unexpected status code: 404. Please contact IT Support";
 
        // Act
        const result = GetTextStatusCode(input);
 
        // Assert
        expect(result).toBe(expectation);
    });
});
 