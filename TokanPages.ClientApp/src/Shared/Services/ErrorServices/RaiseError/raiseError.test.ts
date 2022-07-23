import "../../../../setupTests";
import { IRaiseError, RaiseError } from "..";

describe("Verify RaiseError.", () => 
{
    it("Given error object with validation error. When invoke RaiseError. Should execute dispatch and return error text.", () => 
    {
        // Arrange
        function dispatch(args: { type: string; errorObject: string; }) 
        {
            console.debug(`Dispatch has been called with: ${args}`);
        }

        const input: IRaiseError = 
        {
            dispatch: dispatch,
            errorObject: 
            {  
                response: 
                {
                    data:
                    {
                        ErrorCode: "CANNOT_READ_FROM_AZURE_STORAGE",
                        ErrorMessage: "Cannot read from Azure Storage Blob",
                        ValidationErrors: 
                        [
                            {
                                PropertyName: "Id",
                                ErrorCode: "INVALID_GUID",
                                ErrorMessage: "Must be GUID"
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