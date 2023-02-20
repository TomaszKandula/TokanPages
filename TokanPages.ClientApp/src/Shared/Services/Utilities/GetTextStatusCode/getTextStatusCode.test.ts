import "../../../../setupTests";
import { IGetTextStatusCode, GetTextStatusCode } from "..";
 
describe("verify GetTextStatusCode method", () => 
{
    it("should return HTML code, when warning object is provided.", () => 
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
 