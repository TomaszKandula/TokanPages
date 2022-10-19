import "../../../../setupTests";
import { VALIDATION_ERRORS } from "../../../constants";
import { IErrorDto } from "../../../../Api/Models";
import { IGetErrorMessage, GetErrorMessage } from "..";

describe("Verify GetErrorMessage.", () => 
{
    it("Given valid JSON object. When invoke GetErrorMessage. Should return translated error message.", () => 
    {
        // Arrange
        const jsonObject: string = `
        {
            "response": 
            {
                "data":
                {
                    "errorCode": "USERNAME_ALREADY_EXISTS",
                    "errorMessage": "This user name already exists",
                    "validationErrors": null
                }
            }
        }`

        const textObject: IErrorDto = JSON.parse(jsonObject) as IErrorDto;
        const input: IGetErrorMessage = 
        {
            errorObject: textObject
        }
        
        const expectation: string = "This user name already exists";

        // Act
        const result = GetErrorMessage(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("Given valid JSON object with validation errors. When invoke GetErrorMessage. Should return translated error message.", () => 
    {
        // Arrange
        const jsonObject: string = `
        {
            "response": 
            {
                "data":
                {
                    "errorCode": "CANNOT_ADD_DATA",
                    "errorMessage": "Cannot add invalid data",
                    "validationErrors": 
                    [
                        {
                            "propertyName": "Id",
                            "errorCode": "INVALID_GUID",
                            "errorMessage": "Must be GUID"
                        },
                        {
                            "propertyName": "UserAge",
                            "errorCode": "INVALID_NUMBER",
                            "errorMessage": "Cannot be negative number"
                        }
                    ]
                }
            }            
        }`

        const textObject: IErrorDto = JSON.parse(jsonObject) as IErrorDto;
        const input: IGetErrorMessage = 
        {
            errorObject: textObject
        }

        const expectation: string = "Cannot add invalid data. " + VALIDATION_ERRORS + ".";

        // Act
        const result = GetErrorMessage(input);

        // Assert
        expect(result).toBe(expectation);
    });
});
