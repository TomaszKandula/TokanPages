import "../../../../setupTests";
import { RaiseError } from "..";

describe("verify RaiseError method", () => 
{
    it("should execute dispatch and return error text, when error object with validation error provided. ", () => 
    {
        // Arrange
        function dispatch(args: { type: string; errorObject: string; }) 
        {
            console.debug(`Dispatch has been called with: ${args}`);
        }

        const input = 
        {
            dispatch: dispatch,
            errorObject: 
            {  
                response: 
                {
                    data:
                    {
                        errorCode: "CANNOT_READ_FROM_AZURE_STORAGE",
                        errorMessage: "Cannot read from Azure Storage Blob",
                        validationErrors: 
                        [
                            {
                                propertyName: "Id",
                                errorCode: "INVALID_GUID",
                                errorMessage: "Must be GUID"
                            }
                        ]    
                    }
                }
            }
        }

        const expectation = "Cannot read from Azure Storage Blob. Validation errors have been found.";

        // Act
        const result = RaiseError(input);

        // Assert
        expect(result).toBe(expectation);
    });
});