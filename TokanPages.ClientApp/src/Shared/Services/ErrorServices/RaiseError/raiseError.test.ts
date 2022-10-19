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