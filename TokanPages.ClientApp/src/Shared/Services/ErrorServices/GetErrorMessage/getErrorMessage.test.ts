import "../../../../setupTests";
import { VALIDATION_ERRORS } from "../../../constants";
import { ErrorDto } from "../../../../Api/Models";
import { GetErrorMessage } from "..";

describe("verify GetErrorMessage method", () => 
{
    it("should return translated error message, when valid JSON object provided.", () => 
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

        const textObject: ErrorDto = JSON.parse(jsonObject) as ErrorDto;
        const input = 
        {
            errorObject: textObject
        }
        
        const expectation: string = "This user name already exists";

        // Act
        const result = GetErrorMessage(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should return translated error message, when valid JSON object with validation errors provided.", () => 
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

        const textObject: ErrorDto = JSON.parse(jsonObject) as ErrorDto;
        const input = 
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
