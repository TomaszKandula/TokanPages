/**
 * @jest-environment jsdom
 */

import { IRaiseError } from "./interface";
import { RaiseError } from "./raiseError";

describe("Verify helper methods.", () => 
{
    it("Given . When RaiseError. Should execute dispatch and return empty string.", () => 
    {
        // Arrange
        function dispatch(args: { type: string; errorObject: string; }) 
        {
            console.debug(`Dispatch has been caled with: ${args}`);
        }

        const input: IRaiseError = 
        {
            dispatch: dispatch,
            errorObject: 
            {  
                errorCode: "UNEXPECTED_ERROR",
                errorMessage: "Unexpected error"
            }
        }

        const expectation = "";

        // Act
        const result = RaiseError(input);

        // Assert
        expect(result).toBe(expectation);
    });
});